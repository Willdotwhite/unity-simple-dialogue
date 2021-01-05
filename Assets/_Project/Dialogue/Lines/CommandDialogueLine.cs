using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class CommandDialogueLine : DialogueLine
    {
        /// <summary>
        /// String key to UnityEvent method built in project
        /// </summary>
        public readonly string Command;

        public readonly CommandParameters Params;

        public CommandDialogueLine(DialogueLineConfig config): base(config)
        {
            Command = config.command;
            Params = config.@params;
        }
    }
}