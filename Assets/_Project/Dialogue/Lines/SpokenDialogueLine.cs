using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    /// <summary>
    /// A DialogueLine is a single entry in a DialogueRecord (which is a subset of a larger conversation/narration)
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