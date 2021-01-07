using _Project.Dialogue;
using _Project.Dialogue.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Examples
{
    public class SimpleExampleConversation : MonoBehaviour
    {
        private DialogueSystem _dialogueSystem;

        [SerializeField] private Text _text;

        private GameObject _nextButton;

        // Start is called before the first frame update
        private void Start()
        {
            _dialogueSystem = new DialogueSystem("Dialogue/SimpleExample", "example-conversation-1");

            UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);

            // Dirty hack to wire up the Next Button without needing one per Conversation
            _nextButton = GameObject.Find("Next Button");
            _nextButton.GetComponent<Button>().onClick.AddListener(OnNextDialogueLine);
        }

        private void OnNextDialogueLine()
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
