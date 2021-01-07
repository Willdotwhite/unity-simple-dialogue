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

        protected DialogueLine(DialogueLineConfig config)
        {
            Next = config.next;
        }
    }
}