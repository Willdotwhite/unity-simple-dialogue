using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Examples
{
    public class ExampleConversation : MonoBehaviour
    {
        private DialogueRunner _dialogueRunner;

        [SerializeField] private Text _text;


        // Start is called before the first frame update
        private void Start()
        {
            DialogueAssetLoader assetLoader = new DialogueAssetLoader("Dialogue");
            _dialogueRunner = new DialogueRunner(assetLoader.Records);
            _dialogueRunner.SetCurrentRecord("example-conversation-1");

            UpdateDialogueLine(_dialogueRunner.CurrentDialogueLine);
        }

        public void OnNextDialogueLine()
        {
            if (_dialogueRunner.StepToNextDialogueLine())
            {
                UpdateDialogueLine(_dialogueRunner.CurrentDialogueLine);
            }
        }

        private void UpdateDialogueLine(DialogueLine line)
        {
            SpokenDialogueLine spokenLine = (SpokenDialogueLine) line;
            _text.text = $"{_text.text}\n{spokenLine.Speaker}: {spokenLine.Dialogue}";
        }
    }
}
