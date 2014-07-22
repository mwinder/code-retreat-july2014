using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        public static void Main()
        {
            Go(new Random());
        }

        private static bool notAWinner;

        public static void Go(Random random)
        {
            Random rand = random;
            Game aGame = new Game();

            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

            do
            {

                aGame.RollDice(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    notAWinner = aGame.CorrectAnswer();
                }



            } while (notAWinner);
        }


    }

}

