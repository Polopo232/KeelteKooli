using KeelteKooli.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KeelteKooli.Models
{
    public class Training
    {
        public int Id { get; set; }

        [Display(Name = "Kursuse nimetus")]
        public string CourseName { get; set; }

        [Display(Name = "Keel")]
        public string CourseKeel { get; set; }

        [Display(Name = "Tase")]
        public string CourseTase { get; set; }

        public int TeacherId { get; set; }

        public DateTime AlgusKuupaev { get; set; }
        public DateTime LoppKuupaev { get; set; }

        public decimal Hind { get; set; }
        public int MaxOsalejaid { get; set; }

        public virtual Course Course { get; set; }

        public int CourseId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}

