using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private const string lastBuildIndex = "lastBuildIndex";

        public IEnumerator LoadLastScene(string saveFile)
        {
            var state = LoadFile(saveFile);

            if (state.TryGetValue(lastBuildIndex, out var result))
            {
                var buildIndex = (int)result;
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync((int)result);
                }
            }
            RestoreState(state);
        }

        public void Save(string saveFile)
        {
            var state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Saving to " + path);

            using (var stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);

            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }

            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as Dictionary<string, object>;
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            var saveableList = FindObjectsOfType<SaveableEntity>();

            foreach(var saveable in saveableList)
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state[lastBuildIndex] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            var saveableList = FindObjectsOfType<SaveableEntity>();

            foreach(var saveable in saveableList)
            {
                var identifier = saveable.GetUniqueIdentifier();
                if(state.TryGetValue(identifier, out var result))
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