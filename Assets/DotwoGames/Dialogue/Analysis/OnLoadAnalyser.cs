using System;
using System.Collections.Generic;
using DotwoGames.Dialogue.Lines;

namespace DotwoGames.Dialogue.Analysis
{
    public static class OnLoadAnalyser
    {
        // In dev, this'll throw exceptions, until I mock out a logger or something
        public static void CheckRecords(Dictionary<string, DialogueRecord> records)
        {
            foreach (DialogueRecord record in records.Values)
            {
                CheckRecordForOrphans(record);
            }
        }

        private static void CheckRecordForOrphans(DialogueRecord record)
        {
            // TODO: Handle Options
            int idOfStepLine = -1; // this is an impossible ID

            for (int i = 0; i < record.dialogueLines.Count; i++)
            {
                DialogueLine line = record.dialogueLines[i];

                // When we get to a line with Next, record the ID of this line
                // We only care about the first .Next, as everything after that is unreachable
                // TODO: What about conditions?
                if (idOfStepLine == -1 && line.Next != null)
                {
                    idOfStepLine = i;
                }

                // If we're past this line, then the Record has children that can't be reached
                // idOfStepLine > -1 is just a check that says "has the value been set already?"
                if (idOfStepLine > -1 && idOfStepLine < i)
                {
                    throw new FormatException($"{record.id} contains dialogue lines that cannot be reached");
                }
            }
        }
    }
}