using _Project.Dialogue;
using _Project.Dialogue.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Examples
{
    public class ExampleConversation : MonoBehaviour
    {
        private DialogueSystem _dialogueSystem;

        [SerializeField] private Text _text;

        // Start is called before the first frame update
        private void Start()
        {
            _dialogueSystem = new DialogueSystem("Dialogue", "example-conversation-1");

            UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
        }

        public void OnNextDialogueLine()
        {
            if (_dialogueSystem.StepToNextDialogueLine())
            {
                UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
            }
        }

        private void UpdateDialogueLine(DialogueLine line)
        {
            SpokenDialogueLine spokenLine = (SpokenDialogueLine) line;
            _text.text = $"{_text.text}\n{spokenLine.Speaker}: {spokenLine.Dialogue}";
        }
    }
}
