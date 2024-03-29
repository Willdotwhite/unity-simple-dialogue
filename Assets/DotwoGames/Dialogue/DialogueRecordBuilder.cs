﻿using System.Collections.Generic;
using System.Linq;
using DotwoGames.Dialogue.Analysis;
using DotwoGames.Dialogue.Lines;

namespace DotwoGames.Dialogue
{
    public class DialogueRecordBuilder
    {
        /// <summary>
        /// Records (files) that this Runner can run through
        /// </summary>
        private Dictionary<string, DialogueRecord> Records;

        /// <summary>
        /// Counter for autogenerated branches, ensures unique names for auto Records
        /// <para>
        /// Used primarily for ease of debugging
        /// </para>
        /// </summary>
        private int _branchIdx;

        public static Dictionary<string, DialogueRecord> Build(Dictionary<string, DialogueRecord> records)
        {
            DialogueRecordBuilder builder = new DialogueRecordBuilder();
            builder.SetAndGenerateRecords(records);

            // Validate records are valid before continuing?
            OnLoadAnalyser.CheckRecords(builder.Records);

            return builder.Records;
        }


        /// <summary>
        /// Set given dialogue records, and generate internal records if necessary
        /// </summary>
        /// <param name="records"></param>
        private void SetAndGenerateRecords(Dictionary<string, DialogueRecord> records)
        {
            Records = records;

            // Traverse dialogue lines to create any runtime(?) Records
            foreach (DialogueRecord record in records.Values.ToList())
            {
                // Reset branch naming for current record
                _branchIdx = 1;
                BuildRuntimeDialogueRecords(record.dialogueLines, record);
            }
        }

        /// <summary>
        /// Build any DialogueRecords that haven't been written by user, but are needed during runtime
        /// <para>
        /// Currently, this is online inline OptionsDialogueRecords
        /// </para>
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="record"></param>
        private void BuildRuntimeDialogueRecords(IEnumerable<DialogueLine> lines, DialogueRecord record)
        {
            foreach (OptionsDialogueLine line in lines.OfType<OptionsDialogueLine>())
            {
                GenerateInlineDialogueBranchRecords(line, record);
            }
        }

        /// <summary>
        /// Generate inline DialogueRecords from nested OptionsDialogueRecords branches
        /// </summary>
        /// <param name="line"></param>
        /// <param name="record"></param>
        private void GenerateInlineDialogueBranchRecords(OptionsDialogueLine line, DialogueRecord record)
        {
            foreach (OptionBranchDialogueLine option in line.Options.OfType<OptionBranchDialogueLine>())
            {
                // Create internal record to manage inline dialogue branch as anonymous DialogueRecord
                string branchId = $"{record.id}-option-branch-{_branchIdx++}-autogenerated";

                Records.Add(branchId, new DialogueRecord(branchId, option.Branch));

                // Build nested structures by recursively looping through inline branches
                BuildRuntimeDialogueRecords(option.Branch, record);

                // Remove Branch to keep the format of option branches the same;
                // the branch format is just a shorthand for the standard format
                option.Branch = null;
                option.Next = branchId;
            }
        }
    }
}