using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField]
        private string uniqueIdentifer = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifer;
        }

        public object CaptureState()
        {
            Debug.Log("Capturing State for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoring State for " + GetUniqueIdentifier());
        }

        public void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty(nameof(uniqueIdentifer));            

            if (string.IsNullOrEmpty(property.stringValue))
            {
                uniqueIdentifer = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}