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
                foreach (DialogueLine line in record.dialogueLines)
                {
                    foreach (KeyValuePair<string,string> keyValuePair in replacements)
                    {
                        line.dialogue = line.dialogue.Replace(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }
        }
    }
}