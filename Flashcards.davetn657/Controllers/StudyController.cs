using Flashcards.davetn657.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Flashcards.davetn657.Controllers;

public class StudyController
{
    private IConfiguration configuration;
    private string? connectionString;

    public StudyController()
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        this.connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public void AddSession(StudyDTO session)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"INSERT INTO SESSIONS (SessionName)
                                    VALUES (@Name)";

            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = session.Name;
            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveSession(StudyDTO session)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = @"DELETE FROM SESSIONS
                                    WHERE SessionId = @Id";

            tableCmd.Parameters.Add("@Id", SqlDbType.Text).Value = session.Id;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveSession(StackDTO stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = @"DELETE FROM SESSIONS
                                    WHERE SessionId = @Id";

            tableCmd.Parameters.Add("@Id", SqlDbType.Text).Value = stack.Id;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }


    public void EditSession(StudyDTO session)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"UPDATE SESSIONS
                                    SET StudyName = @Name
                                    WHERE StudyId = @Id";

            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = session.Name;
            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = session.Id;
            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Dictionary<string, StudyDTO> ReadAllSessions()
    {
        var allSessions = new Dictionary<string, StudyDTO>();

        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT * FROM SESSIONS";

            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                var session = new StudyDTO();
                session.Id = reader.GetInt32("StudyId");
                session.Id = reader.GetInt32("StackId");
                session.Name = reader.GetString("StudyName");
                
                allSessions.Add(session.Name, session);
            }

            connection.Close();
        }

        return allSessions;
    }
}