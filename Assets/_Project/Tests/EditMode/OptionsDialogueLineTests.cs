using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class OptionsDialogueLineTests
    {
        [Test]
        public void OptionsDialogueLineCanLookUpOptionByUniqueId()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("DialogueLineFormatTest/");
            DialogueRunner runner = new DialogueRunner(loader.Records);

            runner.SetCurrentRecord("options");
            OptionsDialogueLine line = (OptionsDialogueLine) runner.CurrentDialogueLine;

            SpokenDialogueLine option1 = line.GetOptionByNext("options-next-1");
            Assert.AreEqual("options-test-user", option1.Speaker);
            Assert.AreEqual("Options test option 1", option1.Dialogue);
            Assert.AreEqual("options-next-1", option1.Next);

            SpokenDialogueLine option2 = line.GetOptionByNext("options-next-2");
            Assert.AreEqual("options-test-user", option2.Speaker);
            Assert.AreEqual("Options test option 2", option2.Dialogue);
            Assert.AreEqual("options-next-2", option2.Next);
        }
    }
}