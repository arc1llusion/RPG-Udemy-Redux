using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        private Transform target;
        private Mover mover;

        [SerializeField]
        private float weaponRange = 2.0f;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target == null) return;

            if (Vector3.Distance(target.position, transform.position) >= weaponRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }

        public void Attack(CombatTarget ct)
        {
            target = ct.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}