using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            Params = new CommandParameters();

            // Typecasting for better uniformity
            if (config.@params == null)
            {
                return;
            }

            foreach (KeyValuePair<string, string> kvp in config.@params)
            {
                if (kvp.Value.Contains(".") && float.TryParse(kvp.Value, out float f))
                {
                    Params[kvp.Key] = f;
                }
                else if (int.TryParse(kvp.Value, out int i))
                {
                    Params[kvp.Key] = i;
                }
                else if (bool.TryParse(kvp.Value, out bool b))
                {
                    Params[kvp.Key] = b;
                }
                else
                {
                    // String as default
                    Params[kvp.Key] = kvp.Value;
                }
            }
        }
    }
}