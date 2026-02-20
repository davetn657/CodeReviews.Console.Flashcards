using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;
public class CardView : UserInterface
{
    private readonly CardController _cardController;

    public CardView(CardController cardController)
    {
        _cardController = cardController;
    }

    internal void CreateCard(StackDTO stack)
    {
        TitleCard("Create a new Card");

        var input = string.Empty;
        var card = new CardDTO();

        input = AnsiConsole.Ask<string>("Input question details (type: r to return):");
        if (input.ToLower() == "r") return;
        card.Question = input;

        input = AnsiConsole.Ask<string>("Input answer details (type: r to return):");
        if (input.ToLower() == "r") return;
        card.Answer = input;

        _cardController.AddCard(card, stack);
    }

    internal void ChooseCard(StackDTO stack)
    {
        TitleCard($"{stack.Name} Cards");

        var menuOptions = OptionUtils.GetAllStringValues(typeof(ChooseCardsOptions));
        var cards = _cardController.ReadAllCards();
        menuOptions.Concat<string>(cards.Keys);

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));

        if (cards.ContainsKey(input))
        {
            EditCard(cards[input]);
        }
    }

    internal void EditCard(CardDTO card)
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard($"Edit Card : {card.Question}");

            var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(OptionUtils.GetAllStringValues(typeof(EditCardOptions))));
            var selectedOption = OptionUtils.GetEnumValue(input, typeof(EditCardOptions));

            switch (selectedOption)
            {
                case EditCardOptions.ChangeQuestion:
                    ChangeCardDetails(card, selectedOption);
                    break;
                case EditCardOptions.ChangeAnswer:
                    ChangeCardDetails(card, selectedOption);
                    break;
                case EditCardOptions.DeleteCard:
                    _cardController.RemoveCard(card);
                    AnsiConsole.WriteLine("Successfully removed card!");
                    AnsiConsole.MarkupLine("Press Enter to Return");
                    endEdit = true;
                    break;
                case EditCardOptions.Return:
                    endEdit = true;
                    break;
            }
        }

    }

    private void ChangeCardDetails(CardDTO card, Enum option)
    {
        TitleCard(OptionUtils.GetStringValue(option));

        var input = AnsiConsole.Ask<string>("Enter details (type: r to return):");

        if (input.ToLower() == "r") return;

        _cardController.EditCard(card, option);
    }
}
