using deVoid.Utils;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Movement.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float chaseDistance = 5f;

        [SerializeField]
        private float suspicionTime = 3f;

        private ChangePlayerPositionSignal changePlayerPositionSignal;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private ActionScheduler actionScheduler;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        private GameObject player = null;
        private Vector3 playerPosition;
        private bool canSeePlayer = false;

        private void Awake()
        {
            changePlayerPositionSignal = Signals.Get<ChangePlayerPositionSignal>();
            changePlayerPositionSignal.AddListener(OnPlayerPositionChanged);

            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();

            guardPosition = transform.position;
        }

        private void OnDestroy()
        {
            if(changePlayerPositionSignal != null)
            {
                changePlayerPositionSignal.RemoveListener(OnPlayerPositionChanged);
            }
        }

        private void Update()
        {
            timeSinceLastSawPlayer += Time.deltaTime;

            if (health.IsDead())
            {
                return;
            }

            if (canSeePlayer && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehavior(player);
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }
        }

        private void OnPlayerPositionChanged(GameObject player, Vector3 position)
        {
            this.player = player;
            this.playerPosition = position;

            var distance = Vector3.Distance(position, transform.position);
            this.canSeePlayer = distance <= chaseDistance;
        }

        private void AttackBehavior(GameObject player)
        {
            fighter.Attack(player);
        }

        private void SuspicionBehavior()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void GuardBehavior()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}