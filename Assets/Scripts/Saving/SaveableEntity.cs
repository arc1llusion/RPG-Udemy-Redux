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
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField]
        private string uniqueIdentifer = "";

        private static Dictionary<string, SaveableEntity> globalEntities = new Dictionary<string, SaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifer;
        }

        public object CaptureState()
        {
            var state = new Dictionary<string, object>();
            var saveables = GetComponents<ISaveable>();
            foreach (var saveable in saveables)
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return state;
        }

        public void RestoreState(object state)
        {
            var stateDict = state as Dictionary<string, object>;
            var saveables = GetComponents<ISaveable>();

            foreach (var saveable in saveables)
            {
                if (stateDict.TryGetValue(saveable.GetType().ToString(), out var result))
                {
                    saveable.RestoreState(result);
                }
            }
        }

        private void Update()
        {
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
            if(!globalEntities.ContainsKey(candidate))
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