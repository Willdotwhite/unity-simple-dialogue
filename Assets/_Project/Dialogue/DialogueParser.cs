using System.Collections.Generic;

namespace _Project.Dialogue
{
    public class DialogueParser
    {
        public DialogueParserType Type { get; private set; }

        private Dictionary<string, string> replacements;

        public DialogueParser(DialogueParserType type, Dictionary<string, string> replacements)
        {
            this.Type = type;
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