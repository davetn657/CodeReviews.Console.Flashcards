using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class StackView : UserInterface
{
    private readonly StackController _stackController;
    private readonly CardView _cardView;

    public StackView(StackController stackController, CardView cardView)
    {
        _stackController = stackController;
        _cardView = cardView;
    }

    internal void ManageStack()
    {
        TitleCard("Manage Stacks");

        var options = OptionUtils.GetAllStringValues(typeof(ManageStackOptions));
        var stacks = _stackController.ReadAllStacks();
        var menuOptions = options.Concat(stacks.Keys);

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStackOptions));

        if (stacks.ContainsKey(input))
        {
            EditStack(stacks[input]);
        }
        else
        {
            switch (optionSelected)
            {
                case ManageStackOptions.CreateStack:
                    var name = ChooseName(stacks.Keys);
                    if (name != string.Empty)
                    {
                        _stackController.AddStack(name);
                        AnsiConsole.WriteLine($"Stack named {name} created!\n");
                    }
                    AnsiConsole.MarkupLine("Press Enter to Return");
                    break;
                case ManageStackOptions.Return:
                    break;
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
                    AnsiConsole.MarkupLine("Press Enter to Return");
                    break;
                case EditStackOptions.CreateCard:
                    _cardView.CreateCard(stack);
                    break;
                case EditStackOptions.ChooseCard:
                    _cardView.ChooseCard(stack);
                    break;
                case EditStackOptions.DeleteStack:
                    _stackController.RemoveStack(stack);
                    AnsiConsole.WriteLine("Successfully removed stack!");
                    AnsiConsole.MarkupLine("Press Enter to Return");
                    endEdit = true;
                    break;
                case EditStackOptions.Return:
                    endEdit = true;
                    break;
            }
        }
    }
}