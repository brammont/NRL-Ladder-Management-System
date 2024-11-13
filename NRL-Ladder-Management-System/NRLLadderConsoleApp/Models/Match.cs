namespace NRLLadderConsoleApp.Models
{
    public class Match
    {
        public int MatchId { get; set; }         // Matches match_id in the database
        public DateTime Date { get; set; }       // Matches date in the database
        public int Team1Id { get; set; }         // Matches team_1_id in the database (Foreign Key)
        public int Team2Id { get; set; }         // Matches team_2_id in the database (Foreign Key)
        public int ScoreTeam1 { get; set; }      // Matches score_team_1 in the database
        public int ScoreTeam2 { get; set; }      // Matches score_team_2 in the database
    }
}
