namespace NRLLadderConsoleApp.Models
{
    public class LadderPosition
    {
        public int PositionId { get; set; }      // Matches position_id in the database
        public int Position { get; set; }        // Matches position in the database
        public int TeamId { get; set; }          // Matches team_id in the database (Foreign Key)
        public int Played { get; set; }          // Matches played in the database
        public int Points { get; set; }          // Matches points in the database
        public int Wins { get; set; }            // Matches wins in the database
        public int Drawn { get; set; }           // Matches drawn in the database
        public int Lost { get; set; }            // Matches lost in the database
        public int Byes { get; set; }            // Matches byes in the database
        public int ForScore { get; set; }        // Matches for_score in the database
        public int AgainstScore { get; set; }    // Matches against_score in the database
        public int Diff { get; set; }            // Matches diff in the database
    }
}
