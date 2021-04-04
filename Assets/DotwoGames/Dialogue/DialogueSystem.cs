using System;
using System.Collections.Generic;
using System.Linq;
using DotwoGames.Dialogue.Config;
using DotwoGames.Dialogue.Lines;
using UnityEngine;

namespace DotwoGames.Dialogue
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
        ///
        /// <para>
        /// Because CommandDialogueLines are automatically stepped over, this can never return a CommandDialogueLine.
        /// Therefore (until another base-level class is introduced) this is the lowest meaningful class cast.
        /// </para>
        public SpokenDialogueLine CurrentDialogueLine => (SpokenDialogueLine) _dialogueRunner.CurrentDialogueLine;

        /// <summary>
        /// <inheritdoc cref="DialogueRunner.IsAtEndOfDialogue"/>
        /// </summary>
        public bool IsAtEndOfDialogue => _dialogueRunner.IsAtEndOfDialogue;

        /// <inheritdoc cref="DialogueRunner.StepToNextDialogueLine" />
        public bool StepToNextDialogueLine(string targetDialogueLine = null) => _dialogueRunner.StepToNextDialogueLine(targetDialogueLine);

        public void Reset() => _dialogueRunner.SetCurrentRecord(resolvedStartingRecordId);

        /// <summary>
        /// Internal handler for moving through DialogueRecords
        /// </summary>
        private readonly DialogueRunner _dialogueRunner;

        /// <summary>
        /// Record ID that determined as the starting ID
        /// </summary>
        private readonly string resolvedStartingRecordId;

        public DialogueSystem(
            string filepath,
            string startingRecordId = null,
            DialogueParser parser = null,
            Dictionary<string, Action<CommandParameters>> commands = null
        )
        {
            DialogueAssetLoader assetLoader = new DialogueAssetLoader(filepath);
            _dialogueRunner = new DialogueRunner(assetLoader.Records, parser, commands);

            resolvedStartingRecordId = GetStartingRecordId(startingRecordId, assetLoader);
            _dialogueRunner.SetCurrentRecord(resolvedStartingRecordId);
        }

        // TODO: Consider expanding this, if I want further Editor functionality
        // TODO: Expand to load all files in directory?
        public DialogueSystem(TextAsset textAsset)
        {
            DialogueAssetLoader assetLoader = new DialogueAssetLoader(textAsset);
            _dialogueRunner = new DialogueRunner(assetLoader.Records);

            resolvedStartingRecordId = GetStartingRecordId(null, assetLoader);
            _dialogueRunner.SetCurrentRecord(resolvedStartingRecordId);
        }

        /// <summary>
        /// Get the ID of the starting record
        /// </summary>
        /// <param name="startingRecordId"></param>
        /// <param name="assetLoader"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private string GetStartingRecordId(string startingRecordId, DialogueAssetLoader assetLoader)
        {
            // Set record if explicitly stated
            if (startingRecordId != null)
            {
                return startingRecordId;
            }

            // If we're being left to work out what record is desired, and there's only one option, set that
            if (assetLoader.Records.Count > 1)
            {
                throw new ArgumentException("startingRecordId parameter is required, can't resolve automatically!");
            }

            DialogueRecord onlyRecord = assetLoader.Records.First().Value;
            return onlyRecord.id;
        }
    }
}