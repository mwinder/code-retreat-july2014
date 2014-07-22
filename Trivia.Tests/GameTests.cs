using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyTrivia;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void CanAddPlayer()
        {
            var game = new Game();

            Assert.That(game.AddPlayer("Player"), Is.True);
        }

        [Test]
        public void NewGameHasNoPlayers()
        {
            var game = new Game();

            Assert.That(game.HowManyPlayers(), Is.EqualTo(0));
        }

        [Test]
        public void ReturnsNumberOfPlayers()
        {
            var game = new Game();

            game.AddPlayer("Player 1");

            Assert.That(game.HowManyPlayers(), Is.EqualTo(1));
        }

        [Test]
        public void CanCreateRockQuestion()
        {
            var game = new Game();

            Assert.That(game.CreateRockQuestion(23), Is.EqualTo("Rock Question 23"));

        }

        [Test]
        public void GameIsNotPlayableWithNoPlayers()
        {
            var game = new Game();

            Assert.That(game.IsPlayable(), Is.False);
        }

        [Test]
        public void GameIsNotPlayableWith1Player()
        {
            var game = new Game();
            game.AddPlayer("Player 1");
            Assert.That(game.IsPlayable(), Is.False);
        }

        [Test]
        public void GameIsPlayableWith2Players()
        {
            var game = new Game();

            game.AddPlayer("Player 1");
            game.AddPlayer("Player 2");

            Assert.That(game.IsPlayable(), Is.True);
        }

    }
}
