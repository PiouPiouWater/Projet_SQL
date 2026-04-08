using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Interface_Projet_BDD;
using MySql.Data.MySqlClient;


namespace Projet_SQL
{
    internal class BDD
    {

        private MySqlConnection maConnexion = null;


        public BDD()
        {
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=SalleDeSport;" + "UID=AdminGerant1;PASSWORD=Root";
                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
                Console.WriteLine("connexion établie");
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Erreur de connexion au serveur");
                        break;
                    case 1045:
                        Console.WriteLine("Erreur uid/password");
                        break;
                    default:
                        Console.WriteLine(" ErreurConnexion : " + e.ToString());
                        break;
                }
                return;
            }
        }
        public void InscriptionMembre(string nom, string prenom, string email, string tel, string adresse, DateTime dateinscription, string mdp)
        {

            string cmd = "INSERT INTO Membre (Nom, Prenom, Email, Telephone, Adresse, DateInscription, Mdp) " + "VALUES (@nom, @prenom, @email, @tel, @adresse, @dateinscription,@mdp);";
            int membreId;
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@nom", nom);
                requete.Parameters.AddWithValue("@prenom", prenom);
                requete.Parameters.AddWithValue("@email", email);
                requete.Parameters.AddWithValue("@tel", tel);
                requete.Parameters.AddWithValue("@adresse", adresse);
                requete.Parameters.AddWithValue("@dateinscription", dateinscription);
                requete.Parameters.AddWithValue("@mdp", mdp);
                requete.ExecuteNonQuery();

                membreId = (int)requete.LastInsertedId;
            }
            string cmdInscription = "INSERT INTO Inscription (MembreID, Statut) VALUES (@membreId, 'EnAttente')";
            MySqlCommand requete2 = new MySqlCommand(cmdInscription, maConnexion);
            requete2.Parameters.AddWithValue("@membreId", membreId);
            requete2.ExecuteNonQuery();
        }
        public void ModifStatut(int ID)
        {
            string cmd = "UPDATE Inscription SET Statut=@Statut WHERE MembreID = @id";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@Statut", "Validée");
                requete.Parameters.AddWithValue("@id", ID);
                requete.ExecuteNonQuery();
            }
        }
        public bool CoursComplet(int coursID)
        {
            string cmd = "SELECT COUNT(*) FROM Reservation WHERE CoursID = @coursID";
            MySqlCommand requete = new MySqlCommand(cmd, maConnexion);
            requete.Parameters.AddWithValue("@coursID", coursID);
            int participants = Convert.ToInt32(requete.ExecuteScalar());
            string cmd2 = "SELECT CapaciteMax FROM Cours WHERE ID = @coursID";
            MySqlCommand requete2 = new MySqlCommand(cmd2, maConnexion);
            requete2.Parameters.AddWithValue("@coursID", coursID);
            int partmax = Convert.ToInt32(requete2.ExecuteScalar());
            return participants >= partmax;
        }
        public bool InscriptionCours(int membreID, int coursID)
        {
            if (CoursComplet(coursID))
            {
                return false;
            }
            string cmd = "INSERT INTO Reservation (MembreID, CoursID, DateReservation, Statut)" + "VALUES (@membreID,@coursID,@date,@statut)";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@membreId", membreID);
                requete.Parameters.AddWithValue("@coursId", coursID);
                requete.Parameters.AddWithValue("@date", DateTime.Now);
                requete.Parameters.AddWithValue("@statut", "Confirmée");
                requete.ExecuteNonQuery();
            }
            return true;

        }
        public int Connexion(string email, string mdp)
        {
            string cmd = "SELECT ID FROM Membre WHERE Email = @email AND Mdp = @mdp";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@email", email);
                requete.Parameters.AddWithValue("@mdp", mdp);
                int idMembre = -1;
                if (requete.ExecuteScalar() != null)
                {
                    idMembre = Convert.ToInt32(requete.ExecuteScalar());
                }
                return idMembre;
            }
        }
        public bool ConnexionAdmin(string email, string mdp)
        {
            string cmd = "SELECT COUNT(*) FROM Administrateur WHERE Email=@email AND Mdp=@mdp";
            int count;
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@email", email);
                requete.Parameters.AddWithValue("@mdp", mdp);
                count = Convert.ToInt32(requete.ExecuteScalar());
            }
            return count > 0;
        }
        public List<Membre> AfficherInscriptionEnAttente()
        {
            List<Membre> membres = new List<Membre>();
            string cmd = "SELECT m.ID, m.Nom, m.Prenom, m.Email, m.Telephone, m.Adresse, m.DateInscription " + "FROM Membre m INNER JOIN Inscription i ON m.ID = i.MembreID WHERE i.Statut = 'EnAttente'";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            using (MySqlDataReader reader = requete.ExecuteReader())
            {
                while (reader.Read())
                {
                    membres.Add(new Membre(reader.GetInt32("ID"), reader.GetString("Nom"), reader.GetString("Prenom"), reader.GetString("Email"), reader.GetString("Telephone"), reader.GetString("Adresse"), reader.GetDateTime("DateInscription"), ""));
                }
            }
            return membres;
        }
        public List<Cours> AfficherCours()
        {
            List<Cours> cours = new List<Cours>();
            string cmd = "SELECT * FROM Cours";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            using (MySqlDataReader reader = requete.ExecuteReader())
            {
                while (reader.Read())
                {
                    cours.Add(new Cours(reader.GetInt32("ID"), reader.GetString("Nom"), reader.GetString("Description"), reader.GetDateTime("DateHeure"), reader.GetInt32("DureeMinutes"), reader.GetInt32("CapaciteMax"), reader.GetInt32("NiveauDifficulte"), reader.GetInt32("CoachID"), reader.GetString("CoachNom")));
                }
            }
            return cours;
        }
        public void AjoutCours(string NomCours, string Description, DateTime DateHeure, int Duree, int CapaciteMax, int NiveauDifficulte, int CoachId, string CoachNom)
        {
            string cmd = "INSERT INTO Cours (Nom, Description, DateHeure, DureeMinutes, CapaciteMax, NiveauDifficulte, CoachID, CoachNom) " +
                 "VALUES (@Nom, @Description, @DateHeure, @DureeMinutes, @CapaciteMax, @NiveauDifficulte, @CoachID, @CoachNom);";

            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@Nom", NomCours);
                requete.Parameters.AddWithValue("@Description", Description);
                requete.Parameters.AddWithValue("@DateHeure", DateHeure);
                requete.Parameters.AddWithValue("@DureeMinutes", Duree);
                requete.Parameters.AddWithValue("@CapaciteMax", CapaciteMax);
                requete.Parameters.AddWithValue("@NiveauDifficulte", NiveauDifficulte);
                requete.Parameters.AddWithValue("@CoachID", CoachId);
                requete.Parameters.AddWithValue("@CoachNom", CoachNom);

                requete.ExecuteNonQuery();
            }
        }
        public List<Coach> AfficherCoach()
        {
            List<Coach> Coachs = new List<Coach>();
            string cmd = "SELECT * FROM Coach";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            using (MySqlDataReader reader = requete.ExecuteReader())
            {
                while (reader.Read())
                {
                    Coachs.Add(new Coach(reader.GetInt32("ID"), reader.GetString("Nom"), reader.GetString("Prenom"), reader.GetString("Specialite"), reader.GetString("Telephone"), reader.GetString("Email")));
                }
            }
            return Coachs;
        }
        public void AjoutCoach(string NomCoach, string PrenomCoach, string Telephone, string Specialite, string Email)
        {
            string cmd = "INSERT INTO Coach (Nom, Prenom, Telephone, Specialite, Email)" + "VALUES(@Nom,@Prenom,@Telephone,@Specialite,@Email)";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@Nom", NomCoach);
                requete.Parameters.AddWithValue("@Prenom", PrenomCoach);
                requete.Parameters.AddWithValue("@Telephone", Telephone);
                requete.Parameters.AddWithValue("@Specialite", Specialite);
                requete.Parameters.AddWithValue("@Email", Email);
                requete.ExecuteNonQuery();
            }
        }
        public void ModifierCoach(int ID, string nom, string prenom, string specialite, string telephone, string email)
        {
            string cmd = "UPDATE Coach SET Nom=@nom, Prenom=@prenom, Specialite=@specialite, Telephone=@telephone, Email=@email WHERE ID=@id;";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@id", ID);
                requete.Parameters.AddWithValue("@nom", nom);
                requete.Parameters.AddWithValue("@prenom", prenom);
                requete.Parameters.AddWithValue("@specialite", specialite);
                requete.Parameters.AddWithValue("@telephone", telephone);
                requete.Parameters.AddWithValue("@email", email);
                requete.ExecuteNonQuery();
            }
        }
        public void ModifierCours(int idCours, string nom, string description, DateTime dateHeure, int dureeMinutes, int capaciteMax, int niveauDifficulte, int coachId, string coachNom)
        {
            string cmd = "UPDATE Cours SET Nom=@nom, Description=@description, DateHeure=@dateheure, DureeMinutes=@dureeminutes, CapaciteMax=@capacitemax, NiveauDifficulte=@niveaudifficulte, CoachID=@coachid, CoachNom=@coachnom WHERE ID=@id;";
            using (MySqlCommand requete = new MySqlCommand(cmd, maConnexion))
            {
                requete.Parameters.AddWithValue("@id", idCours);
                requete.Parameters.AddWithValue("@nom", nom);
                requete.Parameters.AddWithValue("@description", description);
                requete.Parameters.AddWithValue("@dateheure", dateHeure);
                requete.Parameters.AddWithValue("@dureeminutes", dureeMinutes);
                requete.Parameters.AddWithValue("@capacitemax", capaciteMax);
                requete.Parameters.AddWithValue("@niveaudifficulte", niveauDifficulte);
                requete.Parameters.AddWithValue("@coachid", coachId);
                requete.Parameters.AddWithValue("@coachnom", coachNom);

                requete.ExecuteNonQuery();
            }
        }
    }
}