using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using _Project.Tests.EditMode.Mocks;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueRunnerTests
    {
        [Test]
        public void DialogueRunnerStepsThroughAllLinesInRecord()
        {
            DialogueRunner runner = DialogueRunnerMocks.GetSimple();

            for (int i = 1; i <= 3; i++)
            {
                DialogueRecord record = runner.CurrentRecord;
                SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;

                Assert.AreEqual($"speaker-{i}", line.Speaker);
                Assert.AreEqual($"Test line {i}", line.Dialogue);
                runner.StepToNextDialogueLine();
            }
        }

        [Test]
        public void DialogueRunnerWalksToNextRecordOnNext()
        {

            const string startId = "multi-record-start";
            const string endId = "multi-record-end";
            DialogueRunner runner = DialogueRunnerMocks.GetMultiRecordByNext(new List<string> {startId, endId});

            foreach (string id in new[] {startId, endId}) {
                for (int i = 1; i <= 3; i++)
                {
                    DialogueRecord record = runner.CurrentRecord;
                    Assert.AreEqual(id, record.id);

                    // Assert the public Property of an internal int:idx field is reset
                    if (i == 1)
                    {
                        Assert.False(record.IsAtEndOfRecord);
                    }

                    SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;
                    Assert.AreEqual($"speaker-{i}", line.Speaker);
                    Assert.AreEqual($"Test line {i}", line.Dialogue);
                    runner.StepToNextDialogueLine();
                }
            }
        }

        [Test]
        public void DialogueRunnerStepsThroughChosenDialogueLineInOption()
        {
            const string startId = "option-record-start";
            DialogueRecord startRecord = SimpleDialogueMocks.GetSimpleOption(startId, 4);

            const string endId = "options-record-end";
            DialogueRecord endRecord = SimpleDialogueMocks.GetSimple(endId, 5);

            // Wire up startRecord to endRecord
            ((OptionsDialogueLine) startRecord.dialogueLines[startRecord.dialogueLines.Count - 1]).Options[0].Next = endId;

            Dictionary<string, DialogueRecord> records = new Dictionary<string, DialogueRecord>
            {
                {startId, startRecord},
                {endId, endRecord}
            };
            DialogueRunner runner = new DialogueRunner(records);
            runner.SetCurrentRecord(startId);

            foreach (string id in new[] {startId, endId}) {
                for (int i = 1; i <= 5; i++)
                {
                    DialogueRecord record = runner.CurrentRecord;
                    Assert.AreEqual(id, record.id);

                    // Assert the public Property of an internal int:idx field is reset
                    if (i == 1)
                    {
                        Assert.False(record.IsAtEndOfRecord);
                    }

                    SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;

                    // Choose the option where it's relevant
                    if (i == 5 && id == startId)
                    {
                        Assert.AreEqual($"speaker-option", line.Speaker);
                        Assert.AreEqual($"Test line - option", line.Dialogue);

                        OptionsDialogueLine option = (OptionsDialogueLine) record.CurrentDialogueLine;
                        runner.StepToNextDialogueLine(option.Options[0]);
                    }
                    else
                    {
                        Assert.AreEqual($"speaker-{i}", line.Speaker);
                        Assert.AreEqual($"Test line {i}", line.Dialogue);
                        runner.StepToNextDialogueLine();
                    }
                }
            }
        }

        [Test]
        public void DialogueRunnerReportsMissingTargetLineIfRequired()
        {
            DialogueRunner runner = DialogueRunnerMocks.GetSimpleOption();

            for (int i = 0; i < 2; i++)
            {
                // Step through to ignore SimpleDialogueLines
                bool stepSuccess = runner.StepToNextDialogueLine();
                Assert.AreEqual(true, stepSuccess);
            }

            bool success = runner.StepToNextDialogueLine();

            // TODO: How to handle logging output?
            Assert.AreEqual(false, success);
        }

        [Test]
        public void DialogueRunnerReportsMissingTargetLineNextField()
        {
            DialogueRunner runner = DialogueRunnerMocks.GetSimpleOption();

            for (int i = 0; i < 2; i++)
            {
                // Step through to ignore SimpleDialogueLines
                bool stepSuccess = runner.StepToNextDialogueLine();
                Assert.AreEqual(true, stepSuccess);
            }

            DialogueLine line = runner.CurrentDialogueLine;
            line.Next = null;

            bool success = runner.StepToNextDialogueLine(line);

            // TODO: How to handle logging output?
            Assert.AreEqual(false, success);
        }

        [Test]
        public void DialogueThrowsErrorOnInvalidRecordSet()
        {
            DialogueRunner runner = new DialogueRunner(new Dictionary<string, DialogueRecord>());
            Assert.Throws<KeyNotFoundException>(() => runner.SetCurrentRecord("file-id-that-does-not-exist"));
        }

    }
}
