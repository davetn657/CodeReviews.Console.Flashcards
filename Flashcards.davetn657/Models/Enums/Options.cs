using System.ComponentModel;
namespace Flashcards.davetn657.Models.Enums;

public enum MainMenuOptions
{
    [Description("Start studying")]StartStudy,
    [Description("Manage data")] ManageData,
    [Description("Exit")] ExitApp
}

public enum ManageDataOptions
{
    [Description("Return")] Return,
    [Description("Manage study sessions")]ManageSessions,
    [Description("Manage stacks")]ManageStacks
}

public enum ManageStudySessionOptions
{
    [Description("Return")] Return,
    [Description("Create a new session")]CreateSession
}

public enum ManageStackOptions
{
    [Description("Return")] Return,
    [Description("Create stack")] CreateStack
}

public enum EditStackOptions
{
    [Description("Return")] Return,
    [Description("Rename stack")]RenameStack,
    [Description("Add a card")]CreateCard,
    [Description("Edit card")]ChooseCard,
    [Description("Delete stack")]DeleteStack
}

public enum EditCardOptions
{
    [Description("Return")] Return,
    [Description("Change question")]ChangeQuestion,
    [Description("Change answer")]ChangeAnswer,
    [Description("Delete card")]DeleteCard
}

public enum ChooseDataOptions
{
    [Description("Return")]Return
}

public enum FlashcardOptions
{
    [Description("Reveal card")]Reveal,
    [Description("Return")]Return
}

public enum RevealedFlashcardOptions
{
    [Description("Need to study more")]StudyAgain,
    [Description("I understood it well")]Understood,
}