using Flashcards.davetn657.Models.DTOs;
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

    public void AddCard(CardDTO card)
    {

    }

    public void RemoveCard()
    {

    }

    public void EditCard()
    {

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
                card.Name = reader.GetString("CardName");
                card.Question = reader.GetString("CardQuestion");
                card.Answer = reader.GetString("CardAnswer");
                allCards.Add(card.Name, card);
            }

            connection.Close();
        }

        return allCards;
    }
}
