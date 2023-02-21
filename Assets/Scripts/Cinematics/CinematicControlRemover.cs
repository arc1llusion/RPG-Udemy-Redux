using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;

        private void Start()
        {
            var director = GetComponent<PlayableDirector>();

            director.played += DisableControl;
            director.stopped += EnableControl;

            player = GameObject.FindWithTag("Player");
        }

        public void DisableControl(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled= false;
        }

        public void EnableControl(PlayableDirector director)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
