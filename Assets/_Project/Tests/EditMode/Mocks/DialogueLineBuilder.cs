using System.Collections.Generic;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using JetBrains.Annotations;
// ReSharper disable ArgumentsStyleNamedExpression

namespace _Project.Tests.EditMode.Mocks
{
    public static class DialogueLineBuilder
    {
        public static SpokenDialogueLine Spoken(
            string speaker,
            string dialogue,
            string next = null,
            Dictionary<string, string> meta = null
        )
        {
            return new SpokenDialogueLine(BuildDialogueLineConfig(
                speaker: speaker,
                dialogue: dialogue,
                next: next,
                meta: meta
            ));
        }

        public static OptionsDialogueLine Option(
            string speaker,
            string dialogue,
            List<DialogueLineConfig> options
        )
        {
            return new OptionsDialogueLine(BuildDialogueLineConfig(
                speaker: speaker,
                dialogue: dialogue,
                options: options
            ));
        }

        public static DialogueLineConfig BuildDialogueLineConfig(
            string speaker = null,
            string dialogue = null,
            string next = null,
            string command = null,
            List<DialogueLineConfig> options = null,
            List<DialogueLineConfig> branch = null,
            SerializableCommandParameters @params = null,
            Dictionary<string, string> meta = null
        )
        {
            return new DialogueLineConfig
            {
                speaker = speaker,
                dialogue = dialogue,
                next = next,
                command = command,
                options = options,
                branch = branch,
                @params = @params,
                meta = meta,
            };
        }
    }
}