using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia.Tests
{
    [TestFixture]
    public class CharacterisationTests
    {
        TextWriter defaultOut;

        [SetUp]
        public void Before()
        {
            defaultOut = Console.Out;
        }

        [TearDown]
        public void After()
        {
            Console.SetOut(defaultOut);
        }

        [Test, Explicit]
        public void MasterOutput()
        {
            using (var output = new StreamWriter("master.txt"))
            {
                WriteGameRunsTo(output);
            }
        }

        [Test]
        public void Verify()
        {
            var master = File.ReadAllText("master.txt");

            using (var capture = new StringWriter())
            {
                WriteGameRunsTo(capture);
                Assert.AreEqual(master, capture.ToString());
            }            
        }

        private static void WriteGameRunsTo(TextWriter output)
        {
            Console.SetOut(output);

            for (int i = 0; i < 1000; i++)
            {
                GameRunner.Go(new Random(i));
            }
        }


    }
}
