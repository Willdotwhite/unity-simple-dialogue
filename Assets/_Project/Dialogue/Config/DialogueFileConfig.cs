using System.Collections.Generic;

namespace _Project.Dialogue.Config
{
    /// <summary>
    /// Config for a DialogueFile as it will be represented in JSON
    /// </summary>
    public class DialogueFileConfig
    {
        public string id;

        // Note that this is DialogueLineConfig; the Config class that only contains primitive types
        // It makes loading the file easier to handle (or at least did when I was using the Unity serialisation tool)
        public List<DialogueLineConfig> dialogueLines;
    }
}