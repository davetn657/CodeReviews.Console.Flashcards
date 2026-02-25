using Flashcards.davetn657.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Flashcards.davetn657.Controllers;

public class ScoreController
{
    private IConfiguration configuration;
    private string? connectionString;

    public ScoreController()
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        this.connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    internal void AddScore(StudyDto session, ScoreDto score)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"INSERT INTO SCORES (SessionId, Score)
                                    VALUES (@id, @score)";

            tableCmd.Parameters.Add("@id", SqlDbType.Int).Value = session.Id;
            tableCmd.Parameters.Add("@score", SqlDbType.Int).Value = score.Score;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    internal List<ScoreDto> GetScores(int numberOfDays)
    {
        var pastScores = new List<ScoreDto>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT Sessions.StackId, Sessions.SessionName, Scores.Score, Scores.CreateDate
                                    FROM Sessions
                                    LEFT JOIN Scores ON Sessions.SessionId = Scores.SessionId
                                    WHERE CAST(Scores.CreateDate AS DATE) >= DATEADD(day, @numDays, GETDATE())
                                    GROUP BY Sessions.StackId, Sessions.SessionName, Scores.Score, Scores.CreateDate
                                    ORDER BY Scores.CreateDate DESC";

            tableCmd.Parameters.Add("@numDays", SqlDbType.Int).Value = -numberOfDays;

            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                var data = new ScoreDto();
                data.SessionId = reader.GetInt32("StackId");
                data.Name = reader.GetString("SessionName");
                data.Score = reader.GetInt32("Score");
                data.CreateDate = reader.GetDateTime("CreateDate");

                pastScores.Add(data);
            }

            connection.Close();
        }

        return pastScores;
    }
}
