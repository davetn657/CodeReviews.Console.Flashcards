using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class StudySessionView : UserInterface
{
    private readonly StudyController _studyController;

    public StudySessionView(StudyController studyController)
    {
        _studyController = studyController;
    }

    internal void StudySession()
    {
        TitleCard("Study Session");

        var options = OptionUtils.GetAllStringValues(typeof(ManageStudySessionOptions));
        var sessions = _studyController.ReadAllSessions();
        var menuOptions = options.Concat<string>(sessions.Keys);

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        if (sessions.ContainsKey(input))
        {
            StartOrEditSession(sessions[input]);
        }
        else
        {
            var optionSelected = OptionUtils.GetEnumValue(input, typeof(ManageStudySessionOptions));

            switch (optionSelected)
            {
                case ManageStudySessionOptions.CreateSession:
                    var name = ChooseName(sessions.Keys);
                    if (!string.IsNullOrEmpty(name))
                    {
                        _studyController.AddSession(name);
                        AnsiConsole.WriteLine($"Study session named {name} created!\n");
                    }
                    AnsiConsole.MarkupLine("Press Enter to Return");
                    break;
                case ManageStudySessionOptions.Return:
                    break;
            }
        }
    }

    private void StartOrEditSession(StudyDTO session)
    {
        TitleCard(session.Name);

        var menuOptions = OptionUtils.GetAllStringValues(typeof(StudySessionOptions));

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var optionSelected = OptionUtils.GetEnumValue(input, typeof(StudySessionOptions));

        switch (optionSelected)
        {
            case StudySessionOptions.StartSession:
                break;
            case StudySessionOptions.RenameSession:
                var sessions = _studyController.ReadAllSessions();
                var name = ChooseName(sessions.Keys);
                if(name != string.Empty)
                {
                    _studyController.AddSession(name);
                    AnsiConsole.WriteLine($"Study session renamed {name}!\n");
                }
                AnsiConsole.MarkupLine("Press Enter to Return");
                break;
            case StudySessionOptions.Return:
                break;
        }
    }
}