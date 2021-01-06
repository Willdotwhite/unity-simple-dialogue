using System;
using System.Collections.Generic;

namespace _Project.Dialogue.Config
{
    public class CommandParameters : Dictionary<string, object> { }

    [Serializable]
    public class SerializableCommandParameters : SerializableDictionary<string, string> { }
}