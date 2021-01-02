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

            DialogueLine firstLine = (DialogueLine) _dialogueRunner.CurrentRecord.CurrentDialogueLine; // TODO: Squish
            UpdateDialogueLine(firstLine);
        }

        public void OnNextDialogueLine()
        {
            _dialogueRunner.StepToNextDialogueLine();
            UpdateDialogueLine((DialogueLine) _dialogueRunner.CurrentRecord.CurrentDialogueLine);
        }

        private void UpdateDialogueLine(DialogueLine line)
        {
            _text.text = $"{_text.text}\n{line.speaker}: {line.dialogue}";
        }
    }
}
