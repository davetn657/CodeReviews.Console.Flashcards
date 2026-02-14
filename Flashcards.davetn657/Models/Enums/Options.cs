using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Models.Enums;

public enum MainMenuOptions
{
    [Description("Start Studying")]StartStudy,
    [Description("Manage Study Sessions")] ManageStudy,
    [Description("Manage Stacks")] ManageStack,
    [Description("Exit")] ExitApp
}

public enum ManageStudySessionOptions
{
    [Description("Create a Study Session")]CreateSeession,
    [Description("Edit Study Session")] ChooseSession,
    [Description("Return")] Return
}

public enum ManageStackOptions
{
    [Description("Create Stack")] CreateStack,
    [Description("Edit Stack")]ChooseStack,
    [Description("Return")] Return
}

public enum EditStackOptions
{
    [Description("Rename Stack")]RenameStack,
    [Description("Add a Card")]CreateCard,
    [Description("Edit Card")]ChooseCard,
    [Description("Delete Stack")]DeleteStack,
    [Description("Return")]Return
}

public enum EditCardOptions 
{
    [Description("Change Question")]ChangeQuestion,
    [Description("Change Answer")]ChangeAnswer,
    [Description("Delete Card")]DeleteCard,
    [Description("Return")]Return
}
