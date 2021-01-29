using System;
using DotwoGames.Dialogue;
using NUnit.Framework;

namespace DotwoGames.Tests.EditMode
{
    public class OnLoadAnalysisTests
    {
        [Test]
        public void OnLoadAnalysisReportsLinesThatCannotBeReached()
        {
            // Exception type?
            Assert.Throws<FormatException>(
                () => new DialogueSystem("AnalysisTest", "orphaned-line-test"),
                "orphaned-line-test contains dialogue lines that cannot be reached"
            );
        }
    }
}