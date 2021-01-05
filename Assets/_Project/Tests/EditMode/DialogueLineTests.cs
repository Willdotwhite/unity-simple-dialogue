﻿using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    // TODO: Refactor into class-based tests
    public class DialogueLineTests
    {
        [Test]
        public void DialogueLineDeserialisesSimpleLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("SingleFileTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("single-file-test-id-1");
            SpokenDialogueLine line = (SpokenDialogueLine) runner.CurrentDialogueLine;

            Assert.AreEqual(line.Speaker, "simple-test-user");
            Assert.AreEqual(line.Dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            OptionsDialogueLine line = (OptionsDialogueLine) runner.CurrentDialogueLine;

            Assert.AreEqual(line.Speaker, "options-test-user");
            Assert.AreEqual(line.Dialogue, "This is an options test");
            Assert.AreEqual(line.Options.Count, 2);

            SpokenDialogueLine option1 = (SpokenDialogueLine) line.Options[0];
            Assert.AreEqual(option1.Speaker, "options-test-user");
            Assert.AreEqual(option1.Dialogue, "Options test option 1");
            Assert.AreEqual(option1.Next, "options-next-1");

            SpokenDialogueLine option2 = (SpokenDialogueLine) line.Options[1];
            Assert.AreEqual(option2.Speaker, "options-test-user");
            Assert.AreEqual(option2.Dialogue, "Options test option 2");
            Assert.AreEqual(option2.Next, "options-next-2");
        }
    }
}