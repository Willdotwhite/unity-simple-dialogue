using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueRunnerTests
    {
        [Test]
        public void DialogueStepsThroughAllLinesInRecord()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("MultiLineDialogueTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("multi-file-test-id-1");
            for (int i = 1; i <= 5; i++)
            {
                DialogueRecord record = runner.CurrentRecord;
                SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;

                Assert.AreEqual($"Test dialogue line {i}", line.Dialogue);
                runner.StepToNextDialogueLine();
            }
        }

        [Test]
        public void DialogueStepsThroughChosenDialogueLineInOption()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("Options/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options-runner-test");
            OptionsDialogueLine currentOptionsDialogueLine = (OptionsDialogueLine) runner.CurrentDialogueLine;
            DialogueLine targetLine = currentOptionsDialogueLine.Options[0];
            runner.StepToNextDialogueLine(targetLine);
            Assert.AreEqual("options-next-1", runner.CurrentRecord.id);
        }


        [Test]
        public void DialogueReportsMissingTargetLineIfRequired()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("Options/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options-runner-test");

            bool success = runner.StepToNextDialogueLine();

            // TODO: How to handle logging output?
            Assert.AreEqual(false, success);
        }

        [Test]
        public void DialogueTraversesAllRelatedFiles()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("MultiFileTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("multi-file-test-id-1");

            /* Test current record is loaded as expected */
            DialogueRecord firstRecord = runner.CurrentRecord;

            Assert.IsNotNull(firstRecord);
            Assert.AreEqual("multi-file-test-id-1", firstRecord.id);

            SpokenDialogueLine firstLine = (SpokenDialogueLine) firstRecord.CurrentDialogueLine;

            Assert.AreEqual("test-user", firstLine.Speaker);
            Assert.AreEqual("This is a test", firstLine.Dialogue);
            Assert.AreEqual("multi-file-test-id-2", firstLine.Next);

            /* Call to step forwards and check "next" lookup has worked as expected */
            runner.StepToNextDialogueLine();
            DialogueRecord secondRecord = runner.CurrentRecord;

            Assert.IsNotNull(secondRecord);
            Assert.AreEqual("multi-file-test-id-2", secondRecord.id);

            SpokenDialogueLine secondLine = (SpokenDialogueLine) secondRecord.CurrentDialogueLine;

            Assert.AreEqual("test-user-2", secondLine.Speaker);
            Assert.AreEqual("This is another test", secondLine.Dialogue);
            Assert.IsNull(secondLine.Next);
        }

        [Test]
        public void DialogueThrowsErrorOnInvalidRecordSet()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("LoopingConversationTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            Assert.Throws<KeyNotFoundException>(() => runner.SetCurrentRecord("file-id-that-does-not-exist"));
        }

        [Test]
        public void LoopingDialogueResetsInternals()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("LoopingConversationTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("looping-file-test-id-1");
            for (int i = 1; i <= 3; i++)
            {
                DialogueRecord record = runner.CurrentRecord;
                Assert.IsFalse(record.IsAtEndOfRecord);

                for (int j = 1; j <= 2; j++)
                {

                    SpokenDialogueLine line = (SpokenDialogueLine) record.CurrentDialogueLine;

                    Assert.AreEqual($"Test dialogue line {i} - {j}", line.Dialogue);
                    runner.StepToNextDialogueLine();
                }
            }
        }
    }
}
