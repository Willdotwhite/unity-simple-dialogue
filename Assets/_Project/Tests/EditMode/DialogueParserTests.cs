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
            Assert.AreEqual(firstLine.Speaker, "test-user");
            Assert.AreEqual(firstLine.Dialogue, "Hello Player");

            record.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual(secondLine.Speaker, "test-user-2");
            Assert.AreEqual(secondLine.Dialogue, "Hello Generic Party Member");

            record.StepToNextDialogueLine();

            SpokenDialogueLine thirdLine = (SpokenDialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual(thirdLine.Speaker, "test-user");
            Assert.AreEqual(thirdLine.Dialogue, "Test dialogue line 3");
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
            Assert.AreEqual(line.Speaker, "test-user");
            Assert.AreEqual(line.Dialogue, "Hello test user");
            Assert.AreEqual(line.Next, "go-to-next-location-from-test");
        }

    }
}