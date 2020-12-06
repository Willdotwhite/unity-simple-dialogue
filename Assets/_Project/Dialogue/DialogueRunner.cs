using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Dialogue
{
    public class DialogueRunner
    {
        private Dictionary<string, DialogueRecord> Records { get; }

        public DialogueRecord CurrentRecord { get; private set; }

        public DialogueRunner(Dictionary<string, DialogueRecord> records)
        {
            Records = records;
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
        }

        /// <summary>
        /// Step through to "next"
        /// </summary>
        public void StepToNextDialogueLine()
        {
            // If at end of current Record, get next Record
            if (CurrentRecord.IsAtEndOfRecord)
            {
                DialogueLine currentLine = CurrentRecord.CurrentDialogueLine;
                string nextRecordId = currentLine.next;
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
