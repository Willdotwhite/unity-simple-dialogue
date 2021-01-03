using System;
using System.Collections.Generic;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

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
        public IDialogueLine CurrentDialogueLine => CurrentRecord.CurrentDialogueLine;

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

            CurrentRecord = Records[recordId];
            CurrentRecord.Reset();

            CurrentRecord.Commands = _commands;
        }

        /// <summary>
        /// Step through to "next"
        /// </summary>
        public void StepToNextDialogueLine()
        {
            // If at end of current Record, get next Record
            if (CurrentRecord.IsAtEndOfRecord)
            {
                // TODO: Fix if command is EOF
                DialogueLine currentLine = (DialogueLine) CurrentRecord.CurrentDialogueLine;
                string nextRecordId = currentLine.Next;
                if (nextRecordId == null)
                {
                    // Best handling practice here?
                    Debug.LogWarning("DialogueRunner has hit end of dialogue");
                    return;
                }

                SetCurrentRecord(nextRecordId);
                return;
            }

            // Else, just step along
            CurrentRecord.StepToNextDialogueLine();
        }

    }
}
