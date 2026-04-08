using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Projet_BDD
{
    internal class Coach
    {
        private int Id { get; set; }
        private string Nom { get; set; }
        private string Prenom { get; set; }
        private string Telephone { get; set; }
        private string specialite { get; set; }
        private string Email { get; set; }
        public Coach(int Id, string nom, string prenom, string specialite, string telephone, string email)
        {
            this.Id = Id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.specialite = specialite;
            this.Telephone = telephone;
            this.Email = email;
        }
        public override string ToString()
        {
            return $"ID : {this.Id}\nNom : {this.Nom}\nPrenom : {this.Prenom}\nSpecialite : {this.specialite}\nTelephone : {this.Telephone}\nEmail : {this.Email}";
        }
    }
}