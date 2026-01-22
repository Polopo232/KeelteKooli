using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int KoolitusId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Staatus { get; set; }

        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }

    }

}