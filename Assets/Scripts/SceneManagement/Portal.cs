using RPG.Control;
using RPG.Core;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        public enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField]
        private int sceneToLoad = -1;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField] 
        private DestinationIdentifier identifier;

        [SerializeField]
        private float fadeOutTime = 1.0f;

        [SerializeField]
        private float fadeInTime = 1.0f;

        [SerializeField]
        private float waitBetweenScenesTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }            
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            DontDestroyOnLoad(this);

            var savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(waitBetweenScenesTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(this.gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        public bool CompareDestination(DestinationIdentifier identifier)
        {
            return identifier == this.identifier;
        }

        private Portal GetOtherPortal()
        {
            var otherPortal = FindObjectsOfType<Portal>().Where(x => x != this && x.CompareDestination(this.identifier)).FirstOrDefault();
            return otherPortal;
        }
    }

}