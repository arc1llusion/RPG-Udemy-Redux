using deVoid.Utils;
using RPG.Combat;
using RPG.Core;
using RPG.Movement.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float chaseDistance = 5f;

        private ChangePlayerPositionSignal changePlayerPositionSignal;
        private Fighter fighter;
        private Health health;

        private void Awake()
        {
            changePlayerPositionSignal = Signals.Get<ChangePlayerPositionSignal>();
            changePlayerPositionSignal.AddListener(OnPlayerPositionChanged);

            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void OnDestroy()
        {
            if(changePlayerPositionSignal != null)
            {
                changePlayerPositionSignal.RemoveListener(OnPlayerPositionChanged);
            }
        }

        private void OnPlayerPositionChanged(GameObject player, Vector3 position)
        {
            if (health.IsDead())
            {
                return;
            }
            
            var distance = Vector3.Distance(position, transform.position);

            if(distance <= chaseDistance && fighter.CanAttack(player)) 
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }
    }
}