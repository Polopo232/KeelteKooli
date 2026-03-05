        using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    namespace KeelteKooli.Models
    {
        public class Teacher
        {
            public int Id { get; set; }

            [Required]
            public string Nimi { get; set; }

            public string Kvalifikatsioon { get; set; }
            public string FotoPath { get; set; }

            public string ApplicationUserId { get; set; }
            public virtual ApplicationUser ApplicationUser { get; set; }
            public virtual ICollection<Training> Trainings { get; set; }
    }

}