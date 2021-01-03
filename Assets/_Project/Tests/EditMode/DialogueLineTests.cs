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

            Assert.AreEqual(line.Speaker, "simple-test-user");
            Assert.AreEqual(line.Dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            DialogueOptionLine line = (DialogueOptionLine) runner.CurrentDialogueLine;

            Assert.AreEqual(line.Speaker, "options-test-user");
            Assert.AreEqual(line.Dialogue, "This is an options test");
            Assert.AreEqual(line.Options.Count, 2);

            DialogueLine option1 = (DialogueLine) line.Options[0];
            Assert.AreEqual(option1.Speaker, "options-test-user");
            Assert.AreEqual(option1.Dialogue, "Options test option 1");
            Assert.AreEqual(option1.Next, "options-next-1");

            DialogueLine option2 = (DialogueLine) line.Options[1];
            Assert.AreEqual(option2.Speaker, "options-test-user");
            Assert.AreEqual(option2.Dialogue, "Options test option 2");
            Assert.AreEqual(option2.Next, "options-next-2");
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
            Assert.AreEqual(preDialogueLine.Dialogue, "This is pre-command firing");

            runner.StepToNextDialogueLine();
            Assert.IsTrue(eventWasFired);

            runner.StepToNextDialogueLine();
            DialogueLine postDialogueLine = (DialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(postDialogueLine.Dialogue, "This is post-command firing");
        }
    }
}