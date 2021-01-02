using System.Collections.Generic;
using _Project.Dialogue.Lines;
using UnityEngine.Events;

namespace _Project.Dialogue
{
    /// <summary>
    /// A DialogueRecord is a collection of dialogue lines grouped together,
    /// along with the logical rules for how this chunk of a conversation/narrative can flow.
    /// <para>
    /// It is expected that a single conversation will be broken into multiple records.
    /// This allows for easier management of branching conversations and game commands.
    /// </para>
    /// </summary>
    ///  TODO: REMOVE AND REFACTOR INTO TREE
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
        public List<IDialogueLine> dialogueLines;

        /// <summary>
        /// Array index of the current line of dialogue
        /// TODO: We're going to need to reset this when we build loops
        /// </summary>
        private int currentLineId = 0;

        /// <summary>
        /// Current DialogueLine
        /// </summary>
        public IDialogueLine CurrentDialogueLine => dialogueLines[currentLineId];

        /// <summary>
        /// Is the current line the last dialogueLine of this record?
        /// <para>
        /// Note: This will be true if a record only contains a single dialogueLine!
        /// </para>
        /// </summary>
        public bool IsAtEndOfRecord => currentLineId == dialogueLines.Count - 1;

        public Dictionary<string, UnityEvent> Commands;

        /// <summary>
        /// Step to next DialogueLine in this record
        /// </summary>
        public void StepToNextDialogueLine()
        {
            currentLineId++;

            if (CurrentDialogueLine is CommandDialogueLine currentCommandDialogueLine)
            {
                // CommandDialogueLine currentCommandDialogueLine = (CommandDialogueLine) CurrentDialogueLine;
                if (Commands.ContainsKey(currentCommandDialogueLine.command))
                {
                    Commands[currentCommandDialogueLine.command].Invoke();
                }
            }
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