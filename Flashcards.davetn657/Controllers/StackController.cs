using Flashcards.davetn657.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Flashcards.davetn657.Controllers;
public class StackController
{
    private IConfiguration configuration;
    private string? connectionString;

    public StackController()
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        this.connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public void AddStack(string name)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
                
            tableCmd.CommandText = @"INSERT INTO STACKS (StackName)
                                    VALUES (@Name)";

            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = name;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveStack(StackDTO stack)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM STACKS 
                                    WHERE      
                                    StackId = @Id AND
                                    StackName = @Name";

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = stack.Id;
            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = stack.Name;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveStack(StackDTO stack, CardController cardController, StudyController sessionController)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM STACKS 
                                    WHERE      
                                    StackId = @Id AND
                                    StackName = @Name";

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = stack.Id;
            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = stack.Name;

            cardController.RemoveCard(stack);
            sessionController.RemoveSession(stack);
            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void EditStack(StackDTO stack) 
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"UPDATE STACKS
                                    SET StackName = @NewName
                                    WHERE StackId = @Id";

            tableCmd.Parameters.Add("@NewName", SqlDbType.Text).Value = stack.Name;
            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = stack.Id;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Dictionary<string, StackDTO> ReadAllStacks()
    {
        var allStacks = new Dictionary<string, StackDTO>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"SELECT * FROM STACKS";

            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                var stack = new StackDTO();
                stack.Id = reader.GetInt32("StackId");
                stack.Name = reader.GetString("StackName");

                allStacks.Add(stack.Name, stack);
            }

            connection.Close();
        }

        return allStacks;
    }
}
