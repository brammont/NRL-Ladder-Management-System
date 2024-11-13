using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NRLLadderConsoleApp.Models;

public class DatabaseHelper
{
    private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=NRLLadderDatabase;Trusted_Connection=True;";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    // CRUD Operations for Teams

    public void AddTeam(string teamName, string homeGround)
    {
        using (var connection = GetConnection())
        {
            string query = "INSERT INTO Teams (team_name, home_ground) VALUES (@teamName, @homeGround)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@teamName", teamName);
                command.Parameters.AddWithValue("@homeGround", homeGround);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Team added successfully.");
            }
        }
    }



    public List<Team> GetTeams()
    {
        List<Team> teams = new List<Team>();
        using (var connection = GetConnection())
        {
            string query = "SELECT * FROM Teams";
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teams.Add(new Team
                        {
                            TeamId = reader.GetInt32(0),
                            TeamName = reader.GetString(1),
                            HomeGround = reader.GetString(2)
                        });
                    }
                }
            }
        }
        return teams;
    }

    public void UpdateTeam(int teamId, string newTeamName, string newHomeGround)
    {
        using (var connection = GetConnection())
        {
            string query = "UPDATE Teams SET team_name = @newTeamName, home_ground = @newHomeGround WHERE team_id = @teamId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@teamId", teamId);
                command.Parameters.AddWithValue("@newTeamName", newTeamName);
                command.Parameters.AddWithValue("@newHomeGround", newHomeGround);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Team updated successfully.");
            }
        }
    }

    public void DeleteTeam(int teamId)
    {
        using (var connection = GetConnection())
        {
            string query = "DELETE FROM Teams WHERE team_id = @teamId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@teamId", teamId);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Team deleted successfully.");
            }
        }
    }

    // CRUD Operations for LadderPositions

    public void AddLadderPosition(int position, int teamId, int played, int points, int wins, int drawn, int lost, int byes, int forScore, int againstScore, int diff)
    {
        using (var connection = GetConnection())
        {
            string query = "INSERT INTO LadderPositions (position, team_id, played, points, wins, drawn, lost, byes, for_score, against_score, diff) VALUES (@position, @teamId, @played, @points, @wins, @drawn, @lost, @byes, @forScore, @againstScore, @diff)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@teamId", teamId);
                command.Parameters.AddWithValue("@played", played);
                command.Parameters.AddWithValue("@points", points);
                command.Parameters.AddWithValue("@wins", wins);
                command.Parameters.AddWithValue("@drawn", drawn);
                command.Parameters.AddWithValue("@lost", lost);
                command.Parameters.AddWithValue("@byes", byes);
                command.Parameters.AddWithValue("@forScore", forScore);
                command.Parameters.AddWithValue("@againstScore", againstScore);
                command.Parameters.AddWithValue("@diff", diff);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Ladder position added successfully.");
            }
        }
    }

    public List<LadderPosition> GetLadderPositions()
    {
        List<LadderPosition> positions = new List<LadderPosition>();
        using (var connection = GetConnection())
        {
            string query = "SELECT * FROM LadderPositions";
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        positions.Add(new LadderPosition
                        {
                            PositionId = reader.GetInt32(0),
                            Position = reader.GetInt32(1),
                            TeamId = reader.GetInt32(2),
                            Played = reader.GetInt32(3),
                            Points = reader.GetInt32(4),
                            Wins = reader.GetInt32(5),
                            Drawn = reader.GetInt32(6),
                            Lost = reader.GetInt32(7),
                            Byes = reader.GetInt32(8),
                            ForScore = reader.GetInt32(9),
                            AgainstScore = reader.GetInt32(10),
                            Diff = reader.GetInt32(11)
                        });
                    }
                }
            }
        }
        return positions;
    }

    // CRUD Operations for Matches

    public void AddMatch(DateTime date, int team1Id, int team2Id, int scoreTeam1, int scoreTeam2)
    {
        using (var connection = GetConnection())
        {
            string query = "INSERT INTO Matches (date, team_1_id, team_2_id, score_team_1, score_team_2) VALUES (@date, @team1Id, @team2Id, @scoreTeam1, @scoreTeam2)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@team1Id", team1Id);
                command.Parameters.AddWithValue("@team2Id", team2Id);
                command.Parameters.AddWithValue("@scoreTeam1", scoreTeam1);
                command.Parameters.AddWithValue("@scoreTeam2", scoreTeam2);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Match added successfully.");
            }
        }
    }

    public List<Match> GetMatches()
    {
        List<Match> matches = new List<Match>();
        using (var connection = GetConnection())
        {
            string query = "SELECT * FROM Matches";
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matches.Add(new Match
                        {
                            MatchId = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            Team1Id = reader.GetInt32(2),
                            Team2Id = reader.GetInt32(3),
                            ScoreTeam1 = reader.GetInt32(4),
                            ScoreTeam2 = reader.GetInt32(5)
                        });
                    }
                }
            }
        }
        return matches;
    }
}
