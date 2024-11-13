using System;
using System.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;

public static class CSVImporter
{
    // Connection string to connect to the SQL Server database
    private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=NRLLadderDatabase;Trusted_Connection=True;";

    // Method to import ladder position data from a CSV file
    public static void ImportLadderPositionsFromCSV(string filePath)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Use TextFieldParser to read the CSV file
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadLine(); // Skip the header line if present

                // Loop through each line in the CSV file
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    // Parse each field to match the database columns
                    int position = int.Parse(fields[0]);
                    string teamName = fields[1];
                    int played = int.Parse(fields[2]);
                    int points = int.Parse(fields[3]);
                    int wins = int.Parse(fields[4]);
                    int drawn = int.Parse(fields[5]);
                    int lost = int.Parse(fields[6]);
                    int byes = int.Parse(fields[7]);
                    int forScore = int.Parse(fields[8]);
                    int againstScore = int.Parse(fields[9]);
                    int diff = int.Parse(fields[10]);

                    // Get or add the team and retrieve its team_id
                    int teamId = GetOrAddTeam(connection, teamName);

                    // Insert the data into the LadderPositions table
                    string query = "INSERT INTO LadderPositions (position, team_id, played, points, wins, drawn, lost, byes, for_score, against_score, diff) " +
                                   "VALUES (@position, @teamId, @played, @points, @wins, @drawn, @lost, @byes, @forScore, @againstScore, @diff)";
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

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        Console.WriteLine("Ladder positions data imported successfully from CSV.");
    }

    // Helper method to get or add a team and return its team_id
    private static int GetOrAddTeam(SqlConnection connection, string teamName)
    {
        // Check if the team already exists in the Teams table
        string selectQuery = "SELECT team_id FROM Teams WHERE team_name = @teamName";
        using (var selectCommand = new SqlCommand(selectQuery, connection))
        {
            selectCommand.Parameters.AddWithValue("@teamName", teamName);
            var result = selectCommand.ExecuteScalar();

            if (result != null)
            {
                return (int)result; // If team exists, return the existing team_id
            }
        }

        // Insert a new team if it doesn't exist
        string insertQuery = "INSERT INTO Teams (team_name) OUTPUT INSERTED.team_id VALUES (@teamName)";
        using (var insertCommand = new SqlCommand(insertQuery, connection))
        {
            insertCommand.Parameters.AddWithValue("@teamName", teamName);
            return (int)insertCommand.ExecuteScalar(); // Return the new team_id after insertion
        }
    }
}
