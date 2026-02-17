using Flashcards.davetn657.Models;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Flashcards.davetn657.Controllers;

public class CardController 
{
    private IConfiguration configuration;
    private string? connectionString;

    public CardController() 
    {
        this.configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        this.connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public void AddCard(CardDTO card, StackDTO stack)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"INSERT INTO CARDS (StackId, CardQuestion, CardAnswer)
                                    VALUES (@StackId, @Question, @Answer)";

            tableCmd.Parameters.Add("@StackId", SqlDbType.Int).Value = stack.Id;
            tableCmd.Parameters.Add("@Question", SqlDbType.Text).Value = card.Question;
            tableCmd.Parameters.Add("@Answer", SqlDbType.Text).Value = card.Answer;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void RemoveCard(CardDTO card)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM CARDS
                                    WHERE CardId = @Id";

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = card.Id;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    public void RemoveCard(StackDTO stack)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"DELETE FROM CARDS
                                    WHERE StackId = @Id";

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = stack.Id;

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    public void EditCard(CardDTO card, Enum option)
    {
        using(var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();

            if (option.Equals(EditCardOptions.ChangeAnswer))
            {
                tableCmd.CommandText = @"UPDATE CARDS
                                        SET CardAnswer = @Answer
                                        WHERE CardId = @id";
                tableCmd.Parameters.Add("@Answer", SqlDbType.Text).Value = card.Answer;
            }
            else if(option.Equals(EditCardOptions.ChangeQuestion))
            {
                tableCmd.CommandText = @"UPDATE CARDS
                                        SET CardQuestion = @Question
                                        WHERE CardId = @id";
                tableCmd.Parameters.Add("@Question", SqlDbType.Text).Value = card.Question;
            }

            tableCmd.Parameters.Add("@Id", SqlDbType.Int).Value = card.Id;
            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    public Dictionary<string, CardDTO> ReadAllCards()
    {
        var allCards = new Dictionary<string, CardDTO>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var tabldCmd = connection.CreateCommand();
            tabldCmd.CommandText = @"SELECT * FROM CARDS";

            var reader = tabldCmd.ExecuteReader();

            while (reader.Read())
            {
                var card = new CardDTO();
                card.Id = reader.GetInt32("CardId");
                card.Question = reader.GetString("CardQuestion");
                card.Answer = reader.GetString("CardAnswer");
                card.LastAppearance = reader.GetDateTime("LastAppearance");
                card.NextAppearance = reader.GetDateTime("NextAppearance");
                allCards.Add(card.Question, card);
            }

            connection.Close();
        }

        return allCards;
    }
}
