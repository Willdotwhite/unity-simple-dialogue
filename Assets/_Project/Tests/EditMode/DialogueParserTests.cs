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

            DialogueParser parser = new DialogueParser(DialogueParserType.OnLoad, substitutions);
            DialogueRunner runner = new DialogueRunner(loader.Records, parser);

            runner.SetCurrentRecord("parser");

            /* Test current record is loaded as expected */
            DialogueRecord record = runner.CurrentRecord;

            DialogueLine firstLine = (DialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual(firstLine.speaker, "test-user");
            Assert.AreEqual(firstLine.dialogue, "Hello Player");

            record.StepToNextDialogueLine();

            DialogueLine secondLine = (DialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual(secondLine.speaker, "test-user-2");
            Assert.AreEqual(secondLine.dialogue, "Hello Generic Party Member");

            record.StepToNextDialogueLine();

            DialogueLine thirdLine = (DialogueLine) record.CurrentDialogueLine;
            Assert.AreEqual(thirdLine.speaker, "test-user");
            Assert.AreEqual(thirdLine.dialogue, "Test dialogue line 3");
        }
    }
}