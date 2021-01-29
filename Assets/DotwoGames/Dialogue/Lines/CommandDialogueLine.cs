using System.Collections.Generic;
using DotwoGames.Dialogue.Config;

namespace DotwoGames.Dialogue.Lines
{
    /// <summary>
    /// A CommandDialogueLine is a DialogueLine that runs code behind the scenes
    /// <para>
    /// CommandDialogueLines shouldn't be displayed to the player; they're used as a way for the developer to easily
    /// call code at certain points of the dialogue.
    /// </para>
    /// <para>
    /// When the DialogueRunner moves to a CommandDialogueLine it will automatically call the command and move to the
    /// next line - this is so that the DialogueRunner never returns a CommandDialogueLine on CurrentDialogueLine
    /// </para>
    /// </summary>
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

            ConvertParameterValueTypes(config);
        }

        /// <summary>
        /// The config loaded from file are being loaded as typeof(string), but can be any of the following:
        /// <para>
        /// int: any whole number (e.g. without a decimal place)
        /// </para>
        /// <para>
        /// float: any number with a decimal place
        /// </para>
        /// <para>
        /// bool: the strings "true" and "false" stored in JSON _without the quotes_
        /// </para>
        /// <para>
        /// string: the default type, if we can't parse a type to one of the above we default to this
        /// </para>
        /// </summary>
        /// <param name="config"></param>
        private void ConvertParameterValueTypes(DialogueLineConfig config)
        {
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