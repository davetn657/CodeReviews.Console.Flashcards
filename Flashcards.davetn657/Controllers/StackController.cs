using Flashcards.davetn657.DTOs;
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
                
            tableCmd.CommandText = @"INSERT INTO STACKS (StackName, CreateDate)
                                    VALUES (@Name, @Date)";

            var currentDate = DateTime.Now.ToString(Globals.CULTURE_INFO);

            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = name;
            tableCmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = currentDate;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveStack(StackDTO stack)
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM STACKS 
                                    WHERE      
                                    StackId = @Id AND
                                    StackName = @Name;";

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = stack.Id;
            tableCmd.Parameters.Add("@Name", SqlDbType.Text).Value = stack.Name;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void EditStack(StackDTO stack) 
    {

    }

    public Dictionary<string, StackDTO> ReadAllStacks()
    {
        var allStacks = new Dictionary<string, StackDTO>();

        using (SqlConnection connection = new SqlConnection(connectionString))
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
