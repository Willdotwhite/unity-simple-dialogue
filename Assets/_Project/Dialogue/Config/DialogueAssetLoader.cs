﻿using System.Collections.Generic;
using System.IO;
using _Project.Dialogue.Lines;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Dialogue.Config
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
                DialogueFileConfig fileConfig = JsonConvert.DeserializeObject<DialogueFileConfig>(asset.ToString());

                List<DialogueLine> lines = new List<DialogueLine>();
                foreach (DialogueLineConfig line in fileConfig.dialogueLines)
                {
                    lines.Add(DialogueLineFactory.FromConfig(line));
                }

                DialogueRecord record = new DialogueRecord();
                record.id = fileConfig.id;
                record.dialogueLines = lines;

                Records.Add(record.id, record);
            }
        }

    }
}
