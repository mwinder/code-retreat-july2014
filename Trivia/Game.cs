using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> players = new List<string>();

        int[] boardPosition = new int[6];
        int[] goldCoins = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isCurrentPlayerGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool AddPlayer(String playerName)
        {


            players.Add(playerName);
            boardPosition[HowManyPlayers()] = 0;
            goldCoins[HowManyPlayers()] = 0;
            inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void RollDice(int numberRolled)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + numberRolled);

            if (inPenaltyBox[currentPlayer])
            {
                if (numberRolled % 2 != 0)
                {
                    isCurrentPlayerGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    boardPosition[currentPlayer] = boardPosition[currentPlayer] + numberRolled;
                    if (boardPosition[currentPlayer] > 11) boardPosition[currentPlayer] = boardPosition[currentPlayer] - 12;

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + boardPosition[currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskNextQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isCurrentPlayerGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                boardPosition[currentPlayer] = boardPosition[currentPlayer] + numberRolled;
                if (boardPosition[currentPlayer] > 11) boardPosition[currentPlayer] = boardPosition[currentPlayer] - 12;

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + boardPosition[currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskNextQuestion();
            }

        }

        private void AskNextQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String CurrentCategory()
        {
            if (boardPosition[currentPlayer] == 0) return "Pop";
            if (boardPosition[currentPlayer] == 4) return "Pop";
            if (boardPosition[currentPlayer] == 8) return "Pop";
            if (boardPosition[currentPlayer] == 1) return "Science";
            if (boardPosition[currentPlayer] == 5) return "Science";
            if (boardPosition[currentPlayer] == 9) return "Science";
            if (boardPosition[currentPlayer] == 2) return "Sports";
            if (boardPosition[currentPlayer] == 6) return "Sports";
            if (boardPosition[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool CorrectAnswer()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isCurrentPlayerGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    goldCoins[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + goldCoins[currentPlayer]
                            + " Gold Coins.");

                    bool winner = DidCurrentPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was correct!!!!");
                goldCoins[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + goldCoins[currentPlayer]
                        + " Gold Coins.");

                bool winner = DidCurrentPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool DidCurrentPlayerWin()
        {
            return !(goldCoins[currentPlayer] == 6);
        }
    }

}
