using System.Collections.Generic;

namespace DotwoGames.Dialogue.Config
{
    /// <summary>
    /// CommandParameters is a class that allows uses of CommandDialogueLine to pass parameters into methods more neatly
    /// <para>
    /// The Value type of object is a boxed primitive of type: [bool, int, float, string]. If you need another type,
    /// serialise it as string in JSON and then manually convert yourself
    /// </para>
    /// <para>
    /// If you're reading this file as an intro-level hunt through the codebase, I'd skip over this part for now
    /// </para>
    /// </summary>
    public class CommandParameters : Dictionary<string, object> { }

    /// <summary>
    /// SerializableCommandParameters is a way of loading the key-value pairs (KVPs) for the "params" field for
    /// CommandDialogueLines
    /// <para>
    /// Using [string, string] casts all values to the string type, which allows us to convert to different
    /// types later more easily than object
    /// </para>
    /// </summary>
    [System.Serializable]
    public class SerializableCommandParameters : Dictionary<string, string> { }
}