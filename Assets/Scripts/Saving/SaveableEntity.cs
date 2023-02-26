using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SaveableEntity : MonoBehaviour
    {
        public string GetUniqueIdentifier()
        {
            return string.Empty;
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
    }
}