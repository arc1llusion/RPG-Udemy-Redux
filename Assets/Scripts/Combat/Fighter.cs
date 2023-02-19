using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        private Transform target;
        private Mover mover; 
        private ActionScheduler scheduler;        

        [SerializeField]
        private float weaponRange = 2.0f;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            scheduler = GetComponent<ActionScheduler>();
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
            scheduler.StartAction(this);
            target = ct.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}