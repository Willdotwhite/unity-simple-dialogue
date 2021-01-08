using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    // TODO: Refactor into class-based tests
    public class DialogueLineTests
    {
        [Test]
        public void DialogueLineDeserialisesSimpleLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("SingleFileTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("single-file-test-id-1");
            SpokenDialogueLine line = (SpokenDialogueLine) runner.CurrentDialogueLine;

            Assert.AreEqual("simple-test-user", line.Speaker);
            Assert.AreEqual("This is a simple test", line.Dialogue);
            Assert.AreEqual("some-test-file.mp3", line.Meta["audio"]);
        }

        [Test]
        public void DialogueLineDeserialisesOptionsLine()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            OptionsDialogueLine line = (OptionsDialogueLine) runner.CurrentDialogueLine;

            Assert.AreEqual("options-test-user", line.Speaker);
            Assert.AreEqual("This is an options test", line.Dialogue);
            Assert.AreEqual(2, line.Options.Count);

            SpokenDialogueLine option1 = (SpokenDialogueLine) line.Options[0];
            Assert.AreEqual("options-test-user", option1.Speaker);
            Assert.AreEqual("Options test option 1", option1.Dialogue);
            Assert.AreEqual("options-next-1", option1.Next);

            SpokenDialogueLine option2 = (SpokenDialogueLine) line.Options[1];
            Assert.AreEqual("options-test-user", option2.Speaker);
            Assert.AreEqual("Options test option 2", option2.Dialogue);
            Assert.AreEqual("options-next-2", option2.Next);
        }
    }
}