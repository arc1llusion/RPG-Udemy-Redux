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

        [SerializeField]
        private PatrolPath patrolPath;

        [SerializeField]
        private float waypointTolerance = 1.0f;

        [SerializeField]
        private float waypointDwellTime = 2.0f;

        [Range(0, 1)]
        [SerializeField]
        private float patrolSpeedFraction = 0.2f;



        private ChangePlayerPositionSignal changePlayerPositionSignal;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private ActionScheduler actionScheduler;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        private GameObject player = null;
        private Vector3 playerPosition;
        private bool canSeePlayer = false;

        private int currentWaypointIndex = 0;

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
            UpdateTimers();

            if (health.IsDead())
            {
                return;
            }

            if (canSeePlayer && fighter.CanAttack(player))
            {
                AttackBehavior(player);
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
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
            timeSinceLastSawPlayer = 0f;
            fighter.Attack(player);
        }

        private void SuspicionBehavior()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if(timeSinceArrivedAtWaypoint >= waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(GetCurrentWaypoint(), transform.position) <= waypointTolerance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}