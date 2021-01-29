using System;
using _Project.Dialogue;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueSystemTests
    {
        [Test]
        public void DialogueSystemCanBeInstantiatedWithMinimalParams()
        {
            DialogueSystem system = new DialogueSystem("DialogueSystemTest/", "example-conversation-1");

            SpokenDialogueLine firstLine = system.CurrentDialogueLine;
            Assert.AreEqual("test-user", firstLine.Speaker);
            Assert.AreEqual("This is a test", firstLine.Dialogue);

            system.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = system.CurrentDialogueLine;
            Assert.AreEqual("test-user", secondLine.Speaker);
            Assert.AreEqual("This is also a test", secondLine.Dialogue);

        }

        [Test]
        public void DialogueSystemCanBeInstantiatedAndWorkOutStartingRecord()
        {
            DialogueSystem system = new DialogueSystem("DialogueSystemTest/");

            SpokenDialogueLine firstLine = system.CurrentDialogueLine;
            Assert.AreEqual("test-user", firstLine.Speaker);
            Assert.AreEqual("This is a test", firstLine.Dialogue);

            system.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = system.CurrentDialogueLine;
            Assert.AreEqual("test-user", secondLine.Speaker);
            Assert.AreEqual("This is also a test", secondLine.Dialogue);
        }

        [Test]
        public void DialogueSystemInformsUserIfItCannotGetStartingRecord()
        {
            // Safe bet for a folder with multiple files, for the short term
            Assert.Throws<ArgumentException>(() => new DialogueSystem("AssetLoaderTest"));
        }
    }
}