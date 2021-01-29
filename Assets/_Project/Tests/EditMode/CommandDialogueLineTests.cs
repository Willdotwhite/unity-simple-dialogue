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
            bool actionHasRun = false;
            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>
            {
                {"_test_command", delegate { actionHasRun = true; }}
            };

            DialogueSystem system = new DialogueSystem("CommandLineTest", "command-line-test-id-1", null, commands);
            Assert.AreEqual("This is pre-command firing", system.CurrentDialogueLine.Dialogue);

            // Line up command to run
            system.StepToNextDialogueLine();

            // Step over auto-running command to next line
            system.StepToNextDialogueLine();
            Assert.IsTrue(actionHasRun);

            system.StepToNextDialogueLine();
            Assert.AreEqual("This is post-command firing", system.CurrentDialogueLine.Dialogue);
            Assert.IsTrue(system.IsAtEndOfDialogue);
        }

        [Test]
        public void CommandDialogueLineLoadsParams()
        {
            CommandParameters _params = new CommandParameters();
            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>
            {
                {"_test_command", delegate(CommandParameters @params) {_params = @params;}}
            };

            DialogueSystem system = new DialogueSystem("CommandLineTest/", "command-line-test-id-2", null, commands);

            // Line up command to run
            system.StepToNextDialogueLine();

            // Step over auto-running command to next line
            system.StepToNextDialogueLine();

            Assert.AreEqual("value1", _params["key1"]);
            Assert.AreEqual("value2", _params["key2"]);
        }

        [Test]
        public void CommandDialogueLineLoadsParamsOfCorrectType()
        {
            CommandParameters _params = new CommandParameters();
            Dictionary<string, Action<CommandParameters>> commands = new Dictionary<string, Action<CommandParameters>>
            {
                {"_test_command", delegate(CommandParameters @params) {_params = @params;}}
            };

            DialogueSystem system = new DialogueSystem("CommandLineTest", "command-line-test-id-4", null, commands);
            system.StepToNextDialogueLine();

            Assert.IsInstanceOf<string>(_params["key1"]);
            Assert.IsInstanceOf<int>(_params["key2"]);
            Assert.IsInstanceOf<bool>(_params["key3"]);
            Assert.IsInstanceOf<float>(_params["key4"]);
        }


        [Test]
        public void CommandDialogueLineFunctionsAsLastLineOfRecord()
        {
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

            DialogueSystem system = new DialogueSystem("CommandLineTest/", "command-line-test-id-3", null, commands);
            system.StepToNextDialogueLine();

            // _test_command_1
            Assert.AreEqual("value1", _params["key1"]);
            Assert.AreEqual("value2", _params["key2"]);

            // _test_command_2
            Assert.AreEqual("value3", _params["key3"]);
            Assert.AreEqual("value4", _params["key4"]);
        }

        [Test]
        public void DialogueRunnerThrowsExceptionWhenRecordHasNoCommandsSet()
        {
            DialogueSystem system = new DialogueSystem("CommandLineTest/", "command-line-test-id-3");
            Assert.Throws<ArgumentOutOfRangeException>(() => system.StepToNextDialogueLine());
        }

        [Test]
        public void DialogueRunnerThrowsExceptionWhenRecordDoesNotHaveIntendedCommandSet()
        {
            DialogueSystem runner = new DialogueSystem("CommandLineTest/", "command-line-test-id-3", null, new Dictionary<string, Action<CommandParameters>>());
            Assert.Throws<ArgumentOutOfRangeException>(() => runner.StepToNextDialogueLine());
        }
    }
}