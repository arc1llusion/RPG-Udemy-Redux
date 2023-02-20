using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Transform target;
        private Mover mover; 
        private ActionScheduler scheduler;
        private Animator animator;

        [SerializeField]
        private float weaponRange = 2.0f;

        [SerializeField]
        private float timeBetweenAttacks = 1.0f;
        private float timeSinceLastAttack = 0f;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (Vector3.Distance(target.position, transform.position) >= weaponRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //Animation Event
        void Hit()
        {
            if (target == null) return;

            var health = target.transform.GetComponent<Health>();

            if (health == null) return;

            health.TakeDamage(5);
        }

        public void Attack(CombatTarget ct)
        {
            scheduler.StartAction(this);
            target = ct.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}