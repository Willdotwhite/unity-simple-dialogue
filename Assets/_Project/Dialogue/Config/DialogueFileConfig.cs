using System.Collections.Generic;
// ReSharper disable UnassignedField.Global    - Collection assigned on file deserialization

namespace _Project.Dialogue.Config
{
    /// <summary>
    /// Config for a DialogueFile as it will be represented in JSON
    /// </summary>
    public class DialogueFileConfig
    {
        public string id;

        /// <summary>
        /// Note that this is DialogueLineConfig; the Config class that only contains primitive types
        /// <para>
        /// It makes loading the file easier to handle (or at least did when I was using the Unity serialisation tool)
        /// </para>
        /// </summary>
        // ReSharper disable once CollectionNeverUpdated.Global - Collection cannot be marked readonly re: JSON
        public List<DialogueLineConfig> dialogueLines;
    }
}