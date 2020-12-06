using _Project.Dialogue;
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
            for (int i = 1; i < 5; i++)
            {
                DialogueRecord record = runner.CurrentRecord;
                DialogueLine line = record.CurrentDialogueLine;

                Assert.AreEqual(line.dialogue, "Test dialogue line " + i);
                runner.StepToNextDialogueLine();
            }
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
            Assert.AreEqual(firstRecord.id, "multi-file-test-id-1");

            DialogueLine firstLine = firstRecord.CurrentDialogueLine;

            Assert.AreEqual(firstLine.speaker, "test-user");
            Assert.AreEqual(firstLine.dialogue, "This is a test");
            Assert.AreEqual(firstLine.next, "multi-file-test-id-2");

            /* Call to step forwards and check "next" lookup has worked as expected */
            runner.StepToNextDialogueLine();
            DialogueRecord secondRecord = runner.CurrentRecord;

            Assert.IsNotNull(secondRecord);
            Assert.AreEqual(secondRecord.id, "multi-file-test-id-2");

            DialogueLine secondLine = secondRecord.CurrentDialogueLine;

            Assert.AreEqual(secondLine.speaker, "test-user-2");
            Assert.AreEqual(secondLine.dialogue, "This is another test");
            Assert.IsNull(secondLine.next);
        }
    }
}
