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
        /// CurrentDialogueLine from the _currentRecord
        /// </summary>
        public DialogueLine CurrentDialogueLine => CurrentRecord.CurrentDialogueLine;

        /// <summary>
        /// Does the Runner have a valid DialogueLine it can reach?
        /// </summary>
        private bool _hasNextLine;

        /// <summary>
        /// Command look-up from string representation to UnityEvent
        /// </summary>
        private readonly Dictionary<string, Action<CommandParameters>> _commands;

        public DialogueRunner(
            Dictionary<string, DialogueRecord> records,
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
        public void SetCurrentRecord(string recordId)
        {
            if (!Records.ContainsKey(recordId))
            {
                throw new KeyNotFoundException($"Could not set current record to {recordId}, no record of that ID exists");
            }

            CurrentRecord = Records[recordId];
            CurrentRecord.Reset();

            CurrentRecord.Commands = _commands;

            // Should I check if this isn't empty?
            _hasNextLine = true;
        }

        /// <summary>
        /// Step to the next line of dialogue and return whether this was successful
        /// </summary>
        /// <returns>Stepped to new DialogueLine, or is stuck at EOF</returns>
        public bool StepToNextDialogueLine([CanBeNull] DialogueLine targetDialogueLine = null)
        {
            if (!_hasNextLine)
            {
                return false;
            }

            // Check for the first line of dialogue being a Command,
            // because the automatic event firing will cause multiple steps to happen at once
            if (CurrentRecord.IsAtStartOfRecord)
            {
                EvaluateCurrentRecordForCommands();
            }

            bool steppedToNextLine = Step(targetDialogueLine);
            if (steppedToNextLine)
            {
                // Check for next Line being a Command, evaluate until non-Command line
                EvaluateCurrentRecordForCommands();
            }

            // Make next non-Command line CurrentDialogueLine
            return steppedToNextLine;
        }

        /// <summary>
        /// Step to next line, walking to next Record if necessary
        /// <para>
        /// Same as StepToNextDialogueLine, return if Step has found a new DialogueLine
        /// </para>
        /// <returns>Stepped to new DialogueLine, or is stuck at EOF</returns>
        /// </summary>
        private bool Step([CanBeNull] DialogueLine targetDialogueLine = null)
        {
            // If you're stepping through a record, just keep going
            if (!CurrentRecord.IsAtEndOfRecord)
            {
                // Else, just step along
                CurrentRecord.StepToNextDialogueLine();
                return true;
            }

            string nextRecordId = CurrentDialogueLine.Next;

            // If at end of current Record, get next Record
            if (CurrentDialogueLine is OptionsDialogueLine)
            {
                if (targetDialogueLine == null)
                {
                    Debug.LogWarning("DialogueRunner is waiting for OptionsDialogueLine option, and cannot continue");
                    return false;
                }

                nextRecordId = targetDialogueLine.Next;
            }

            if (nextRecordId == null)
            {
                // Best handling practice here?
                _hasNextLine = false;
                Debug.LogWarning("DialogueRunner has hit end of dialogue");
                return false;
            }

            SetCurrentRecord(nextRecordId);
            return true;
        }

        /// <summary>
        /// Attempt to run (sequential) CurrentDialogueLine Command(s), by running all commands in order and
        /// exiting when stepping to non-Command line
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void EvaluateCurrentRecordForCommands()
        {
            while (CurrentDialogueLine is CommandDialogueLine commandDialogueLine)
            {
                if (CurrentRecord.Commands == null)
                {
                    throw new ArgumentOutOfRangeException($"{CurrentRecord.id} has no commands set, but is trying to fire {commandDialogueLine.Command}. Did you forget to add your commands?");
                }

                if (!CurrentRecord.Commands.ContainsKey(commandDialogueLine.Command))
                {
                    throw new ArgumentOutOfRangeException($"{commandDialogueLine.Command} has not been defined in Command list for {CurrentRecord.id}");
                }

                // Invoke Command with Parameters
                CurrentRecord.Commands[commandDialogueLine.Command].Invoke(commandDialogueLine.Params);

                // Walk to next line in Record/next Record to handle multiple Commands in sequence
                Step();

                // Escape infinite loop if CommandDialogueLine is last command of last Record
                if (!_hasNextLine)
                {
                    break;
                }
            }
        }

    }
}
