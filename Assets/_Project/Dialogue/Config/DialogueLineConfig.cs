using System.Collections.Generic;

namespace _Project.Dialogue.Config
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// </summary>
    [System.Serializable]
    public class DialogueLineConfig {
        public string speaker;
        public string dialogue;
        public string next;
        public string command;
        public List<DialogueLineConfig> options;
    }
}