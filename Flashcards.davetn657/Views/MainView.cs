using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class MainView : UserInterface
{
    private readonly ManageDataView _manageDataView;
    private readonly StartStudySessionView _startStudySession;

    public MainView(ManageDataView manageDataView, StartStudySessionView startStudySession) 
    {
        _manageDataView = manageDataView;
        _startStudySession = startStudySession;
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
                    _startStudySession.ChooseStudySession();
                    break;
                case MainMenuOptions.ManageData:
                    _manageDataView.ChooseDataToManage();
                    break;
                case MainMenuOptions.ExitApp:
                    endApp = true;
                    break;
            }
        }
    }
}