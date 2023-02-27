using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPG.Saving
{
    public abstract class SavingStrategy : ScriptableObject
    {
        public abstract void SaveToFile(string saveFile, JObject State);

        public abstract JObject LoadFromFile(string saveFile);
        public abstract string Extension { get; }

        public string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, Path.ChangeExtension(saveFile, this.Extension));
        }
    }
}
