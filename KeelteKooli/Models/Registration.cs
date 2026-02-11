using KeelteKooli.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Registration
{
    public int Id { get; set; }

    [Required]
    public int TrainingId { get; set; }

    [Required]
    public string ApplicationUserId { get; set; }

    public string Staatus { get; set; }

    [ForeignKey("TrainingId")]
    public virtual Training Training { get; set; }

    [ForeignKey("ApplicationUserId")]
    public virtual ApplicationUser User { get; set; }
}
