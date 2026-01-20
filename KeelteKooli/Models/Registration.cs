using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKooli.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Status { get; set; } // Ootel / Kinnitatud / Tühistatud

        public virtual Training Training { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

}