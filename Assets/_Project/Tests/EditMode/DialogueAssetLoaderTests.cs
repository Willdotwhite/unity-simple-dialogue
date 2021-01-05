using System.IO;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueAssetLoaderTests
    {
        [Test]
        public void DialogueAssetSimplePasses()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("SingleFileTest/");
            Assert.NotZero(loader.Records.Count);

            DialogueRecord record = loader.Records["single-file-test-id-1"];
            Assert.AreEqual(record.id, "single-file-test-id-1");

            SpokenDialogueLine line = (SpokenDialogueLine) record.dialogueLines[0];

            Assert.AreEqual(line.Speaker, "simple-test-user");
            Assert.AreEqual(line.Dialogue, "This is a simple test");
        }

        [Test]
        public void DialogueAssetLoadsAllRelatedFiles()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("MultiFileTest/");
            Assert.AreEqual(loader.Records.Count, 2);
        }

        [Test]
        public void DialogueAssetThrowsErrorIfFilesNotFound()
        {
            Assert.Throws<FileLoadException>(() => new DialogueAssetLoader("InvalidOrEmptyFilePath/"));
        }
    }
}
