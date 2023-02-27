using Newtonsoft.Json.Linq;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class JsonSaveableEntity : MonoBehaviour
    {
        [SerializeField]
        private string uniqueIdentifer = "";

        private static Dictionary<string, JsonSaveableEntity> globalEntities = new Dictionary<string, JsonSaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifer;
        }

        public JToken CaptureAsJToken()
        {
            JObject state = new JObject();
            IDictionary<string, JToken> stateDict = state;
            foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
            {
                JToken token = jsonSaveable.CaptureAsJToken();
                string component = jsonSaveable.GetType().ToString();
                Debug.Log($"{name} Capture {component} = {token.ToString()}");
                stateDict[jsonSaveable.GetType().ToString()] = token;
            }
            return state;
        }

        public void RestoreFromJToken(JToken s)
        {
            JObject state = s.ToObject<JObject>();
            IDictionary<string, JToken> stateDict = state;
            foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())
            {
                string component = jsonSaveable.GetType().ToString();
                if (stateDict.TryGetValue(component, out var result))
                {

                    Debug.Log($"{name} Restore {component} =>{result}");
                    jsonSaveable.RestoreFromJToken(result);
                }
            }
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty(nameof(uniqueIdentifer));

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            globalEntities[property.stringValue] = this;
        }

        private bool IsUnique(string candidate)
        {
            if (!globalEntities.ContainsKey(candidate))
            {
                return true;
            }

            if (globalEntities[candidate] == this)
            {
                return true;
            }

            if (globalEntities[candidate] == null)
            {
                globalEntities.Remove(candidate);
                return true;
            }

            if (globalEntities[candidate].GetUniqueIdentifier() != candidate)
            {
                globalEntities.Remove(candidate);
                return true;
            }

            return false;
        }
#endif
    }
}