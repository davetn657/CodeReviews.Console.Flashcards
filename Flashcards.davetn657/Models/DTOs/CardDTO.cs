namespace Flashcards.davetn657.Models.DTOs;

public class CardDTO
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public DateTime LastAppearance { get; set; }
    public DateTime NextAppearance { get; set; }
}
