using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Enums;

public enum MainMenuOptions
{
    [Description("Create a Stack")]CreateStack,
    [Description("Manage Stacks")] ManageStack,
    [Description("Study Sessions")] StudySession
}

