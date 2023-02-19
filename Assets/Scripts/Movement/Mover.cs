using deVoid.Utils;
using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        private NavMeshAgent agent;
        private Animator animator;
        private Fighter fighter;
        private ActionScheduler scheduler;

        // Start is called before the first frame update
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
            scheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var localSpace = transform.InverseTransformDirection(agent.velocity);
            animator.SetFloat("forwardSpeed", localSpace.z);
        }

        public void StartMoveAction(Vector3 destination)
        {
            fighter.Cancel();
            scheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            agent.isStopped = false;
            agent.destination = destination;
        }

        public void Stop()
        {
            agent.isStopped = true;
        }
    }
}