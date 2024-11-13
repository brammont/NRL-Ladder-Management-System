namespace NRLLadderConsoleApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty; // Default to an empty string
        public string HomeGround { get; set; } = string.Empty; // Default to an empty string
    }
}
