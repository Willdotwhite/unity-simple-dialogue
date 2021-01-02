namespace _Project.Dialogue
{
    public class CommandDialogueLine : BaseDialogueLine
    {
        public string command;

        public CommandDialogueLine(DialogueLineConfig config)
        {
            command = config.command;
        }
    }
}