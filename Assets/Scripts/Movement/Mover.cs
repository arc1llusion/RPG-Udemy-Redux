using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        private NavMeshAgent agent;
        private Animator animator;
        private ActionScheduler scheduler;
        private Health health;

        // Start is called before the first frame update
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var localSpace = transform.InverseTransformDirection(agent.velocity);
            animator.SetFloat("forwardSpeed", localSpace.z);
        }

        public void StartMoveAction(Vector3 destination)
        {
            scheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            agent.isStopped = false;
            agent.destination = destination;
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
    }
}