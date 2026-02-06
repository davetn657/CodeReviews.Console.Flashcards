using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Enums;

public enum MainMenuOptions
{
    [Description("Manage Stacks")] ManageStack,
    [Description("Study Sessions")] StudySession
}

public enum ManageStackOptions
{
    [Description("Create Stack")] CreateStack,
    [Description("Or Choose a Stack to Edit")]ChooseStack
};