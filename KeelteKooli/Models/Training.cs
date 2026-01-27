using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Display(Name = "Kursuse nimetus")]
        public string CourseName { get; set; }

        public int TeacherId { get; set; }


        public DateTime AlgusKuupaev { get; set; }
        public DateTime LoppKuupaev { get; set; }

        public decimal Hind { get; set; }
        public int MaxOsalejaid { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
