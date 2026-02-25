using Flashcards.davetn657.Controllers;
using Flashcards.davetn657.Models;
using Flashcards.davetn657.Models.DTOs;
using Flashcards.davetn657.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

namespace Flashcards.davetn657.Views;

public class StartStudySessionView : UserInterface
{
    private readonly StudyController _studyController;
    private readonly CardController _cardController;
    private readonly ScoreController _scoreController;

    public StartStudySessionView(StudyController studyController, CardController cardController, ScoreController scoreController)
    {
        _studyController = studyController;
        _cardController = cardController;
        _scoreController = scoreController;
    }

    internal void ChooseStudySession()
    {
        DisplayWeeklySummary();

        var options = OptionUtils.GetAllStringValues(typeof(ChooseDataOptions));
        var session = _studyController.ReadAllSessions();
        var menuOptions = options.Concat<string>(session.Keys);

        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        if (session.ContainsKey(input))
        {
            GenerateCards(session[input]);
        }
        else return;
    }

    private void GenerateCards(StudyDto session)
    {
        var cards = _cardController.ReadAllCards(session);
        var menuOptions = OptionUtils.GetAllStringValues(typeof(FlashcardOptions));
        var revealedMenuOptions = OptionUtils.GetAllStringValues(typeof(RevealedFlashcardOptions));

        var score = new ScoreDto()
        {
            SessionId = session.Id,
            Score = 0
        };

        if (cards.IsNullOrEmpty())
        {
            AnsiConsole.WriteLine("!!! This Study Session Has No Cards !!!\n!!! Please Add Cards in the Manage Data Options !!!");
            AnsiConsole.Prompt(new TextPrompt<string>("Press Enter to return...").AllowEmpty());
            return;
        }

        while (true)
        {
            TitleCard("Session in Progress...");

            var currentCard = cards.FirstOrDefault().Value;

            var selectedOption = OptionUtils.GetEnumValue(DisplayCard(currentCard.Question, menuOptions), typeof(FlashcardOptions));

            if (selectedOption.Equals(FlashcardOptions.Return)) break;

            TitleCard("The answer is...");

            selectedOption = OptionUtils.GetEnumValue(DisplayCard(currentCard.Answer, revealedMenuOptions), typeof(RevealedFlashcardOptions));

            var timeChange = new DateTime();
            var lastCard = cards.LastOrDefault().Value;

            AnsiConsole.WriteLine("How well did you understand?");
            switch (selectedOption)
            {
                case RevealedFlashcardOptions.StudyAgain:
                    timeChange = lastCard.NextAppearance.AddMinutes(10);
                    break;
                case RevealedFlashcardOptions.Understood:
                    timeChange = lastCard.NextAppearance.AddHours(1);
                    break;
            }

            _cardController.ChangeTime(currentCard, timeChange);
            score.Score++;

            cards.Remove(currentCard.Question);

            if (cards.Count == 0)
            {
                cards = _cardController.ReadAllCards(session);
            }
        }

        _scoreController.AddScore(session, score);

    }

    private string DisplayCard(string cardDetails, List<string> menuOptions)
    {
        var panel = new Panel(cardDetails)
        {
            Border = BoxBorder.Ascii
        };

        AnsiConsole.Write(panel);
        var input = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        return input;
    }

    //Should Display past week session summary
    //should display breakdown of session types
    private void DisplayWeeklySummary()
    {
        var allScoresBar = new BarChart()
            .Label("Study Sessions in the Past Week")
            .LeftAlignLabel();

        var sessionsBreakdown = new BreakdownChart()
            .Compact()
            .ShowPercentage();

        var sessions = new Dictionary<string, int>();
        var dailyScores = new Dictionary<DateTime, int>();
        var totalScore = 0;

        var scores = _scoreController.GetScores(7);
        if(scores.IsNullOrEmpty()) return;

        var colors = new Color[]
        {
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Gold3,
            Color.White,
            Color.Red,
            Color.Violet
        };

        foreach (var score in scores)
        {
            if (sessions.ContainsKey(score.Name))
            {
                sessions[score.Name] += score.Score;
            }
            else
            {
                sessions.Add(score.Name, score.Score);
            }

            if (dailyScores.ContainsKey(score.CreateDate.Date))
            {
                dailyScores[score.CreateDate.Date] += score.Score;
            }
            else
            {
                dailyScores.Add(score.CreateDate.Date, score.Score);
            }
            totalScore += score.Score;
        }

        var colorCount = colors.Count() - 1;
        var randomHexDigit = new Random();

        foreach (var score in dailyScores)
        {
            allScoresBar.AddItem(score.Key.ToString(Globals.DATE_FORMAT), score.Value, colors[colorCount]);
            
            colorCount--;
        }

        foreach(var session in sessions)
        {
            var percentageOfTotalSessions = Math.Round(((float)session.Value / (float)totalScore) * 100, 2);
            
            sessionsBreakdown.AddItem(session.Key, percentageOfTotalSessions, Color.FromInt32(randomHexDigit.Next(1, 250)));
        }

        var dailyScorePanel = new Panel(allScoresBar).Border(BoxBorder.Ascii);

        AnsiConsole.Clear();
        AnsiConsole.Write(dailyScorePanel);
        AnsiConsole.Write(sessionsBreakdown);
    }
}