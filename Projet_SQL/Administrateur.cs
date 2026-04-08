using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Projet_BDD
{
    internal class Administrateur
    {
        private int Id_Admin { get; set; }
        private string Nom { get; set; }
        private string Prenom { get; set; }
        private string Tel { get; set; }
        private string Email { get; set; }
        private string mdp { get; set; }
        private string Role { get; set; }

        public Administrateur(int id, string Nom, string prenom, string tel, string Email, string mdp, string role)
        {
            this.Id_Admin = id;
            this.Nom = Nom;
            this.Prenom = prenom;
            this.Tel = tel;
            this.Email = Email;
            this.mdp = mdp;
            this.Role = role;
        }
    }
}