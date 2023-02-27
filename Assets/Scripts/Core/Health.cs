using Newtonsoft.Json.Linq;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] private float healthPoints = 100f;

        private Animator animator;
        private ActionScheduler scheduler;
        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints, gameObject);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
            scheduler.CancelCurrentAction();
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(healthPoints);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoints = state.ToObject<float>();
        }
    }
}