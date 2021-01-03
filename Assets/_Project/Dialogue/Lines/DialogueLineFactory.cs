using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public static class DialogueLineFactory
    {
        public static IDialogueLine FromConfig(DialogueLineConfig config)
        {
            if (config.options != null && config.options.Count > 0)
            {
                return new DialogueOptionLine(config);
            }

            if (config.command != null)
            {
                return new CommandDialogueLine(config);
            }

            return new DialogueLine(config);
        }
    }
}