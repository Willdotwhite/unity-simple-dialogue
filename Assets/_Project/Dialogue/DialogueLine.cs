namespace _Project.Dialogue
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// </summary>
    [System.Serializable]
    // TODO abstract and break out
    public class DialogueLine {
        // TODO: Comment and markup after readding "meta"
        public string speaker;
        public string dialogue;
        public string next;
    }
}