using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Place { get; set; }
        public int Coins { get; set; }
        public bool InPenaltyBox { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool IsWinner()
        {
            return !(Coins == 6);
        }
    }

    public class Game
    {
        List<Player> players = new List<Player>();

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
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(new Player
            {
                Name = playerName,
                Place = 0,
                Coins = 0,
                InPenaltyBox = false
            });

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            var player = players[currentPlayer];

            Console.WriteLine(player + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (player.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(player + " is getting out of the penalty box");
                    player.Place += roll;

                    if (player.Place > 11)
                    {
                        player.Place -= 12;
                    }

                    Console.WriteLine(player
                            + "'s new location is "
                            + player.Place);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(player + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                player.Place += roll;
                if (player.Place > 11)
                {
                    player.Place -= 12;
                }

                Console.WriteLine(player
                        + "'s new location is "
                        + player.Place);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

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
            var player = players[currentPlayer];

            if (player.Place == 0) return "Pop";
            if (player.Place == 4) return "Pop";
            if (player.Place == 8) return "Pop";
            if (player.Place == 1) return "Science";
            if (player.Place == 5) return "Science";
            if (player.Place == 9) return "Science";
            if (player.Place == 2) return "Sports";
            if (player.Place == 6) return "Sports";
            if (player.Place == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            var player = players[currentPlayer];
            if (player.InPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    player.Coins++;
                    Console.WriteLine(player
                            + " now has "
                            + player.Coins
                            + " Gold Coins.");

                    bool winner = player.IsWinner();
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

                Console.WriteLine("Answer was corrent!!!!");
                player.Coins++;
                Console.WriteLine(player
                        + " now has "
                        + player.Coins
                        + " Gold Coins.");

                bool winner = player.IsWinner();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            players[currentPlayer].InPenaltyBox = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }
    }

}
