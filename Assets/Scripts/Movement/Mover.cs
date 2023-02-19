using deVoid.Utils;
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

        // Start is called before the first frame update
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
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

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
        }
    }
}