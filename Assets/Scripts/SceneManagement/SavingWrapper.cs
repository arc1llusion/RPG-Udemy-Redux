using RPG.Saving;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour, Actions.ISaveSystemActions
    {
        private const string defaultSaveFile = "save";

        private SavingSystem savingSystem;
        private Actions actions;

        [SerializeField]
        private float fadeInTime = 1.0f;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();

            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
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