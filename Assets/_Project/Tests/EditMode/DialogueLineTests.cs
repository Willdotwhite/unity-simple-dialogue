using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;
using UnityEngine.Events;

namespace _Project.Tests.EditMode
{
    public class DialogueLineTests
    {
        [Test]
        public void DialogueLineDeserialisesSimpleLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("SingleFileTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("single-file-test-id-1");
            DialogueLine line = (DialogueLine) runner.CurrentDialogueLine;

            Assert.AreEqual(line.speaker, "simple-test-user");
            Assert.AreEqual(line.dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            DialogueOptionLine line = (DialogueOptionLine) runner.CurrentDialogueLine;

            Assert.AreEqual(line.speaker, "options-test-user");
            Assert.AreEqual(line.dialogue, "This is an options test");
            Assert.AreEqual(line.options.Count, 2);

            DialogueLine option1 = (DialogueLine) line.options[0];
            Assert.AreEqual(option1.speaker, "options-test-user");
            Assert.AreEqual(option1.dialogue, "Options test option 1");
            Assert.AreEqual(option1.next, "options-next-1");

            DialogueLine option2 = (DialogueLine) line.options[1];
            Assert.AreEqual(option2.speaker, "options-test-user");
            Assert.AreEqual(option2.dialogue, "Options test option 2");
            Assert.AreEqual(option2.next, "options-next-2");
        }

        [Test]
        public void DialogueLineRunsCommands()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("CommandLineTest/");
            bool eventWasFired = false;
            UnityEvent testEvent = new UnityEvent();
            testEvent.AddListener(() => { eventWasFired = true;});

            Dictionary<string, UnityEvent> commands = new Dictionary<string, UnityEvent>()
            {
                {"_test_command", testEvent}
            };

            DialogueRunner runner = new DialogueRunner(loader.Records, null, commands);

            runner.SetCurrentRecord("command-line-test-id-1");
            DialogueLine preDialogueLine = (DialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(preDialogueLine.dialogue, "This is pre-command firing");

            runner.StepToNextDialogueLine();
            Assert.IsTrue(eventWasFired);

            runner.StepToNextDialogueLine();
            DialogueLine postDialogueLine = (DialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(postDialogueLine.dialogue, "This is post-command firing");
        }
    }
}