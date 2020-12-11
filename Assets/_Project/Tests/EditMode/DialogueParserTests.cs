﻿using System.Collections.Generic;
using _Project.Dialogue;
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

            DialogueLine firstLine = record.CurrentDialogueLine;
            Assert.AreEqual(firstLine.Speaker, "test-user");
            Assert.AreEqual(firstLine.Dialogue, "Hello Player");

            record.StepToNextDialogueLine();

            DialogueLine secondLine = record.CurrentDialogueLine;
            Assert.AreEqual(secondLine.Speaker, "test-user-2");
            Assert.AreEqual(secondLine.Dialogue, "Hello Generic Party Member");

            record.StepToNextDialogueLine();

            DialogueLine thirdLine = record.CurrentDialogueLine;
            Assert.AreEqual(thirdLine.Speaker, "test-user");
            Assert.AreEqual(thirdLine.Dialogue, "Test dialogue line 3");
        }
    }
}