using System.Collections.Generic;
using System.Linq;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class OptionsDialogueLine : SpokenDialogueLine
    {
        /// <summary>
        /// Available dialogue options
        /// </summary>
        public readonly List<SpokenDialogueLine> Options = new List<SpokenDialogueLine>();

        public OptionsDialogueLine(DialogueLineConfig config) : base(config)
        {
            foreach (DialogueLineConfig lineConfig in config.options)
            {
                // TODO: Handle CommandDialogueLine cases
                DialogueLine line = DialogueLineFactory.FromConfig(lineConfig);
                Options.Add((SpokenDialogueLine) line);
            }
        }

        // TODO: Set chosen option here?
        public SpokenDialogueLine GetOptionByNext(string next)
        {
            return Options.First(o => o.Next.Equals(next));
        }
    }
}