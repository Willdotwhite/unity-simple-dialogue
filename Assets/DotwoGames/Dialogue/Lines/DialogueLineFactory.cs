using DotwoGames.Dialogue.Config;

namespace DotwoGames.Dialogue.Lines
{
    /// <summary>
    /// Factory to generate DialogueLine instances from DialogueLineConfig
    /// <para>
    /// This is used for two reasons:
    /// </para>
    /// <para>
    /// 1: While it _is_ possible to intuit what class to use based on what fields the config has, Unity's internal
    /// JSON serialisation (which I was using before I turned to NewtonsoftJson in desperation) doesn't work like that
    /// </para>
    /// <para>
    /// 2: Given that I needed some code that says "work out what type of DialogueLine we have" somewhere, moving that
    /// code into a unique class lets me work more flexibly in future
    /// </para>
    /// <para>
    /// For instance, if I have a new DialogueLine class, I just need to add it here; alternatively, I could let a
    /// developer using this class build their own Factory in use it in place of this one
    /// </para>
    /// </summary>
    public static class DialogueLineFactory
    {
        /// <summary>
        /// Generate a DialogueLine from DialogueLineConfig
        /// </summary>
        /// <param name="config">DialogueLineConfig loaded from file</param>
        /// <returns></returns>
        public static DialogueLine FromConfig(DialogueLineConfig config)
        {
            if (config.options != null && config.options.Count > 0)
            {
                return new OptionsDialogueLine(config);
            }

            if (config.branch != null && config.branch.Count > 0)
            {
                return new OptionBranchDialogueLine(config);
            }

            if (config.command != null)
            {
                return new CommandDialogueLine(config);
            }

            return new SpokenDialogueLine(config);
        }
    }
}