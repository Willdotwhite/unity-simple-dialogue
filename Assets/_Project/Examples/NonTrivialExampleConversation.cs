using System;
using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Examples
{
    public class NonTrivialExampleConversation : MonoBehaviour
    {
        private DialogueSystem _dialogueSystem;

        [SerializeField] private Text _text;

        [SerializeField] private GameObject _redButton, _greenButton, _blueButton, _nextButton;

        private bool _colourButtonsAreActive;

        private void Awake()
        {
            ToggleColourButtons(false);
        }

        // Start is called before the first frame update
        private void Start()
        {
            _dialogueSystem = new DialogueSystem(
                "Dialogue/NonTrivialExample",
                "non-trivial-conversation::start",
                BuildDialogueParser(),
                BuildDialogueActions()
            );

            UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);

            // Dirty hack to wire up the Next Button without needing one per Conversation
            _nextButton = GameObject.Find("Next Button");
            _nextButton.GetComponent<Button>().onClick.AddListener(OnNextDialogueLine);
        }

        private void Update()
        {
            // Reload on spacebar for ease of testing/review
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void OnNextDialogueLine()
        {
            if (_dialogueSystem.StepToNextDialogueLine())
            {
                UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
            }
        }

        public void OnColourChosen(string colour)
        {
            // Each button passes a string of either 'red' 'green' or 'blue'
            string nextDialogueBranch = $"non-trivial-conversation::colour-option::{colour}";

            // Move to the chosen Option DialogueLine
            OptionsDialogueLine currentOptionDialogueLine = (OptionsDialogueLine) _dialogueSystem.CurrentDialogueLine;
            DialogueLine chosenOption = currentOptionDialogueLine.GetOptionByNext(nextDialogueBranch);

            _dialogueSystem.StepToNextDialogueLine(chosenOption);
            UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
        }

        private void UpdateDialogueLine(DialogueLine line)
        {
            SpokenDialogueLine spokenLine = (SpokenDialogueLine) line;
            _text.text = $"{_text.text}\n{spokenLine.Speaker}: {spokenLine.Dialogue}";

            if (line is OptionsDialogueLine optionsDialogueLine)
            {
                _redButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[0].Dialogue;
                _greenButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[1].Dialogue;
                _blueButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[2].Dialogue;
            }
        }

        private DialogueParser BuildDialogueParser()
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("npc::dev", "Dotwo");

            return new DialogueParser(replacements);
        }

        private Dictionary<string, Action<CommandParameters>> BuildDialogueActions()
        {
            Dictionary<string, Action<CommandParameters>> dialogueActions =
                new Dictionary<string, Action<CommandParameters>>
                {
                    {
                        "_toggle_colour_buttons", _ =>
                        {
                            _colourButtonsAreActive = !_colourButtonsAreActive;

                            ToggleColourButtons(_colourButtonsAreActive);
                            _nextButton.SetActive(!_colourButtonsAreActive);
                        }
                    },
                    {
                        "_background_colour_change", _params =>
                        {
                            float r = (int) _params["r"] / 255.0f;
                            float g = (int) _params["g"] / 255.0f;
                            float b = (int) _params["b"] / 255.0f;

                            Camera.main.backgroundColor = new Color(r, g, b);
                        }
                    }
                };

            return dialogueActions;
        }

        private void ToggleColourButtons(bool isVisible)
        {
            _redButton.SetActive(isVisible);
            _greenButton.SetActive(isVisible);
            _blueButton.SetActive(isVisible);
        }
    }
}