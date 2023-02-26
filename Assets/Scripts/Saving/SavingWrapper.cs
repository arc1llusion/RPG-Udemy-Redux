using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour, Actions.ISaveSystemActions
    {
        private const string defaultSaveFile = "save";

        private SavingSystem savingSystem;
        private Actions actions;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private void Start()
        {
            Load();
        }

        private void OnEnable()
        {
            if (actions == null)
            {
                actions = new Actions();
            }
            actions.SaveSystem.SetCallbacks(this);
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
            actions = null;
        }

        public void OnLoading(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (savingSystem != null)
                {
                    Load();
                }
            }
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        public void OnSaving(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (savingSystem != null)
                {
                    Save();
                }
            }
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }
    }
}