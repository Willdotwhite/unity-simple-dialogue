using System.Collections.Generic;
using UnityEngine;

namespace _Project.Dialogue
{
    public class DialogueAssetLoader
    {
        public List<DialogueEntry> Entries { get; }

        public DialogueAssetLoader(string filepath)
        {
            TextAsset file = Resources.Load<TextAsset>(filepath.Replace(".json", ""));
            DialogueEntryWrapper wrapper = JsonUtility.FromJson<DialogueEntryWrapper>(file.ToString());

            Entries = wrapper.entries;
        }

    }
}
