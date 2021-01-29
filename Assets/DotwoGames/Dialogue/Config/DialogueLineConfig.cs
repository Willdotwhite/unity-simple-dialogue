using System.Collections.Generic;

// ReSharper disable UnassignedField.Global    - All fields in this file are assigned during deserialization

namespace DotwoGames.Dialogue.Config
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// <para>
    /// This config is a blob of all potential config for each JSON object in the Dialogue files - when the .json files
    /// get loaded into instances of this class, whatever fields exist will be filled in (and the other fields will
    /// be empty defaults like null)
    /// </para>
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global    - Class is instantiated at JSON deserialization
    public class DialogueLineConfig {
        public string speaker;
        public string dialogue;
        public string next;
        public string command;
        public List<DialogueLineConfig> options;
        public List<DialogueLineConfig> branch;
        public SerializableCommandParameters @params;
        public Dictionary<string, string> meta;
    }
}