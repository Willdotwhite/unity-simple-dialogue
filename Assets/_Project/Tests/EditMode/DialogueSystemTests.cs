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
            DialogueSystem _dialogueSystem = new DialogueSystem("DialogueSystemTests", "example-conversation-1");

            SpokenDialogueLine firstLine = (SpokenDialogueLine) _dialogueSystem.CurrentDialogueLine;
            Assert.AreEqual(firstLine.Speaker, "test-user");
            Assert.AreEqual(firstLine.Dialogue, "This is a test");

            _dialogueSystem.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = (SpokenDialogueLine) _dialogueSystem.CurrentDialogueLine;
            Assert.AreEqual(secondLine.Speaker, "test-user");
            Assert.AreEqual(secondLine.Dialogue, "This is also a test");

        }

        [Test]
        public void DialogueSystemCanBeInstantiatedAndWorkOutStartingRecord()
        {
            DialogueSystem _dialogueSystem = new DialogueSystem("DialogueSystemTests");

            SpokenDialogueLine firstLine = (SpokenDialogueLine) _dialogueSystem.CurrentDialogueLine;
            Assert.AreEqual(firstLine.Speaker, "test-user");
            Assert.AreEqual(firstLine.Dialogue, "This is a test");

            _dialogueSystem.StepToNextDialogueLine();

            SpokenDialogueLine secondLine = (SpokenDialogueLine) _dialogueSystem.CurrentDialogueLine;
            Assert.AreEqual(secondLine.Speaker, "test-user");
            Assert.AreEqual(secondLine.Dialogue, "This is also a test");
        }

        [Test]
        public void DialogueSystemInformsUserIfItCannotGetStartingRecord()
        {
            // Safe bet for a folder with multiple files, for the short term
            Assert.Throws<ArgumentException>(() => new DialogueSystem("LoopingConversationTest"));
        }
    }
}