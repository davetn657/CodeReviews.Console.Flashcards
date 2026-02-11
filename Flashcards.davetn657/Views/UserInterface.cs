using Spectre.Console;
using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.Enums;
using Flashcards.davetn657.Models.DTOs;

namespace Flashcards.davetn657.Views;
public class UserInterface
{
    private StackController stackController;
    private CardController cardController;
    
    public UserInterface()
    {
        stackController = new StackController();
        cardController = new CardController();
    }

    public void StartApp()
    {
        var endApp = false;

        while (!endApp)
        {
            TitleCard("Main Menu");

            var menuOptions = OptionUtils.GetAllStringValues(typeof(MainMenuOptions));

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
            var inputValue = OptionUtils.GetEnumValue(input, typeof(MainMenuOptions));


            switch (inputValue)
            {
                case MainMenuOptions.StartStudy:
                    break;
                case MainMenuOptions.ManageStack:
                    ManageStack();
                    break;
                case MainMenuOptions.ManageStudy:
                    break;
                case MainMenuOptions.ExitApp:
                    endApp = true;
                    break;
            }
        }
    }

    private void ManageStack()
    {
        TitleCard("Manage Stacks");

        var menuOptions = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

        switch (optionSelected)
        {
            case ManageStackOptions.CreateStack:
                stackController.AddStack(NameStack());
                AnsiConsole.WriteLine($"Stack named {input} created!\n");
                AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices("Press Enter to Return"));
                break;
            case ManageStackOptions.ChooseStack:
                ChooseStack();
                break;
        }
    }

    

    private void ChooseStack()
    {
        TitleCard("Edit a Stack");

        var stacks = stackController.ReadAllStacks();

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(stacks.Keys).AddChoices("Return") );

        switch (input)
        {
            case "Return":
                break;
            default:
                EditStack(stacks[input]);
                break;
        }
    }

    private void EditStack(StackDTO stack)
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard($"Edit : {stack.Name}");

            var menuOptions = OptionUtils.GetAllStringValues(typeof(EditStackOptions));

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
            var optionSelected = OptionUtils.GetEnumValue(input, typeof(EditStackOptions));

            switch (optionSelected)
            {
                case EditStackOptions.RenameStack:
                    var name = NameStack();
                    if(name != string.Empty) stackController.EditStack(stack, name);
                    break;
                case EditStackOptions.CreateCard:
                    CreateCard(stack);
                    break;
                case EditStackOptions.ChooseCard:
                    ChooseCard(stack);
                    break;
                case EditStackOptions.DeleteStack:
                    stackController.RemoveStack(stack);
                    AnsiConsole.WriteLine("Successfully removed stack!");
                    AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices("Return?"));
                    endEdit = true;
                    break;
                case EditStackOptions.Return:
                    endEdit = true;
                    break;
            }
        }
    }

    private string NameStack()
    {
        TitleCard("Name your Stack");

        var input = string.Empty;
        var cards = stackController.ReadAllStacks();

        while (true)
        {
            input = AnsiConsole.Ask<string>("Name your stack (type: r to return):");

            if (!cards.ContainsKey(input) && input != "Return")
            {
                return input;
            }
            else if(input.ToLower() == "r") 
            {
                return string.Empty;
            }
            else
            {
                AnsiConsole.Markup("[red]Stack Name Already in Use or Not Available Choose a Different Name![/]\n");
            }
        }
    }

    private void CreateCard(StackDTO stack)
    {
        TitleCard("Create a new Card");

        var input = string.Empty;
        var cards = cardController.ReadAllCards();
        var card = new CardDTO();

        input = AnsiConsole.Ask<string>("Input question details (type: r to return):");
        if (input.ToLower() == "r") return;
        card.Question = input;

        input = AnsiConsole.Ask<string>("Input answer details (type: r to return):");
        if (input.ToLower() == "r") return;
        card.Answer = input;

        cardController.AddCard(card, stack);
    }

    private void ChooseCard(StackDTO stack)
    {
        TitleCard($"{stack.Name} Cards");

        var allCards = cardController.ReadAllCards();

        var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(allCards.Keys).AddChoices("Return"));

        switch (input)
        {
            case "Return":
                break;
            default:
                EditCard(allCards[input]);
                break;
        }
    }

    private void EditCard(CardDTO card)
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
                    cardController.RemoveCard(card);
                    AnsiConsole.WriteLine("Successfully removed card!");
                    AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices("Return?"));
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

        cardController.EditCard(card, option);
    }

    private void StudySession()
    {
        TitleCard("Study Session");
    }

    private void ManageStudySessions()
    {

    }

    private void TitleCard(string title)
    {
        var titleFiglet = new FigletText(title);

        var centered = Align.Center(titleFiglet);

        AnsiConsole.Clear();
        AnsiConsole.Write(centered);
    }

    
}

