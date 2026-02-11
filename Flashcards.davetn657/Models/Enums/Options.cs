using System.ComponentModel;
using System.Reflection;

namespace Flashcards.davetn657.Models.Enums;

public enum MainMenuOptions
{
    [Description("Start Studying")]StartStudy,
    [Description("Manage Stacks")] ManageStack,
    [Description("Manage Study Sessions")] ManageStudy,
    [Description("Exit")] ExitApp
}

public enum ManageStackOptions
{
    [Description("Create Stack")] CreateStack,
    [Description("Edit Stack")]ChooseStack
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
