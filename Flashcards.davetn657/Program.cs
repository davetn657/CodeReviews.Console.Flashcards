using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Views;

namespace Flashcards.davetn657;
class Program
{
    public static void Main(string[] args)
    {
        var studyController = new StudyController();
        var stackController = new StackController();
        var cardController = new CardController();
        var scoreController = new ScoreController();

        var manageDataview = new ManageDataView(studyController, stackController, cardController);
        var startStudySession = new StartStudySessionView(studyController, cardController, scoreController);
        MainView view = new MainView(manageDataview, startStudySession);
        view.StartApp();
    }
}
