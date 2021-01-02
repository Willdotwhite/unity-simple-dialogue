using System.Collections.Generic;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class DialogueOptionLine : DialogueLine
    {
        public List<IDialogueLine> options;

        public DialogueOptionLine(DialogueLineConfig config) : base(config)
        {
            // TODO: This is grim, refactor
            options = new List<IDialogueLine>();

            foreach (DialogueLineConfig lineConfig in config.options)
            {
                options.Add(DialogueLineFactory.FromConfig(lineConfig));
            }
        }
    }
}