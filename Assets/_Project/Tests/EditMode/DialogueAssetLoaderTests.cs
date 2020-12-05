using _Project.Dialogue;
using NUnit.Framework;

namespace _Project.Tests.EditMode
{
    public class DialogueAssetLoaderTests
    {
        [Test]
        public void DialogueSimplePasses()
        {
            DialogueAssetLoader loader = new DialogueAssetLoader("test.json");
            Assert.NotZero(loader.Entries.Count);

            DialogueEntry entry = loader.Entries[0];
            Assert.AreEqual(entry.speaker, "test-user");
            Assert.AreEqual(entry.dialogue, "This is a test");
            Assert.AreEqual(entry.meta, "end");
            Assert.AreEqual(entry.next, "0");
        }

    }
}
