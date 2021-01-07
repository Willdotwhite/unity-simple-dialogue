using System.Collections.Generic;

namespace _Project.Dialogue.Config
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// <para>
    /// This config is a blob of all potential config for each JSON object in the Dialogue files - when the .json files
    /// get loaded into instances of this class, whatever fields exist will be filled in (and the other fields will
    /// be empty defaults like null)
    /// </para>
    /// </summary>
    public class DialogueLineConfig {
        public string speaker;
        public string dialogue;
        public string next;
        public string command;
        public List<DialogueLineConfig> options;
        public SerializableCommandParameters @params;
    }
}