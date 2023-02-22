using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneMnagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            
        }
    }

}