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
            Assert.AreEqual(option1.Speaker, "options-test-user");
            Assert.AreEqual(option1.Dialogue, "Options test option 1");
            Assert.AreEqual(option1.Next, "options-next-1");

            SpokenDialogueLine option2 = line.GetOptionByNext("options-next-2");
            Assert.AreEqual(option2.Speaker, "options-test-user");
            Assert.AreEqual(option2.Dialogue, "Options test option 2");
            Assert.AreEqual(option2.Next, "options-next-2");
        }
    }
}