using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    /// <summary>
    /// A SpokenDialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
    /// <para>
    /// This is the base class it is expected you will use, as it is the line that is "spoken" by a character and should
    /// be printed on screen (even if the "character" is a narrator or whatever)
    /// </para>
    /// </summary>
    public class SpokenDialogueLine : DialogueLine {

        /// <summary>
        /// Who is speaking this line?
        /// </summary>
        public string Speaker;

        /// <summary>
        /// Dialogue of the line, what is being said
        /// </summary>
        public string Dialogue;

        public SpokenDialogueLine(DialogueLineConfig config) : base(config)
        {
            Speaker = config.speaker;
            Dialogue = config.dialogue;
        }
    }
}