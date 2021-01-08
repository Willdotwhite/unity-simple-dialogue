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
            [CanBeNull] string next = null,
            [CanBeNull] Dictionary<string, string> meta = null
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
            [CanBeNull] string speaker = null,
            [CanBeNull] string dialogue = null,
            [CanBeNull] string next = null,
            [CanBeNull] string command = null,
            [CanBeNull] List<DialogueLineConfig> options = null,
            [CanBeNull] List<DialogueLineConfig> branch = null,
            [CanBeNull] SerializableCommandParameters @params = null,
            [CanBeNull] Dictionary<string, string> meta = null
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