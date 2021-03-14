# Simple JSON Dialogue System

## Overview

The Simple JSON Dialogue System is a tool to structure and run game dialogue in Unity without the need for a visual editor. The package uses structured JSON files linked together by custom IDs to create dialogue trees that can be easily stepped through with only a few lines of code.



## Installation

[Follow the Unity docs guide on installing a package from a git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html). The git URL to use is **git@bitbucket.org:WillDotWhite/unity-dialogue-system.git**.



## Quick Start

### Example 1: Simple Linear Dialogue

```cs
// Create a new instance of DialogueSystem by passing it the path to your JSON file
// In this example, the folder only contains one JSON file (see below for multiple files)
DialogueSystem dialogueSystem = new DialogueSystem("Dialogue/YourFilePath");

// Use .CurrentDialogueLine to get the current line of dialogue
// (See X for why this is a SpokenDialogueLine instance)
SpokenDialogueLine line = _dialogueSystem.CurrentDialogueLine;
Debug.Log($"{line.speaker} says {line.dialogue}");

// .StepToNextDialogueLine() tries to move to the next line of dialogue
// It returns true if this successful, and false if it wasn't able to step properly
// (e.g. you're at the end of the dialogue)
if (_dialogueSystem.StepToNextDialogueLine())
{
    // We're now at the next line of dialogue!
    SpokenDialogueLine nextLine = _dialogueSystem.CurrentDialogueLine;
    Debug.Log($"{nextLine.speaker} says {nextLine.dialogue}");
}
```

```json
// Assets/.../Resources/Dialogue/YourFilePath/example-conversation.json
{
  // This ID can be anything, choose something that makes sense to you
  // The ID of a set of lines is how you move from file to file
  "id": "example-conversation-1",
  "dialogueLines": [
    {
      "speaker": "dotwo",
      "dialogue": "Hello there!"
    },
    {
      "speaker": "dotwo",
      "dialogue": "Thank you for using the Simple JSON Dialogue System."
    },
    {
      "speaker": "dotwo",
      "dialogue": "I hope you find this fun and easy to use!"
    }
  ]
}
```



### Example 2: Starting At A Specific Record

Each JSON file must declare a unique ID at the start of the file - this ID is used to link the dialogue tree together, and is how you define what chunk of your dialogue to start at:

```csharp
// Create a new instance of DialogueSystem by passing it the path to your JSON file
// and the ID of the chunk to start with - this isn't neccesarily the name of the file!
DialogueSystem dialogueSystem = new DialogueSystem("Dialogue/YourFilePath", "barkeeper-introduction");

```

This can be omitted if the first parameter points to a directory with only one file in (like in example 1), but will throw an error otherwise.

```json
// Assets/.../Resources/Dialogue/YourFilePath/barkeeper-npc-intro.json
{
  "id": "barkeeper-introduction",
  "dialogueLines": [
    {
      // ...
    }
  ]
}
```

```json
// Assets/.../Resources/Dialogue/YourFilePath/barkeeper-general.json
{
  "id": "barkeeper-general-dialogue",
  "dialogueLines": [
    {
      // ...
    }
  ]
}
```



### Example 3: Moving Between Files

The **DialogueSystem** class automatically moves between files by taking the `next` value for a given **DialogueLine** and moving to the file which has that ID declared at the top:

```json
// Assets/.../Resources/Dialogue/YourFilePath/moving-conversation-1.json
{
  "id": "moving-conversation-1",
  "dialogueLines": [
    {
      "speaker": "npc_1",
      "dialogue": "This line has come from moving-conversation-1...",
      // Any DialogueLine that has a "next" property immediately moves
      // to the file with that ID - use this to branch, loop, and just
      // organise your dialogue into neater chunks
      "next": "moving-conversation-2"
    }
  ]
}
```

```json
// Assets/.../Resources/Dialogue/YourFilePath/moving-conversation-2.json
{
  "id": "moving-conversation-2",
  "dialogueLines": [
    {
      "speaker": "npc_1",
      "dialogue": "... and this line has come from moving-conversation-2!",
      // And we loop back to the start of the conversation!
      "next": "moving-conversation-1"
    }
  ]
}
```



## Basic Class Reference

This isn't an intro to every class in the system, just the minimum needed in order to use this tool. For more complete information, read the summary blocks in each class.

### DialogueSystem

This is the primary class in the Simple JSON Dialogue System, and for most users you'll only need this class and the **DialogueLine** instances the **DialogueSystem** gives you.



### DialogueLine

A **DialogueLine** is the (abstract!) base class used to model a line of dialogue in JSON. 

Every other type of dialogue line extends from this base class, which defines two optional fields:

1. _next_ (optional): if present, the **DialogueSystem** will move the **DialogueRecord** of the corresponding ID the next time `StepToNextDialogueLine` is called
2. _meta_ (optional): a **Dictionary** of `<string, string>` key-value pairs - _meta_ is a generic catch-all to allow you to add additional fields to your lines that aren't currently supported by this tool

Examples of potential uses of _meta_: tracking an audio file for each line; having a "should this line be displayed?" condition; something else



### SpokenDialogueLine

A **SpokenDialogueLine** is a line that has a speaker and some dialogue (shocking, I know!) - the most common type of dialogue line you'll write are **SpokenDialogueLines**. They contain:

1. _speaker_ (required): who is saying this line of dialogue
2. _dialogue_ (required): what is being said



### OptionsDialogueLine

The **OptionsDialogueLine** allows you to create a branching structure, where the response to the **OptionsDialogueLine** takes the player down one of a few paths.

_Currently_ the `.StepToNextDialogueLine()` method takes an optional string parameter, the `.Next` value of one of the options (see the **OptionsDialogueLine** examples for more details).

Ex:

```csharp
// This method might be wired up to a UI.Button for each choice,
// where the .Next property of each line was set to the button programmatically
public void OnDialogueChoice(string next)
{
  _dialogueSystem.StepToNextDialogueLine(next);
}
```

The JSON for an **OptionsDialogueLine** takes a lot of vertical space - check the examples for how to use these.



### CommandDialogueLine

A **CommandDialogueLine** allows you to call predefined code from within the **DialogueSystem**. For instance, you may want an NPC's dialogue line of "Ok, time to go!" to trigger a scene transition, or a response to "So can I join your party?" to add an NPC to your player's team.

A **CommandDialogueLine** has two fields: 

1. *command* (required): a string which links to an **Action** declared elsewhere
2. _params_ (optional): a JSON object of key-value pairs to be passed into your **Action** when called. 

**CommandDialogueLines** trigger and run automatically when the **DialogueSystem** steps them - the Command(s) run until the end of the dialogue, or a new **SpokenDialogueLine** is reached:

```json
{
  "id": "command-example",
  "dialogueLines": [
    {
      "speaker": "npc::driver",
      "dialogue": "Time to head out to the big city!"
    },
    {
      // This command will be automatically run the next time .StepToNextDialogueLine() 
      // is called, and the DialogueSystem will step to the next line
      "command": "_fade_screen_to_black"
    },
    {
      // Consecutive commands are called in order until either the end of the dialogue
      // (such as in this example) or until a non-command line is found
      "command": "_load_new_scene",
      "params": {
        "scene": "whipple-village"
      }
    }
  ]
}
```


