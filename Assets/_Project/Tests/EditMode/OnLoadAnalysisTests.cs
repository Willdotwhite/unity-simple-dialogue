using System;
using _Project.Dialogue;
using NUnit.Framework;

namespace _Project.Tests.EditMode
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