using System.Collections.Generic;

namespace _Project.Dialogue
{
    [System.Serializable]
    public class DialogueRecord
    {
        public string id;

        public List<DialogueLine> entries;

        private int currentEntryId = 0;

        public DialogueLine CurrentDialogueLine => entries[currentEntryId];

        public bool IsAtEndOfRecord => currentEntryId == entries.Count - 1;

        public void StepToNextDialogueLine()
        {
            currentEntryId++;
        }
    }
}