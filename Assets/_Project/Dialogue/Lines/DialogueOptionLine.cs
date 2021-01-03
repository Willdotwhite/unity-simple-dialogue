using System.Collections.Generic;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class DialogueOptionLine : DialogueLine
    {
        /// <summary>
        /// Available dialogue options
        /// </summary>
        public readonly List<IDialogueLine> Options = new List<IDialogueLine>();

        public DialogueOptionLine(DialogueLineConfig config) : base(config)
        {
            foreach (DialogueLineConfig lineConfig in config.options)
            {
                Options.Add(DialogueLineFactory.FromConfig(lineConfig));
            }
        }
    }
}