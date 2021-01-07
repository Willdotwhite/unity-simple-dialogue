using System.Collections.Generic;
using _Project.Dialogue.Lines;

namespace _Project.Dialogue
{
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