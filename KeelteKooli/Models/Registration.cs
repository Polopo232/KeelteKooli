using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeelteKooli.Models
{
    public enum RegistrationStatus
    {
        Ootel = 0,
        Kinnitatud = 1,
        Tuhistatud = 2
    }

    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public int TrainingId { get; set; }

        [ForeignKey("TrainingId")]
        public virtual Training Training { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public RegistrationStatus Staatus { get; set; } = RegistrationStatus.Ootel;
    }
}
