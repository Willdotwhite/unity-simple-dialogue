using System.Collections.Generic;
using System.Linq;
using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    /// <summary>
    /// A DialogueLine that has a set of multiple potential paths to follow
    /// <para>
    /// Each potential Option needs to have a Next that will point to a new DialogueRecord
    /// </para>
    /// </summary>
    public class OptionsDialogueLine : SpokenDialogueLine
    {
        /// <summary>
        /// Available dialogue options
        /// </summary>
        public readonly List<SpokenDialogueLine> Options;

        public OptionsDialogueLine(DialogueLineConfig config) : base(config)
        {
            Options = config.options.Select(DialogueLineFactory.FromConfig).Cast<SpokenDialogueLine>().ToList();

            // TODO: Boundary checking on invalid config clashes

            // if (config.Next) {throw new Excpetion}
        }

        /// <summary>
        /// Get the desired Option to jump to be the string value of the Next field
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public SpokenDialogueLine GetOptionByNext(string next)
        {
            // TODO: Enforce only one option of each Next exists
            return Options.First(o => o.Next.Equals(next));
        }
    }
}