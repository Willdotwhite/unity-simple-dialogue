using System.Linq;
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

            Assert.AreEqual(line.speaker, "simple-test-user");
            Assert.AreEqual(line.dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            DialogueLine line = runner.CurrentRecord.CurrentDialogueLine;

            Assert.AreEqual(line.speaker, "options-test-user");
            Assert.AreEqual(line.dialogue, "This is an options test");
            Assert.AreEqual(line.options.Count, 2);

            Assert.AreEqual(line.options[0].speaker, "options-test-user");
            Assert.AreEqual(line.options[0].dialogue, "Options test option 1");
            Assert.AreEqual(line.options[0].next, "options-next-1");

            Assert.AreEqual(line.options[1].speaker, "options-test-user");
            Assert.AreEqual(line.options[1].dialogue, "Options test option 2");
            Assert.AreEqual(line.options[1].next, "options-next-2");
        }
    }
}