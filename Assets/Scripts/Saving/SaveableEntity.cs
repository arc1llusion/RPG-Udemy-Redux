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
        [EditorReadOnly]
        private string uniqueIdentifer = "";

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

        public void OnValidate()
        {
#if UNITY_EDITOR
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty(nameof(uniqueIdentifer));

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
#endif
        }
    }
}