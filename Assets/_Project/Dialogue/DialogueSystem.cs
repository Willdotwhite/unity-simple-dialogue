using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using JetBrains.Annotations;

namespace _Project.Dialogue
{
    public class DialogueSystem
    {
        /// <inheritdoc cref="DialogueRunner.CurrentDialogueLine" />
        public DialogueLine CurrentDialogueLine => _dialogueRunner.CurrentDialogueLine;

        /// <inheritdoc cref="DialogueRunner.StepToNextDialogueLine" />
        public bool StepToNextDialogueLine([CanBeNull] DialogueLine targetDialogueLine = null) => _dialogueRunner.StepToNextDialogueLine(targetDialogueLine);

        /// <summary>
        /// Internal handler for moving through DialogueRecords
        /// </summary>
        private readonly DialogueRunner _dialogueRunner;

        public DialogueSystem(
            string filepath,
            [CanBeNull] string startingRecordId,
            [CanBeNull] DialogueParser parser = null,
            [CanBeNull] Dictionary<string, Action<CommandParameters>> commands = null
        )
        {
            DialogueAssetLoader assetLoader = new DialogueAssetLoader(filepath);
            _dialogueRunner = new DialogueRunner(assetLoader.Records, parser, commands);

            // Set record if explicitly stated
            if (startingRecordId != null)
            {
                _dialogueRunner.SetCurrentRecord(startingRecordId);
                return;
            }

            // If we're being left to work out what record is desired, and there's only one option, set that
            if (assetLoader.Records.Count == 1)
            {
                DialogueRecord onlyRecord = assetLoader.Records.First().Value;
                _dialogueRunner.SetCurrentRecord(onlyRecord.id);
                return;
            }

            throw new ArgumentException("startingRecordId parameter is required, can't resolve automatically!");
        }
    }
}