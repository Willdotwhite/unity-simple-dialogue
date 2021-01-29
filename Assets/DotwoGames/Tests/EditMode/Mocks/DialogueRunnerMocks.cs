using System;
using System.Collections.Generic;
using System.Linq;
using DotwoGames.Dialogue;
using JetBrains.Annotations;

namespace DotwoGames.Tests.EditMode.Mocks
{
    public static class DialogueRunnerMocks
    {
        private const int defaultLineCount = 3;

        public static DialogueRunner GetSimple(string id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid().ToString();
            }

            Dictionary<string, DialogueRecord> records = GetSimpleDialogueRecordsForIds(new List<string> {id});

            DialogueRunner runner = new DialogueRunner(records);
            runner.SetCurrentRecord(id);

            return runner;
        }

        public static DialogueRunner GetMultiRecordByNext(List<string> ids = null)
        {
            Dictionary<string, DialogueRecord> records = GetSimpleDialogueRecordsForIds(ids);

            for (int i = 0; i < ids.Count - 1; i++) // -1 to ensure we don't try to overflow for ids[i+1]
            {
                // Set the next of this record to the ID of the next
                records[ids[i]].dialogueLines[defaultLineCount - 1].Next = ids[i+1];
            }

            DialogueRunner runner = new DialogueRunner(records);
            runner.SetCurrentRecord(ids[0]);

            return runner;
        }

        public static DialogueRunner GetSimpleOption(string id = null, int preOptionLineCount = defaultLineCount - 1)
        {
            if (id == null)
            {
                id = Guid.NewGuid().ToString();
            }

            Dictionary<string, DialogueRecord> records = GetSimpleOptionDialogueRecordsForIds(new []{id}, preOptionLineCount);

            DialogueRunner runner = new DialogueRunner(records);
            runner.SetCurrentRecord(id);

            return runner;
        }

        private static Dictionary<string, DialogueRecord> GetSimpleDialogueRecordsForIds(IEnumerable<string> ids)
        {
            return ids.ToDictionary(id => id, id => SimpleDialogueMocks.GetSimple(id, defaultLineCount));
        }

        private static Dictionary<string, DialogueRecord> GetSimpleOptionDialogueRecordsForIds(IEnumerable<string> ids, int preOptionLineCount)
        {
            return ids.ToDictionary(id => id, id => SimpleDialogueMocks.GetSimpleOption(id, preOptionLineCount));
        }
    }
}