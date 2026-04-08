using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Projet_BDD
{
    internal class Cours
    {
        private int Id { get; set; }
        private string Nom { get; set; }
        private string Description { get; set; }
        private DateTime DateHeure { get; set; }
        private int DureeMinutes { get; set; }
        private int CapaciteMax { get; set; }
        private int NiveauDifficulte { get; set; }
        private int CoachId { get; set; }
        private string CoachNom { get; set; }

        public Cours(int id, string nom, string description, DateTime dateHeure, int dureeMinutes, int capaciteMax, int niveauDifficulte, int coachId, string coachNom)
        {
            this.Id = id;
            this.Nom = nom;
            this.Description = description;
            this.DateHeure = dateHeure;
            this.DureeMinutes = dureeMinutes;
            this.CapaciteMax = capaciteMax;
            this.NiveauDifficulte = niveauDifficulte;
            this.CoachId = coachId;
            this.CoachNom = coachNom;
        }
        public override string ToString()
        {
            return $"Nom : {this.Nom}\nDescription : {this.Description}\nDate et heure : {this.DateHeure}\nDurée (minutes) : {this.DureeMinutes}\nCapacité maximale : {this.CapaciteMax}\nNiveau de difficulté : {this.NiveauDifficulte}\nCoach : {this.CoachNom}";
        }
    }
}