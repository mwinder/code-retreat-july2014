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

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (IsWrongAnswer(rand))
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);
        }

        private static bool IsWrongAnswer(Random rand)
        {
            return rand.Next(9) == 7;
        }


    }

}

