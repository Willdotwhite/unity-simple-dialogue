namespace _Project.Dialogue
{
    public static class DialogueLineFactory
    {
        public static IDialogueLine FromConfig(DialogueLineConfig config)
        {
            if (config.options.Count > 0)
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