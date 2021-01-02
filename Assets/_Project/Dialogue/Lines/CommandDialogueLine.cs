using _Project.Dialogue.Config;

namespace _Project.Dialogue.Lines
{
    public class CommandDialogueLine : IDialogueLine
    {
        public string command;

        public CommandDialogueLine(DialogueLineConfig config)
        {
            command = config.command;
        }
    }
}