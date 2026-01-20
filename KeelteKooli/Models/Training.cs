using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int MaxParticipants { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }

}