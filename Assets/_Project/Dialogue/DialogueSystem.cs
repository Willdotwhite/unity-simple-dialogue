﻿using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;

namespace _Project.Dialogue
{
    /// <summary>
    /// DialogueSystem is the top level class for this system
    /// <para>
    /// Unless you need some functionality that isn't possible from this class, you should only ever need to use
    /// this class and the public properties/methods is exposes
    /// </para>
    /// <para>
    /// This design is quite common and is intended to hide away complexity from the user of an API (the developer, not
    /// the player) - if I make a massive change to the system in future, nothing will break for users who are just
    /// using this class as long as the public parts like CurrentDialogueLine still exist and return the same type
    /// of objects
    /// </para>
    /// </summary>
    public class DialogueSystem
    {
        /// <inheritdoc cref="DialogueRunner.CurrentDialogueLine" />
        public DialogueLine CurrentDialogueLine => _dialogueRunner.CurrentDialogueLine;

        /// <inheritdoc cref="DialogueRunner.StepToNextDialogueLine" />
        public bool StepToNextDialogueLine(DialogueLine targetDialogueLine = null) => _dialogueRunner.StepToNextDialogueLine(targetDialogueLine);

        /// <summary>
        /// Internal handler for moving through DialogueRecords
        /// </summary>
        private readonly DialogueRunner _dialogueRunner;

        public DialogueSystem(
            string filepath,
            string startingRecordId = null,
            DialogueParser parser = null,
            Dictionary<string, Action<CommandParameters>> commands = null
        )
        {
            DialogueAssetLoader assetLoader = new DialogueAssetLoader(filepath);
            _dialogueRunner = new DialogueRunner(assetLoader.Records, parser, commands);

            SetStartingRecord(startingRecordId, assetLoader);
        }

        /// <summary>
        /// Try to set the initial DialogueRecord for the starting state of the system
        /// </summary>
        /// <param name="startingRecordId"></param>
        /// <param name="assetLoader"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetStartingRecord(string startingRecordId, DialogueAssetLoader assetLoader)
        {
            // Set record if explicitly stated
            if (startingRecordId != null)
            {
                _dialogueRunner.SetCurrentRecord(startingRecordId);
                return;
            }

            // If we're being left to work out what record is desired, and there's only one option, set that
            if (assetLoader.Records.Count > 1)
            {
                throw new ArgumentException("startingRecordId parameter is required, can't resolve automatically!");
            }

            DialogueRecord onlyRecord = assetLoader.Records.First().Value;
            _dialogueRunner.SetCurrentRecord(onlyRecord.id);
        }
    }
}