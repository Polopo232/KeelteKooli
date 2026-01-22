using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Training
    {
        public int Id { get; set; }
        public int KeelekursusId { get; set; }
        public int OpetajaId { get; set; }

        public DateTime AlgusKuupaev { get; set; }
        public DateTime LoppKuupaev { get; set; }

        public decimal Hind { get; set; }
        public int MaxOsalejaid { get; set; }

        public virtual Course Course { get; set; }
        public int CourseId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
