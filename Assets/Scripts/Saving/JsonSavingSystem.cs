using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class JsonSavingSystem : MonoBehaviour
    {
        private const string lastBuildIndex = "lastBuildIndex";
        private const string extension = ".json";

        [SerializeField] SavingStrategy strategy;

        public IEnumerator LoadLastScene(string saveFile)
        {
            var state = LoadJsonFromFile(saveFile);

            if (state.TryGetValue(lastBuildIndex, out var result))
            {
                var buildIndex = (int)result;
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            RestoreFromJToken(state);
        }

        public void Save(string saveFile)
        {
            var state = LoadJsonFromFile(saveFile);
            CaptureAsJToken(state);
            SaveFileAsJson(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreFromJToken(LoadJsonFromFile(saveFile));
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(Application.persistentDataPath))
            {
                if (Path.GetExtension(path) == strategy.Extension)
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        private JObject LoadJsonFromFile(string saveFile)
        {
            return strategy.LoadFromFile(saveFile);
        }

        private void SaveFileAsJson(string saveFile, JObject state)
        {
            strategy.SaveToFile(saveFile, state);
        }

        private void CaptureAsJToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
            {
                stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJToken();
            }

            stateDict[lastBuildIndex] = SceneManager.GetActiveScene().buildIndex;
        }


        private void RestoreFromJToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (stateDict.TryGetValue(id, out var result))
                {
                    saveable.RestoreFromJToken(result);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return strategy.GetPathFromSaveFile(saveFile);
        }
    }
}