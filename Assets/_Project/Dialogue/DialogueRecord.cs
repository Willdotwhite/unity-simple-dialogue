using System.Collections.Generic;

namespace _Project.Dialogue
{
    [System.Serializable]
    public class DialogueRecord
    {
        public string id;

        public List<DialogueLine> entries;
    }
}