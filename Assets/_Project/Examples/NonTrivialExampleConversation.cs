using System;
using System.Collections.Generic;
using _Project.Dialogue;
using _Project.Dialogue.Config;
using _Project.Dialogue.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Examples
{
    public class NonTrivialExampleConversation : MonoBehaviour
    {
        private DialogueSystem _dialogueSystem;

        [SerializeField] private Text _text;

        [SerializeField] private GameObject _redButton, _greenButton, _blueButton, _nextButton;

        private bool _colourButtonsAreActive = false;

        private void Awake()
        {
            ToggleColourButtons(false);
        }

        // Start is called before the first frame update
        private void Start()
        {
            Dictionary<string, Action<CommandParameters>> dialogueActions = new Dictionary<string, Action<CommandParameters>>();
            dialogueActions.Add("_toggle_colour_buttons", _ =>
            {
                _colourButtonsAreActive = !_colourButtonsAreActive;

                ToggleColourButtons(_colourButtonsAreActive);
                _nextButton.SetActive(!_colourButtonsAreActive);
            });
            dialogueActions.Add("_background_colour_change", _params =>
            {
                float r = (int)_params["r"] / 255.0f;
                float g = (int) _params["g"] / 255.0f;
                float b = (int) _params["b"] / 255.0f;

                Camera.main.backgroundColor = new Color(r, g, b);
            });

            _dialogueSystem = new DialogueSystem(
                "Dialogue",
                "non-trivial-conversation::start",
                null,
                dialogueActions
            );

            UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
        }

        public void OnNextDialogueLine()
        {
            if (_dialogueSystem.StepToNextDialogueLine())
            {
                UpdateDialogueLine(_dialogueSystem.CurrentDialogueLine);
            }
        }

        public void OnColourChosen(string colour)
        {
            string nextDialogueBranch = $"non-trivial-conversation::colour-option::{colour}";
            // TODO: How was I going to choose which option to go down?
            // How do I set an option.next as the next target?
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
                // This ID lookup for Options[0], Options[1] etc. isn't ideal - this will be getting changed soon
                _redButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[0].Dialogue;
                _greenButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[1].Dialogue;
                _blueButton.GetComponentInChildren<Text>().text = optionsDialogueLine.Options[2].Dialogue;
            }
        }

        private void ToggleColourButtons(bool isVisible)
        {
            _redButton.SetActive(isVisible);
            _greenButton.SetActive(isVisible);
            _blueButton.SetActive(isVisible);
        }
    }
}