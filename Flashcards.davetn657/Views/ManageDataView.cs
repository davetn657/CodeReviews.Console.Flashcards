using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class ManageDataView : UserInterface
{
    private readonly StudyController _studyController;
    private readonly StackController _stackController;
    private readonly CardController _cardController;

    public ManageDataView(StudyController studyController, StackController stackController, CardController cardController)
    {
        _studyController = studyController; 
        _stackController = stackController;
        _cardController = cardController;
    }

    internal void ChooseDataToManage()
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard("Edit Data");

            var menuOptions = OptionUtils.GetAllStringValues(typeof(ManageDataOptions));
            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
            var selectedOption = OptionUtils.GetEnumValue(input, typeof(ManageDataOptions));

            switch (selectedOption)
            {
                case ManageDataOptions.ManageSessions:
                    ManageStudySession();
                    break;
                case ManageDataOptions.ManageStacks:
                    ManageStack();
                    break;
                case ManageDataOptions.Return:
                    endEdit = true;
                    break;
            }
        }
    }

    // STUDY SESSIONS

    private void ManageStudySession()
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard("Study Session");

            var options = OptionUtils.GetAllStringValues(typeof(ManageStudySessionOptions));
            var sessions = _studyController.ReadAllSessions();
            var menuOptions = options.Concat<string>(sessions.Keys);

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

            if (sessions.ContainsKey(input))
            {
                SessionDetails(sessions[input]);
            }
            else
            {
                var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStudySessionOptions));

                switch (optionSelected)
                {
                    case ManageStudySessionOptions.CreateSession:
                        CreateSession();
                        break;
                    case ManageStudySessionOptions.Return:
                        endEdit = true;
                        break;
                }
            }
        }
    }

    private void CreateSession()
    {
        TitleCard("Create a new session");

        var input = AnsiConsole.Ask<string>("Name your session (or type r to cancel):");

        if (input.Equals('r')) return;

        TitleCard("What stack to add?");
        AnsiConsole.WriteLine("Choose which stack to add onto the study session");

        var session = new StudyDTO();
        session.Name = input;

        var options = OptionUtils.GetAllStringValues(typeof(ChooseDataOptions));
        var stackOptions = _stackController.ReadAllStacks();
        var menuOptions = options.Concat<string>(stackOptions.Keys);

        input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        if (stackOptions.ContainsKey(input))
        {
            session.StackId = stackOptions[input].Id;

            _studyController.AddSession(session);
            AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
        }
    }

    private void SessionDetails(StudyDTO session)
    {
        TitleCard(session.Name);

        var stack = _stackController.ReadAllStacks(session);
        var cards = _cardController.ReadAllCards(session);

        var table = new Table();
        table.AddColumn("Stack Id");
        table.AddColumn("Stack");
        table.AddColumn("# of Cards");

        table.AddRow(new string[] { stack.Id.ToString(), stack.Name, cards.Count.ToString() });

        AnsiConsole.Write(table);

        AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
    }

    // STACKS

    private void ManageStack()
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard("Manage Stacks");

            var options = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));
            var stacks = _stackController.ReadAllStacks();
            var menuOptions = options.Concat<string>(stacks.Keys);

            var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

            if (stacks.ContainsKey(input))
            {
                EditStack(stacks[input]);
            }
            else
            {
                var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

                switch (optionSelected)
                {
                    case ManageStackOptions.CreateStack:
                        var name = ChooseName(stacks.Keys);
                        if (name != string.Empty)
                        {
                            _stackController.AddStack(name);
                            AnsiConsole.WriteLine($"Stack named {name} created!\n");
                        }
                        AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
                        break;
                    case ManageStackOptions.Return:
                        endEdit = true;
                        break;
                }
            }
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
                    var stacks = _stackController.ReadAllStacks();
                    var name = ChooseName(stacks.Keys);
                    if (name != string.Empty)
                    {
                        stack.Name = name;
                        _stackController.EditStack(stack);
                        AnsiConsole.WriteLine($"Stack renamed to: {name}\n");
                    }
                    AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
                    break;
                case EditStackOptions.CreateCard:
                    CreateCard(stack);
                    break;
                case EditStackOptions.ChooseCard:
                    ChooseCard(stack);
                    break;
                case EditStackOptions.DeleteStack:
                    _stackController.RemoveStack(stack);
                    AnsiConsole.WriteLine("Successfully removed stack!");
                    AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
                    endEdit = true;
                    break;
                case EditStackOptions.Return:
                    endEdit = true;
                    break;
            }
        }
    }

    // CARDS

    internal void CreateCard(StackDTO stack)
    {
        TitleCard("Create a new Card");

        var input = string.Empty;
        var card = new CardDTO();

        input = AnsiConsole.Ask<string>("Input question details (type: r to cancel):");
        if (input.ToLower() == "r") return;
        card.Question = input;

        input = AnsiConsole.Ask<string>("Input answer details (type: r to cancel):");
        if (input.ToLower() == "r") return;
        card.Answer = input;

        _cardController.AddCard(card, stack);
    }

    internal void ChooseCard(StackDTO stack)
    {
        var endEdit = false;

        while (!endEdit)
        {
            TitleCard($"{stack.Name} Cards");

            var options = OptionUtils.GetAllStringValues(typeof(ChooseDataOptions));
            var cards = _cardController.ReadAllCards();
            var menuOptions = options.Concat<string>(cards.Keys);

            var input = AnsiConsole.Prompt<string>(new SelectionPrompt<string>().AddChoices(menuOptions));

            if (cards.ContainsKey(input))
            {
                EditCard(cards[input]);
            }
            else
            {
                endEdit = true; 
            }
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
                    AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
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

        switch (option)
        {
            case EditCardOptions.ChangeQuestion:
                card.Question = input; 
                break;
            case EditCardOptions.ChangeAnswer:
                card.Answer = input;
                break;
        }

        _cardController.EditCard(card, option);
    }
}