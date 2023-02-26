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

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifer;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            var sv3 = state as SerializableVector3;
            var agent = GetComponent<NavMeshAgent>();
            var actionSchedular = GetComponent<ActionScheduler>();

            agent.Warp(sv3.ToVector());
            actionSchedular.CancelCurrentAction();
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty(nameof(uniqueIdentifer));

            if (string.IsNullOrEmpty(property.stringValue))
            {
                uniqueIdentifer = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
#endif
        }
    }
}