using System.Collections.Generic;
using DotwoGames.Dialogue;
using DotwoGames.Dialogue.Config;
using DotwoGames.Dialogue.Lines;

namespace DotwoGames.Tests.EditMode.Mocks
{
    public static class SimpleDialogueMocks
    {
        private const string speakerPrefix = "speaker-";

        private const string dialogueLinePrefix = "Test line";

        private const string nextPrefix = "next-";

        // public static DialogueRecord GetEmpty(string id)
        // {
        //     return GetSimple(id, 0);
        // }

        public static DialogueRecord GetSimple(string id, int lineCount)
        {
            List<DialogueLine> lines = new List<DialogueLine>();
            for (int i = 1; i <= lineCount; i++)
            {
                lines.Add(DialogueLineBuilder.Spoken(
                    $"{speakerPrefix}{i}",
                    $"{dialogueLinePrefix} {i}"
                ));
            }

            return new DialogueRecord(id, lines);
        }

        public static DialogueRecord GetSimpleOption(string id, int preOptionLineCount)
        {
            List<DialogueLine> lines = new List<DialogueLine>();
            for (int i = 1; i <= preOptionLineCount; i++)
            {
                lines.Add(DialogueLineBuilder.Spoken(
                    $"{speakerPrefix}{i}",
                    $"{dialogueLinePrefix} {i}"
                ));
            }

            List<DialogueLineConfig> optionsConfig = new List<DialogueLineConfig>();
            optionsConfig.Add(DialogueLineBuilder.BuildDialogueLineConfig(
                speaker: $"{speakerPrefix}1",
                $"{dialogueLinePrefix} - option - branch 1",
                $"{nextPrefix}option-branch-1"
            ));
            optionsConfig.Add(DialogueLineBuilder.BuildDialogueLineConfig(
                speaker: $"{speakerPrefix}2",
                $"{dialogueLinePrefix} - option - branch 2",
                $"{nextPrefix}option-branch-2"
            ));

            lines.Add(DialogueLineBuilder.Option(
                $"{speakerPrefix}option",
                $"{dialogueLinePrefix} - option",
                optionsConfig
            ));

            return new DialogueRecord(id, lines);
        }
    }
}