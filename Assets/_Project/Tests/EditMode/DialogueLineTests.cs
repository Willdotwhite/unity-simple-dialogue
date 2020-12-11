using _Project.Dialogue;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueLineTests
    {
        [Test]
        public void DialogueLineDeserialisesSimpleLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("simple");
            DialogueLine line = runner.CurrentRecord.CurrentDialogueLine;

            Assert.AreEqual(line.Speaker, "simple-test-user");
            Assert.AreEqual(line.Dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            DialogueLine line = runner.CurrentRecord.CurrentDialogueLine;

            Assert.AreEqual(line.Speaker, "options-test-user");
            Assert.AreEqual(line.Dialogue, "This is an options test");
            Assert.AreEqual(line.Options.Count, 2);

            Assert.AreEqual(line.Options[0].Speaker, "options-test-user");
            Assert.AreEqual(line.Options[0].Dialogue, "Options test option 1");
            Assert.AreEqual(line.Options[0].Next, "options-next-1");

            Assert.AreEqual(line.Options[1].Speaker, "options-test-user");
            Assert.AreEqual(line.Options[1].Dialogue, "Options test option 2");
            Assert.AreEqual(line.Options[1].Next, "options-next-2");
        }
    }
}