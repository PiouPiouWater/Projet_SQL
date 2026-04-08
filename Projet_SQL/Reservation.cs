using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Projet_BDD
{
    internal class Reservation
    {
        private int Id { get; set; }
        private int MembreId { get; set; }
        private int CoursId { get; set; }
        private DateTime DateReservation { get; set; }
        private string Statut { get; set; }

        public Reservation(int Id, int MembreId, int CoursId, DateTime DateResrvation, string statut)
        {
            this.Id = Id;
            this.MembreId = MembreId;
            this.CoursId = CoursId;
            this.DateReservation = DateResrvation;
            this.Statut = statut;
        }
    }
}
