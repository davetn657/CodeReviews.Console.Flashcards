using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class MainView : UserInterface
{
    private readonly StudySessionView _studySessionView;
    private readonly StackView _stackView;

    public MainView(StudySessionView studySessionView, StackView stackView) 
    {
        _studySessionView = studySessionView;
        _stackView = stackView;
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
                    _studySessionView.StudySession();
                    break;
                case MainMenuOptions.ManageStack:
                    _stackView.ManageStack();
                    break;
                case MainMenuOptions.ExitApp:
                    endApp = true;
                    break;
            }
        }
    }
}