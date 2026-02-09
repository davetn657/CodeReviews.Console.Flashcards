using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Enums;

public enum MainMenuOptions
{
    [Description("Manage Stacks")] ManageStack,
    [Description("Study Sessions")] StudySession,
    [Description("Exit")] ExitApp
}

public enum ManageStackOptions
{
    [Description("Create Stack")] CreateStack,
    [Description("Choose a Stack to Edit")]ChooseStack
};