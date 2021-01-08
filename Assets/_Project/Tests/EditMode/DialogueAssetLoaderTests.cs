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
            DialogueAssetLoader loader = new DialogueAssetLoader("AssetLoaderTest/Single/");
            Assert.NotZero(loader.Records.Count);

            DialogueRecord record = loader.Records["single-file-test-id-1"];
            Assert.AreEqual("single-file-test-id-1", record.id);

            SpokenDialogueLine line = (SpokenDialogueLine) record.dialogueLines[0];

            Assert.AreEqual("simple-test-user", line.Speaker);
            Assert.AreEqual("This is a simple test", line.Dialogue);
        }

        [Test]
        public void DialogueAssetLoadsAllRelatedFiles()
        {
            DialogueAssetLoader loaderWithoutTrailingSlash = new DialogueAssetLoader("AssetLoaderTest/Multiple");
            Assert.AreEqual(2, loaderWithoutTrailingSlash.Records.Count);

            DialogueAssetLoader loaderWithTrailingSlash = new DialogueAssetLoader("AssetLoaderTest/Multiple/");
            Assert.AreEqual(2, loaderWithTrailingSlash.Records.Count);
        }

        [Test]
        public void DialogueAssetThrowsErrorIfFilesNotFound()
        {
            Assert.Throws<FileLoadException>(() => new DialogueAssetLoader("InvalidOrEmptyFilePath/"));
        }
    }
}
