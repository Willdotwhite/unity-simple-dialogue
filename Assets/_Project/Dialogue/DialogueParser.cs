using System.Collections.Generic;
using _Project.Dialogue.Lines;

namespace _Project.Dialogue
{
    public class DialogueParser
    {
        private readonly Dictionary<string, string> replacements;

        public DialogueParser(Dictionary<string, string> replacements)
        {
            this.replacements = replacements;
        }

        public void Parse(Dictionary<string, DialogueRecord> records)
        {
            foreach (DialogueRecord record in records.Values)
            {
                foreach (IDialogueLine dialogueLine in record.dialogueLines)
                {
                    // Return early to avoid CastException
                    if (dialogueLine is CommandDialogueLine)
                    {
                        return;
                    }

                    DialogueLine line = (DialogueLine) dialogueLine;
                    foreach (KeyValuePair<string,string> keyValuePair in replacements)
                    {
                        // TODO: Should this include speaker etc?
                        line.Dialogue = line.Dialogue.Replace(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }
        }
    }
}