using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        private bool IsEven(int roll)
        {
            return roll % 2 != 0;
        }

        private bool IsCurrentPlayerInPenaltyBox()
        {
            return inPenaltyBox[currentPlayer];
        }

        public void roll(int roll)
        {
            ReportPlayerStatus(roll);

            if (IsCurrentPlayerInPenaltyBox())
            {
                if (IsEven(roll))
                {
                    isGettingOutOfPenaltyBox = true;
                    ReportPlayerGettingOutOfPenaltyBox();

                    PlayersTurn(roll);
                }
                else
                {
                    isGettingOutOfPenaltyBox = false;
                    ReportPlayerGettingOutOfPenaltyBox();
                }

            }
            else
            {

                PlayersTurn(roll);
            }

        }

        private void PlayersTurn(int roll)
        {
            MovePlayer(roll);

            ReportPlayerLocation();
            ReportCurrentCategory();
            askQuestion();
        }

        private void ReportPlayerStatus(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }

        private void ReportPlayerGettingOutOfPenaltyBox()
        {
            if (isGettingOutOfPenaltyBox)
                Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
            else
                Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
        }

        private void ReportCurrentCategory()
        {
            Console.WriteLine("The category is " + currentCategory());
        }

        private void ReportPlayerLocation()
        {
            Console.WriteLine(players[currentPlayer]
                    + "'s new location is "
                    + places[currentPlayer]);
        }

        private void MovePlayer(int roll)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            if (HasPlayerReachedEndOfBoard()) places[currentPlayer] = places[currentPlayer] - 12;
        }

        private bool HasPlayerReachedEndOfBoard()
        {
            return places[currentPlayer] > 11;
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        private String currentCategory()
        {
            if (places[currentPlayer] == 0 || places[currentPlayer] == 4 || places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1 || places[currentPlayer] == 5 || places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2 || places[currentPlayer] == 6 || places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    return ProcessCorrectAnswer();
                }
                else
                {
                    NextPlayer();
                    return true;
                }
            }
            else
            {
                return ProcessCorrectAnswer();
            }
        }

        private bool ProcessCorrectAnswer()
        {
            ReportCorrectAnswer();
            purses[currentPlayer]++;
            ReportPlayerScore();

            bool winner = didPlayerWin();
            NextPlayer();

            return winner;
        }

        private void ReportPlayerScore()
        {
            Console.WriteLine(players[currentPlayer]
                    + " now has "
                    + purses[currentPlayer]
                    + " Gold Coins.");
        }

        private static void ReportCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
        }

        private void NextPlayer()
        {
            currentPlayer++;
            if (IsLastPlayer()) currentPlayer = 0;
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            NextPlayer();
            return true;
        }

        private bool IsLastPlayer()
        {
            return currentPlayer == players.Count;
        }

        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }
}
