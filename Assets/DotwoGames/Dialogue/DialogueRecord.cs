using System;
using System.Collections.Generic;
using DotwoGames.Dialogue.Config;
using DotwoGames.Dialogue.Lines;

namespace DotwoGames.Dialogue
{
    /// <summary>
    /// A DialogueRecord is a collection of dialogue lines grouped together,
    /// along with the logical rules for how this chunk of a conversation/narrative can flow.
    ///
    /// <para>
    /// Each JSON file will be represented as a single DialogueRecord
    /// </para>
    ///
    /// <para>
    /// It is expected that a single conversation will be broken into multiple records.
    /// This allows for easier management of branching conversations and game commands.
    /// </para>
    /// </summary>
    public class DialogueRecord
    {
        /// <summary>
        /// Unique ID of this record
        /// <para>
        /// Can be anything that uniquely represents a Record,
        /// but it is recommended to pick something human-readable
        /// </para>
        /// </summary>
        public string id;

        /// <summary>
        /// All dialogue lines for the current record
        /// </summary>
        public List<DialogueLine> dialogueLines;

        /// <summary>
        /// Array index of the current line of dialogue
        /// </summary>
        private int currentLineId;

        /// <summary>
        /// Current DialogueLine
        /// </summary>
        public DialogueLine CurrentDialogueLine => dialogueLines[currentLineId];

        /// <summary>
        /// Is the current line the last dialogueLine of this record?
        /// <para>
        /// Note: This will be true if a record only contains a single dialogueLine!
        /// </para>
        /// </summary>
        public bool IsAtEndOfRecord => currentLineId == dialogueLines.Count - 1;

        /// <summary>
        /// Is the current line the first dialogueLine of this record?
        /// <para>
        /// Note: This will be true if a record only contains a single dialogueLine!
        /// </para>
        /// </summary>
        public bool IsAtStartOfRecord => currentLineId == 0;

        public Dictionary<string, Action<CommandParameters>> Commands;

        public DialogueRecord(string id, List<DialogueLine> dialogueLines)
        {
            this.id = id;
            this.dialogueLines = dialogueLines;
        }

        /// <summary>
        /// Step to next DialogueLine in this record
        /// </summary>
        public void StepToNextDialogueLine()
        {
            currentLineId++;
        }

        /// <summary>
        /// Reset any previous state on this Record
        /// <para>
        /// This allows us to walk the same Record multiple times
        /// </para>
        /// </summary>
        public void Reset()
        {
            currentLineId = 0;
        }
    }
}