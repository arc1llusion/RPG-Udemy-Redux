using RPG.Control;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneMnagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private int sceneToLoad = -1;

        [SerializeField]
        private Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }            
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(this);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(this.gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            var otherPortal = FindObjectsOfType<Portal>().Where(x => x != this).FirstOrDefault();
            return otherPortal;
        }
    }

}