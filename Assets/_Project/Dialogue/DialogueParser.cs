﻿using System.Collections.Generic;
using _Project.Dialogue.Lines;

namespace _Project.Dialogue
{
    /// <summary>
    /// DialogueParser is an optional extension class that allows you to change the dialogue stored on disk into
    /// something else
    /// <para>
    /// This is designed for easy use of variables or placeholders to be replaced when the dialogue is loaded - e.g.
    /// {"speaker": "npc::town::cleric_2"} or {"dialogue": "Welcome to _TOWN_NAME"} instead of remembering a
    /// specific character or place name
    /// </para>
    /// <para>
    /// This isn't required for the system to run, but it allows you to write your dialogue in whatever way works best
    /// for you; I personally never like using explicit names like {"speaker": "Jeremy"} in code in case the name changes
    /// later on.
    /// </para>
    /// </summary>
    public class DialogueParser
    {
        private readonly Dictionary<string, string> replacements;

        private readonly bool canReplaceMetaFields;

        /// <summary>
        /// A find-and-replace parser for DialogueLine variable values
        /// </summary>
        /// <param name="replacements">Dictionary of key-value pair replacements</param>
        /// <param name="canReplaceMetaFields">Can this parser change the meta fields like Next? By default, a Parser only changes Speaker and Dialogue</param>
        public DialogueParser(Dictionary<string, string> replacements, bool canReplaceMetaFields = false)
        {
            this.replacements = replacements;
            this.canReplaceMetaFields = canReplaceMetaFields;
        }

        /// <summary>
        /// Replace all given string patterns to their appropriate replacements
        /// </summary>
        /// <param name="records"></param>
        public void Parse(Dictionary<string, DialogueRecord> records)
        {
            foreach (DialogueRecord record in records.Values)
            {
                foreach (DialogueLine dialogueLine in record.dialogueLines)
                {
                    // Continue for this loop to avoid CastException
                    if (dialogueLine is CommandDialogueLine)
                    {
                        continue;
                    }

                    SpokenDialogueLine line = (SpokenDialogueLine) dialogueLine;
                    foreach (KeyValuePair<string,string> keyValuePair in replacements)
                    {
                        line.Speaker = line.Speaker.Replace(keyValuePair.Key, keyValuePair.Value);
                        line.Dialogue = line.Dialogue.Replace(keyValuePair.Key, keyValuePair.Value);

                        if (canReplaceMetaFields)
                        {
                            line.Next = line.Next.Replace(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                }
            }
        }
    }
}