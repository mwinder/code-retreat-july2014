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

            Assert.That(game.add("Player"), Is.True);
        }

        [Test]
        public void NewGameHasNoPlayers()
        {
            var game = new Game();

            Assert.That(game.howManyPlayers(), Is.EqualTo(0));
        }

        [Test]
        public void ReturnsNumberOfPlayers()
        {
            var game = new Game();

            game.add("Player 1");

            Assert.That(game.howManyPlayers(), Is.EqualTo(1));
        }

        [Test]
        public void CanCreateRockQuestion()
        {
            var game = new Game();

            Assert.That(game.createRockQuestion(23), Is.EqualTo("Rock Question 23"));

        }

        [Test]
        public void GameIsNotPlayableWithNoPlayers()
        {
            var game = new Game();

            Assert.That(game.isPlayable(), Is.False);
        }

        [Test]
        public void GameIsNotPlayableWith1Player()
        {
            var game = new Game();
            game.add("Player 1");
            Assert.That(game.isPlayable(), Is.False);
        }

        [Test]
        public void GameIsPlayableWith2Players()
        {
            var game = new Game();

            game.add("Player 1");
            game.add("Player 2");

            Assert.That(game.isPlayable(), Is.True);
        }

    }
}
