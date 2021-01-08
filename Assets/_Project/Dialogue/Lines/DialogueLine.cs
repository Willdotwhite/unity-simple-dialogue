using System.Collections.Generic;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// <para>
    /// This is the base class of this system, all classes need to be based off of this
    /// </para>
    /// </summary>
    public abstract class DialogueLine {

        /// <summary>
        /// Optional ID of the DialogueRecord to jump to after this line
        /// <para>
        /// If not set, the Next dialogue line will be the next in the dialogueLines array of the current DialogueRecord
        /// </para>
        /// <para>
        /// If the DialogueLine is at the end of the DialogueRecord and doesn't have a Next set, the runner will end
        /// </para>
        /// </summary>
        public string Next;

        /// <summary>
        /// Meta is a blank space for any other fields you want to attach to a specific dialogue line
        /// <para>
        /// For instance, if you wanted to track an audio file for each SpokenDialogueLine, you would
        /// put it in Meta as the general catch-all for unsupported fields
        /// </para>
        /// </summary>
        public Dictionary<string, string> Meta;

        protected DialogueLine(DialogueLineConfig config)
        {
            Next = config.next;
            Meta = config.meta;
        }
    }
}