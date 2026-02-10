using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Models.Enums;

public enum MainMenuOptions
{
    [Description("Manage Stacks")] ManageStack,
    [Description("Study Sessions")] StudySession,
    [Description("Exit")] ExitApp
}

public enum ManageStackOptions
{
    [Description("Create Stack")] CreateStack,
    [Description("Edit Stack")]ChooseStack
};

public enum EditStackOptions
{
    [Description("Rename Stack")]RenameStack,
    [Description("Edit Card")]ChooseCard,
    [Description("Delete Stack")]DeleteStack,
    [Description("Return")]Return
}