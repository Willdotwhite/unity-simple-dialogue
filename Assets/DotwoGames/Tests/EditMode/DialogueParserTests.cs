using System.Collections.Generic;
using DotwoGames.Dialogue.Config;
using DotwoGames.Dialogue.Lines;
using DotwoGames.Dialogue;
using NUnit.Framework;

namespace DotwoGames.Tests.EditMode
{
    public class DialogueParserTests
    {
        [Test]
        public void DialogueParserSimpleReplacements()
        {
            Dictionary<string, string> substitutions = new Dictionary<string, string>
            {
                {"_PLAYER_", "Player"},
                {"_PARTY_MEMBER_", "Generic Party Member"},
            };

            DialogueParser parser = new DialogueParser(substitutions);
            DialogueSystem system = new DialogueSystem("ParserTest/", "parser", parser);

            Assert.AreEqual("test-user", system.CurrentDialogueLine.Speaker);
            Assert.AreEqual("Hello Player", system.CurrentDialogueLine.Dialogue);

            system.StepToNextDialogueLine();

            Assert.AreEqual("test-user-2", system.CurrentDialogueLine.Speaker);
            Assert.AreEqual("Hello Generic Party Member", system.CurrentDialogueLine.Dialogue);

            system.StepToNextDialogueLine();

            Assert.AreEqual("test-user", system.CurrentDialogueLine.Speaker);
            Assert.AreEqual("Test dialogue line 3", system.CurrentDialogueLine.Dialogue);
        }

        [Test]
        public void DialogueParserMetaReplacements()
        {
            Dictionary<string, string> substitutions = new Dictionary<string, string>
            {
                {"_NEXT_", "next-location-from-test"},
            };

            DialogueParser parser = new DialogueParser(substitutions, true);
            DialogueSystem system = new DialogueSystem("ParserTest/", "parser-meta", parser);

            Assert.AreEqual("test-user", system.CurrentDialogueLine.Speaker);
            Assert.AreEqual("Hello test user", system.CurrentDialogueLine.Dialogue);
            Assert.AreEqual("go-to-next-location-from-test", system.CurrentDialogueLine.Next);
        }

    }
}