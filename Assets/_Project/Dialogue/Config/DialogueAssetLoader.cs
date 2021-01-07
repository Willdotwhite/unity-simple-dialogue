using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Dialogue.Lines;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Dialogue.Config
{
    /// <summary>
    /// This class loads the JSON files as TextAssets from disc
    /// </summary>
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
                DialogueFileConfig fileConfig = JsonConvert.DeserializeObject<DialogueFileConfig>(asset.ToString());
                List<DialogueLine> lines = fileConfig.dialogueLines.Select(DialogueLineFactory.FromConfig).ToList();
                DialogueRecord record = new DialogueRecord(fileConfig.id, lines);

                Records.Add(record.id, record);
            }
        }

    }
}
