using System.Collections.Generic;
using UnityEngine;

namespace _Project.Dialogue
{
    public class DialogueAssetLoader
    {
        public Dictionary<string, DialogueRecord> Records { get; } = new Dictionary<string, DialogueRecord>();

        public DialogueAssetLoader(string filepath)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(filepath);

            foreach (TextAsset asset in textAssets)
            {
                DialogueRecord record = JsonUtility.FromJson<DialogueRecord>(asset.ToString());
                Records.Add(record.id, record);
            }
        }

        // TODO: Set conversation start

        // TODO: Step through conversation

    }
}
