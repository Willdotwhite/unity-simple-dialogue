using System.Collections.Generic;
using System.Linq;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    /// <summary>
    /// OptionBranchDialogueLine is an internal class used to handle inline branches for OptionsDialogueLine options
    /// <para>
    /// When building the DialogueRunner.Records collection, FINISH THIS LATER
    /// </para>
    /// </summary>
    // TODO: RENAME THIS CLASS
    public class OptionBranchDialogueLine: SpokenDialogueLine
    {
        public List<DialogueLine> Branch;

        public OptionBranchDialogueLine(DialogueLineConfig config) : base(config)
        {
            Branch = config.branch.Select(DialogueLineFactory.FromConfig).ToList();
        }
    }
}