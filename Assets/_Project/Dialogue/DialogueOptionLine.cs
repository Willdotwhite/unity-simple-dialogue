using System.Collections.Generic;

namespace _Project.Dialogue
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