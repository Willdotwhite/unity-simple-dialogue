using System.Collections.Generic;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class OptionsDialogueLine : SpokenDialogueLine
    {
        /// <summary>
        /// Available dialogue options
        /// </summary>
        public readonly List<DialogueLine> Options = new List<DialogueLine>();

        public OptionsDialogueLine(DialogueLineConfig config) : base(config)
        {
            foreach (DialogueLineConfig lineConfig in config.options)
            {
                Options.Add(DialogueLineFactory.FromConfig(lineConfig));
            }
        }
    }
}