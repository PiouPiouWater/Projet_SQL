using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interface_Projet_BDD;

namespace Projet_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tentative de connexion...");
            BDD maBdd = new BDD();
            bool continuer = true;
            while (continuer == true)
            {
                Console.Clear();
                Console.WriteLine("SALLE DE SPORT : ACCUEIL");
                Console.WriteLine("1. Je suis membre (Connexion)");
                Console.WriteLine("2. Je m'inscris (Nouveau membre)");
                Console.WriteLine("3. Je suis administrateur");
                Console.WriteLine("0. Quitter");
                Console.Write("Votre choix : ");

                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Connexion Membre");
                        Console.WriteLine("\nEmail : ");
                        string email = Console.ReadLine();
                        Console.WriteLine("\n mdp : ");
                        string mdp = Console.ReadLine();
                        Console.Clear();
                        int idMembre = maBdd.Connexion(email, mdp);
                        if (idMembre != -1)
                        {
                            Console.WriteLine("Bienvenue sur votre espace voulez vous inscrire à un cours ?(O/N)");
                            string repm = Console.ReadLine();
                            switch (repm)
                            {
                                case ("O"):
                                    Console.Clear();
                                    Console.WriteLine("Entrez l'ID du cours auquel vous souhaitez participer");
                                    int ID = int.Parse(Console.ReadLine());
                                    bool inscription = maBdd.InscriptionCours(idMembre, ID);
                                    if (inscription)
                                    {
                                        Console.WriteLine("Succès de l'inscription");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cours Complet");
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Addresse mail ou mdp incorrect");
                        }
                        Thread.Sleep(5000);
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Inscription");
                        Console.WriteLine("\nJe remplis le formulaire : ");
                        Console.WriteLine("\nNom : ");
                        string nom = Console.ReadLine();
                        Console.WriteLine("\nPrenom : ");
                        string prenom = Console.ReadLine();
                        Console.WriteLine("\nEmail : ");
                        string email2 = Console.ReadLine();
                        Console.WriteLine("\nNuméro de téléphone : ");
                        string telephone = Console.ReadLine();
                        Console.WriteLine("\nAddresse : ");
                        string adresse = Console.ReadLine();
                        Console.WriteLine("\nCréez un mot de passe : ");
                        string Mdp = Console.ReadLine();
                        DateTime dateinscription = DateTime.Now;
                        Console.WriteLine("Appuyez pour continuer...");
                        Console.ReadKey();
                        maBdd.InscriptionMembre(nom, prenom, email2, telephone, adresse, dateinscription, Mdp);
                        Console.WriteLine("Membre ajouté !");
                        Thread.Sleep(1000);
                        break;//Inscription

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Accès Admin");
                        Console.WriteLine("\nEmail : ");
                        string user = Console.ReadLine();
                        Console.WriteLine("\nMot de Passe : ");
                        string mdpAdmin = Console.ReadLine();
                        Console.Clear();
                        if (maBdd.ConnexionAdmin(user, mdpAdmin))
                        {
                            bool rester = true;
                            while (rester == true)
                            {
                                Console.Clear();
                                Console.WriteLine("INTERFACE GESTION " + user);
                                Console.WriteLine("1- Afficher les inscriptions en attente de validation");
                                Console.WriteLine("2- Afficher les cours");
                                Console.WriteLine("3- Ajouter un cours");
                                Console.WriteLine("4- Modifier un cours");
                                Console.WriteLine("5- Afficher les coachs");
                                Console.WriteLine("6- Ajouter un coach");
                                Console.WriteLine("7- Modifier un coach");
                                Console.WriteLine("0- Retour au menu principal");
                                Console.Write("\nVotre choix : ");
                                string choixAdmin = Console.ReadLine();

                                switch (choixAdmin)
                                {
                                    case "1":
                                        Console.WriteLine("\n>> Liste des inscriptions en attente...");
                                        List<Membre> membres = maBdd.AfficherInscriptionEnAttente();
                                        foreach (Membre membre in membres)
                                        {
                                            Console.WriteLine(membre);
                                        }
                                        Console.WriteLine("Voulez vous valider les inscriptions (O/N)");
                                        string rep = Console.ReadLine();
                                        switch (rep)
                                        {
                                            case ("O"):
                                                foreach (Membre membre in membres)
                                                {
                                                    maBdd.ModifStatut(membre.Id);
                                                }
                                                Console.WriteLine("Succès de la modification");
                                                break;
                                            default:
                                                break;
                                        }

                                        break;//affichage inscription en attente
                                    case "2":
                                        Console.WriteLine("\n>> Liste des cours...");
                                        List<Cours> cours = maBdd.AfficherCours();
                                        foreach (Cours cour in cours)
                                        {
                                            Console.WriteLine(cour);
                                        }
                                        break;//affichage cours
                                    case "3":
                                        Console.WriteLine("\n>> Ajout d'un cours...");
                                        Console.WriteLine("Nom :");
                                        string NomCours = Console.ReadLine();
                                        Console.WriteLine("Description :");
                                        string Description = Console.ReadLine();
                                        Console.WriteLine("Date et Heure :");
                                        DateTime DateHeure = DateTime.Parse(Console.ReadLine());
                                        Console.WriteLine("Durée (en Minutes) :");
                                        int Duree = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Capacité maximale :");
                                        int CapaciteMax = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Niveau de difficulté :");
                                        int NiveauDifficulte = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Coach ID :");
                                        int CoachId = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Nom du Coach :");
                                        string CoachNom = Console.ReadLine();
                                        maBdd.AjoutCours(NomCours, Description, DateHeure, Duree, CapaciteMax, NiveauDifficulte, CoachId, CoachNom);
                                        break;//ajout cours
                                    case "4":
                                        Console.WriteLine("\n>> Modification d'un cours...");
                                        Console.WriteLine("ID du Cours à modifier");
                                        int IDCours = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Entrez les modifications");
                                        Console.WriteLine("Nom :");
                                        string NomCoursM = Console.ReadLine();
                                        Console.WriteLine("Description :");
                                        string DescriptionM = Console.ReadLine();
                                        Console.WriteLine("Date et Heure :");
                                        DateTime DateHeureM = DateTime.Parse(Console.ReadLine());
                                        Console.WriteLine("Durée (en Minutes) :");
                                        int DureeM = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Capacité maximale :");
                                        int CapaciteMaxM = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Niveau de difficulté :");
                                        int NiveauDifficulteM = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Coach ID :");
                                        int CoachIdM = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Nom du Coach :");
                                        string CoachNomM = Console.ReadLine();
                                        maBdd.ModifierCours(IDCours, NomCoursM, DescriptionM, DateHeureM, DureeM, CapaciteMaxM, NiveauDifficulteM, CoachIdM, CoachNomM);
                                        Console.WriteLine("Modification réalisée avec succès");
                                        break;//Modif Cours
                                    case "5":
                                        Console.WriteLine("\n>> Liste des coachs...");
                                        List<Coach> Coachs = maBdd.AfficherCoach();
                                        foreach (Coach coach in Coachs)
                                        {
                                            Console.WriteLine(coach);
                                        }
                                        break;//affichage coachs
                                    case "6":
                                        Console.WriteLine("\n>> Ajout d'un coach...");
                                        Console.WriteLine("\nNom : ");
                                        string nomCoach = Console.ReadLine();
                                        Console.WriteLine("\nPrenom : ");
                                        string prenomCoach = Console.ReadLine();
                                        Console.WriteLine("\nNuméro de téléphone : ");
                                        string telephoneCoach = Console.ReadLine();
                                        Console.WriteLine("\nSpecialite : ");
                                        string Specialite = Console.ReadLine();
                                        Console.WriteLine("\nEmail : ");
                                        string emailCoach = Console.ReadLine();
                                        maBdd.AjoutCoach(nomCoach, prenomCoach, telephoneCoach, Specialite, emailCoach);
                                        break;//ajout coach
                                    case "7":
                                        Console.WriteLine("\n>> Modification d'un coach...");
                                        Console.WriteLine("ID du coach à modifer");
                                        int IDCoach = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Entrez les modifications :");
                                        Console.WriteLine("Nom : ");
                                        string nomCoachM = Console.ReadLine();
                                        Console.WriteLine("Prenom : ");
                                        string prenomCoachM = Console.ReadLine();
                                        Console.WriteLine("Spécialité : ");
                                        string specialiteCoachM = Console.ReadLine();
                                        Console.WriteLine("Téléphone : ");
                                        string telephoneCoachM = Console.ReadLine();
                                        Console.WriteLine("Email : ");
                                        string emailCoachM = Console.ReadLine();
                                        maBdd.ModifierCoach(IDCoach, nomCoachM, prenomCoachM, specialiteCoachM, telephoneCoachM, emailCoachM);
                                        Console.WriteLine("Modification réalisée avec succès");
                                        break;//Modif Coach
                                    case "0":
                                        rester = false;
                                        break;
                                    default:
                                        Console.WriteLine("\nChoix invalide.");
                                        break;
                                }
                                Console.ReadKey();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Email ou mdp incorrect");
                        }
                        break;//Admin



                    case "0":
                        continuer = false;
                        Console.WriteLine("\nFermeture du programme...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("\nChoix invalide.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
