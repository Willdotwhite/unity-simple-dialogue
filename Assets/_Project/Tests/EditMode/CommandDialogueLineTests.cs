using System;
using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class CommandDialogueLineTests
    {
        [Test]
        public void CommandDialogueLineRuns()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("CommandLineTest/");

            bool actionHasRun = false;

            Action<CommandParameters> testAction = delegate { actionHasRun = true; };

            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>()
            {
                {"_test_command", testAction}
            };

            DialogueRunner runner = new DialogueRunner(loader.Records, null, commands);

            runner.SetCurrentRecord("command-line-test-id-1");
            DialogueLine preDialogueLine = (DialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(preDialogueLine.Dialogue, "This is pre-command firing");

            runner.StepToNextDialogueLine();
            Assert.IsTrue(actionHasRun);

            runner.StepToNextDialogueLine();
            DialogueLine postDialogueLine = (DialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(postDialogueLine.Dialogue, "This is post-command firing");
        }

        [Test]
        public void CommandDialogueLineLoadsParams()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("Bucket/");

            CommandParameters _params = new CommandParameters();

            Action<CommandParameters> testAction = delegate(CommandParameters @params)
            {
                _params = @params;
            };

            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>
            {
                {"_test_command", testAction}
            };

            DialogueRunner runner = new DialogueRunner(loader.Records, null, commands);

            runner.SetCurrentRecord("command-line-test-id-2");
            runner.StepToNextDialogueLine();

            Assert.AreEqual(_params["key1"], "value1");
            Assert.AreEqual(_params["key2"], "value2");
        }
    }
}