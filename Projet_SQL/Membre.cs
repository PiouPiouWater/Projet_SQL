using System;

namespace Interface_Projet_BDD
{
    internal class Membre
    {
        private int id;
        private string Nom { get; set; }
        private string Prenom { get; set; }
        private string Email { get; set; }
        private string Telephone { get; set; }
        private string Adresse { get; set; }
        private string Mdp { get; set; }
        private DateTime DateInscription { get; set; }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Membre(int Id, string nom, string Prenom, string Email, string Telephone, string Adresse, DateTime dateinscription, string mdp)
        {
            this.id = Id;
            this.Nom = nom;
            this.Prenom = Prenom;
            this.Email = Email;
            this.Telephone = Telephone;
            this.Adresse = Adresse;
            this.DateInscription = dateinscription;
            this.Mdp = mdp;
        }
        public override string ToString()
        {
            string a = $"ID : {this.id}\nNom : {this.Nom}\nPrenom : {this.Prenom}\nEmail : {this.Email}\nTelephone : {this.Telephone}\nAdresse : {this.Adresse}\nDate Inscription : {this.DateInscription}";
            return a;
        }
    }
}