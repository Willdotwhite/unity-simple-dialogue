using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueParserTests
    {
        [Test]
        public void DialogueParserSimpleReplacements()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("ParserTest/");

            Dictionary<string, string> substitutions = new Dictionary<string, string>
            {
                {"_PLAYER_", "Player"},
                {"_PARTY_MEMBER_", "Generic Party Member"},
            };

            DialogueParser parser = new DialogueParser(substitutions);
            DialogueRunner runner = new DialogueRunner(loader.Records, parser);

            runner.SetCurrentRecord("parser");

            /* Test current record is loaded as expected */
            DialogueRecord record = runner.CurrentRecord;

            SpokenDialogueLine firstLine = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual("test-user", firstLine.Speaker);
            Assert.AreEqual("Hello Player", firstLine.Dialogue);

            record.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual("test-user-2", secondLine.Speaker);
            Assert.AreEqual("Hello Generic Party Member", secondLine.Dialogue);

            record.StepToNextDialogueLine();

            SpokenDialogueLine thirdLine = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual("test-user", thirdLine.Speaker);
            Assert.AreEqual("Test dialogue line 3", thirdLine.Dialogue);
        }

        [Test]
        public void DialogueParserMetaReplacements()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("ParserTest/");

            Dictionary<string, string> substitutions = new Dictionary<string, string>
            {
                {"_NEXT_", "next-location-from-test"},
            };

            DialogueParser parser = new DialogueParser(substitutions, true);
            DialogueRunner runner = new DialogueRunner(loader.Records, parser);

            runner.SetCurrentRecord("parser-meta");

            /* Test current record is loaded as expected */
            DialogueRecord record = runner.CurrentRecord;

            SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual("test-user", line.Speaker);
            Assert.AreEqual("Hello test user", line.Dialogue);
            Assert.AreEqual("go-to-next-location-from-test", line.Next);
        }

    }
}