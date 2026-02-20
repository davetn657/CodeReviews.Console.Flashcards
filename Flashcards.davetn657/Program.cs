using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Views;

namespace Flashcards.davetn657;
class Program
{
    public static void Main(string[] args)
    {
        var sessionView = new StudySessionView(new StudyController());
        var stackView = new StackView(new StackController(), new CardView(new CardController()));
        MainView view = new MainView(sessionView, stackView);
        view.StartApp();
    }
}
