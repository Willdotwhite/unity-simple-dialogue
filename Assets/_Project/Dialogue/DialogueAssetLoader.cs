using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Project.Dialogue
{
    public class DialogueAssetLoader
    {
        public Dictionary<string, DialogueRecord> Records { get; } = new Dictionary<string, DialogueRecord>();

        public DialogueAssetLoader(string filepath)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(filepath);

            if (textAssets.Length == 0)
            {
                throw new FileLoadException($"Attempted to load Dialogue assets at {filepath} but nothing was found!");
            }

            foreach (TextAsset asset in textAssets)
            {
                DialogueRecord record = JsonUtility.FromJson<DialogueRecord>(asset.ToString());
                Records.Add(record.id, record);
            }
        }

    }
}
