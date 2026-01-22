using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Nimetus { get; set; }
        public string Keel { get; set; }
        public string Tase { get; set; }
        public string Kirjeldus { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }

}