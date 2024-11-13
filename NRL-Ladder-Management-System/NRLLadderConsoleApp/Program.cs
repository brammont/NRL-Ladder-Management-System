using System;
using NRLLadderConsoleApp.Models;

class Program
{
    static void Main(string[] args)
    {
        DatabaseHelper dbHelper = new DatabaseHelper();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nNRL Ladder Management System");
            Console.WriteLine("1. View Teams");
            Console.WriteLine("2. Add Team");
            Console.WriteLine("3. Update Team");
            Console.WriteLine("4. Delete Team");
            Console.WriteLine("5. View Ladder Positions");
            Console.WriteLine("6. Add Ladder Position");
            Console.WriteLine("7. View Matches");
            Console.WriteLine("8. Add Match");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewTeams(dbHelper);
                    break;
                case "2":
                    AddTeam(dbHelper);
                    break;
                case "3":
                    UpdateTeam(dbHelper);
                    break;
                case "4":
                    DeleteTeam(dbHelper);
                    break;
                case "5":
                    ViewLadderPositions(dbHelper);
                    break;
                case "6":
                    AddLadderPosition(dbHelper);
                    break;
                case "7":
                    ViewMatches(dbHelper);
                    break;
                case "8":
                    AddMatch(dbHelper);
                    break;
                case "9":
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Display all teams
    static void ViewTeams(DatabaseHelper dbHelper)
    {
        var teams = dbHelper.GetTeams();
        Console.WriteLine("\nTeam ID\tTeam Name\tHome Ground");
        foreach (var team in teams)
        {
            Console.WriteLine($"{team.TeamId}\t{team.TeamName}\t{team.HomeGround}");
        }
    }

    // Add a new team
    static void AddTeam(DatabaseHelper dbHelper)
    {
        Console.Write("Enter Team Name: ");
        string teamName = Console.ReadLine();
        Console.Write("Enter Home Ground: ");
        string homeGround = Console.ReadLine();

        dbHelper.AddTeam(teamName, homeGround);
    }

    // Update an existing team
    static void UpdateTeam(DatabaseHelper dbHelper)
    {
        Console.Write("Enter Team ID to update: ");
        int teamId = int.Parse(Console.ReadLine());
        Console.Write("Enter new Team Name: ");
        string newTeamName = Console.ReadLine();
        Console.Write("Enter new Home Ground: ");
        string newHomeGround = Console.ReadLine();

        dbHelper.UpdateTeam(teamId, newTeamName, newHomeGround);
    }

    // Delete a team
    static void DeleteTeam(DatabaseHelper dbHelper)
    {
        Console.Write("Enter Team ID to delete: ");
        int teamId = int.Parse(Console.ReadLine());

        dbHelper.DeleteTeam(teamId);
    }

    // Display all ladder positions
    static void ViewLadderPositions(DatabaseHelper dbHelper)
    {
        var positions = dbHelper.GetLadderPositions();
        Console.WriteLine("\nPosition ID\tPosition\tTeam ID\tPlayed\tPoints\tWins\tDrawn\tLost\tByes\tFor\tAgainst\tDiff");
        foreach (var position in positions)
        {
            Console.WriteLine($"{position.PositionId}\t{position.Position}\t{position.TeamId}\t{position.Played}\t{position.Points}\t{position.Wins}\t{position.Drawn}\t{position.Lost}\t{position.Byes}\t{position.ForScore}\t{position.AgainstScore}\t{position.Diff}");
        }
    }

    // Add a new ladder position
    static void AddLadderPosition(DatabaseHelper dbHelper)
    {
        Console.Write("Enter Position: ");
        int position = int.Parse(Console.ReadLine());
        Console.Write("Enter Team ID: ");
        int teamId = int.Parse(Console.ReadLine());
        Console.Write("Enter Played: ");
        int played = int.Parse(Console.ReadLine());
        Console.Write("Enter Points: ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("Enter Wins: ");
        int wins = int.Parse(Console.ReadLine());
        Console.Write("Enter Drawn: ");
        int drawn = int.Parse(Console.ReadLine());
        Console.Write("Enter Lost: ");
        int lost = int.Parse(Console.ReadLine());
        Console.Write("Enter Byes: ");
        int byes = int.Parse(Console.ReadLine());
        Console.Write("Enter For Score: ");
        int forScore = int.Parse(Console.ReadLine());
        Console.Write("Enter Against Score: ");
        int againstScore = int.Parse(Console.ReadLine());
        Console.Write("Enter Difference: ");
        int diff = int.Parse(Console.ReadLine());

        dbHelper.AddLadderPosition(position, teamId, played, points, wins, drawn, lost, byes, forScore, againstScore, diff);
    }

    // Display all matches
    static void ViewMatches(DatabaseHelper dbHelper)
    {
        var matches = dbHelper.GetMatches();
        Console.WriteLine("\nMatch ID\tDate\t\tTeam 1 ID\tTeam 2 ID\tScore Team 1\tScore Team 2");
        foreach (var match in matches)
        {
            Console.WriteLine($"{match.MatchId}\t{match.Date.ToShortDateString()}\t{match.Team1Id}\t{match.Team2Id}\t{match.ScoreTeam1}\t{match.ScoreTeam2}");
        }
    }

    // Add a new match
    static void AddMatch(DatabaseHelper dbHelper)
    {
        Console.Write("Enter Match Date (yyyy-mm-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Team 1 ID: ");
        int team1Id = int.Parse(Console.ReadLine());
        Console.Write("Enter Team 2 ID: ");
        int team2Id = int.Parse(Console.ReadLine());
        Console.Write("Enter Score for Team 1: ");
        int scoreTeam1 = int.Parse(Console.ReadLine());
        Console.Write("Enter Score for Team 2: ");
        int scoreTeam2 = int.Parse(Console.ReadLine());

        dbHelper.AddMatch(date, team1Id, team2Id, scoreTeam1, scoreTeam2);
    }
}
