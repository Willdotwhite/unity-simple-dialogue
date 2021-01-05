using System;
using System.Collections.Generic;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Dialogue
{
    public class DialogueRunner
    {
        /// <summary>
        /// Records (files) that this Runner can run through
        /// </summary>
        private Dictionary<string, DialogueRecord> Records { get; }

        /// <summary>
        /// CurrentRecord being used for Dialogue
        /// </summary>
        public DialogueRecord CurrentRecord { get; private set; }

        /// <summary>
        /// CurrentDialogueLine from the CurrentRecord
        /// </summary>
        public DialogueLine CurrentDialogueLine => CurrentRecord.CurrentDialogueLine;

        public bool HasNextLine { get; private set; }

        /// <summary>
        /// Command look-up from string representation to UnityEvent
        /// </summary>
        private readonly Dictionary<string, Action<CommandParameters>> _commands;

        public DialogueRunner(
            Dictionary<string, DialogueRecord> records, // TODO: tidy this up with asset loading?
            [CanBeNull] DialogueParser parser = null,
            [CanBeNull] Dictionary<string, Action<CommandParameters>> commands = null
        )
        {
            Records = records;
            parser?.Parse(Records);
            _commands = commands;
        }

        /// <summary>
        /// Set the current record for the DialogueRunner to read from
        /// <para>
        /// Set the current record to the start of a conversation
        /// </para>
        /// </summary>
        /// <param name="recordId"></param>
        // TODO: Should this be called automatically if you only load a single file?
        public void SetCurrentRecord(string recordId)
        {
            Assert.IsNotNull(Records[recordId]);

            // TODO: Throw exception if recordId not exist
            CurrentRecord = Records[recordId];
            CurrentRecord.Reset();

            CurrentRecord.Commands = _commands;

            // Should I check if this isn't empty?
            HasNextLine = true;
        }

        /// <summary>
        /// Step through to "next"
        /// </summary>
        public void StepToNextDialogueLine()
        {
            // Check for next Line being a Command, evaluate until non-Command line
            EvaluateCurrentRecordForCommands();

            // Make next non-Command line CurrentDialogueLine
            Step();
        }

        /// <summary>
        /// Step to next line, walking to next Record if necessary
        /// </summary>
        private void Step()
        {
            // If at end of current Record, get next Record
            if (CurrentRecord.IsAtEndOfRecord)
            {
                string nextRecordId = CurrentRecord.CurrentDialogueLine.Next;
                if (nextRecordId == null)
                {
                    // Best handling practice here?
                    Debug.LogWarning("DialogueRunner has hit end of dialogue");
                    HasNextLine = false;

                    return;
                }

                SetCurrentRecord(nextRecordId);
                return;
            }

            // Else, just step along
            CurrentRecord.StepToNextDialogueLine();
        }

        // TODO: Move into Record
        private void EvaluateCurrentRecordForCommands()
        {
            while (CurrentDialogueLine is CommandDialogueLine commandDialogueLine)
            {
                if (!CurrentRecord.Commands.ContainsKey(commandDialogueLine.Command))
                {
                    // TODO: BETTER
                    throw new ArgumentOutOfRangeException($"{commandDialogueLine.Command} has not been defined in Command list for Dialogue");
                }

                // Invoke Command with Parameters
                CurrentRecord.Commands[commandDialogueLine.Command].Invoke(commandDialogueLine.Params);

                // Walk to next line in Record/next Record to handle multiple Commands in sequence
                Step();

                // Escape infinite loop if CommandDialogueLine is last command of last Record
                if (!HasNextLine)
                {
                    break;
                }
            }
        }

    }
}
