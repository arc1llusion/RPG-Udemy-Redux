using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Saving to " + path);

            using (var stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);

            if (!File.Exists(path))
            {
                return;
            }

            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        private object CaptureState()
        {
            var state = new Dictionary<string, object>();

            var saveableList = FindObjectsOfType<SaveableEntity>();

            foreach(var saveable in saveableList)
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            return state;
        }

        private void RestoreState(object state)
        {
            var stateDict = state as Dictionary<string, object>;

            var saveableList = FindObjectsOfType<SaveableEntity>();

            foreach(var saveable in saveableList)
            {
                var identifier = saveable.GetUniqueIdentifier();
                if(stateDict.TryGetValue(identifier, out var result))
                {
                    saveable.RestoreState(result);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, Path.ChangeExtension(saveFile, ".sav"));
        }
    }
}