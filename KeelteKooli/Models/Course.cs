using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Level { get; set; } // A1-C2
        public string Description { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }

}