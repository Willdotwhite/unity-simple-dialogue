using _Project.Dialogue;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueAssetLoaderTests
    {
        [Test]
        public void DialogueSimplePasses()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("SingleFileTest/");
            Assert.NotZero(loader.Records.Count);

            DialogueRecord record = loader.Records["single-file-test-id-1"];
            Assert.AreEqual(record.id, "single-file-test-id-1");

            DialogueLine line = record.entries[0];

            Assert.AreEqual(line.speaker, "test-user");
            Assert.AreEqual(line.dialogue, "This is a test");
            Assert.AreEqual(line.meta, "end");
            Assert.AreEqual(line.next, "0");
        }

        [Test]
        public void DialogueLoadsAndTraversesAllRelatedFiles()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("MultiFileTest/");
            Assert.NotZero(loader.Records.Count);
        }
    }
}
