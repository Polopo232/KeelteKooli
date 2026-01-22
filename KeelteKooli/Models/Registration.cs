using KeelteKooli.Models;

public class Registration
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public string ApplicationUserId { get; set; }
    public string Staatus { get; set; }

    public virtual Training Training { get; set; }
    public virtual ApplicationUser User { get; set; }
}
