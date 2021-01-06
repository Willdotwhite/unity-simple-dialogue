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
        // close
        // but this is way too nice to let go to waste

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
            SpokenDialogueLine preDialogueLine = (SpokenDialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(preDialogueLine.Dialogue, "This is pre-command firing");

            // Line up command to run
            runner.StepToNextDialogueLine();

            // Step over auto-running command to next line
            runner.StepToNextDialogueLine();
            Assert.IsTrue(actionHasRun);

            runner.StepToNextDialogueLine();
            SpokenDialogueLine postDialogueLine = (SpokenDialogueLine) runner.CurrentDialogueLine;
            Assert.AreEqual(postDialogueLine.Dialogue, "This is post-command firing");
            Assert.IsTrue(runner.CurrentRecord.IsAtEndOfRecord);
        }

        [Test]
        public void CommandDialogueLineLoadsParams()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("CommandLineTest/");

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
            // Line up command to run
            runner.StepToNextDialogueLine();

            // Step over auto-running command to next line
            runner.StepToNextDialogueLine();

            Assert.AreEqual(_params["key1"], "value1");
            Assert.AreEqual(_params["key2"], "value2");
        }

        [Test]
        public void CommandDialogueLineLoadsParamsOfCorrectType()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("CommandLineTest/");

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

            runner.SetCurrentRecord("command-line-test-id-4");
            runner.StepToNextDialogueLine();

            Assert.IsInstanceOf<string>(_params["key1"]);
            Assert.IsInstanceOf<int>(_params["key2"]);
            Assert.IsInstanceOf<bool>(_params["key3"]);
            Assert.IsInstanceOf<float>(_params["key4"]);
        }


        [Test]
        public void CommandDialogueLineFunctionsAsLastLineOfRecord()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("CommandLineTest/");

            CommandParameters _params = new CommandParameters();

            Action<CommandParameters> testAction = delegate(CommandParameters @params)
            {
                // Ugly merge of dictionaries to enable params in one place
                foreach (KeyValuePair<string, object> kvp in @params)
                {
                    _params[kvp.Key] = kvp.Value;
                }
            };

            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>
            {
                {"_test_command_1", testAction},
                {"_test_command_2", testAction}
            };

            DialogueRunner runner = new DialogueRunner(loader.Records, null, commands);

            runner.SetCurrentRecord("command-line-test-id-3");
            runner.StepToNextDialogueLine();

            // _test_command_1
            Assert.AreEqual(_params["key1"], "value1");
            Assert.AreEqual(_params["key2"], "value2");

            // _test_command_2
            Assert.AreEqual(_params["key3"], "value3");
            Assert.AreEqual(_params["key4"], "value4");
        }
    }
}