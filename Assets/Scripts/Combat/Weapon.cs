using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private GameObject equippedPrefab = null;

        [SerializeField]
        AnimatorOverrideController animatorOverride = null;

        [SerializeField]
        private float range = 2.0f;

        [SerializeField]
        private float damage = 5.0f;

        [SerializeField]
        private bool isRightHanded = true;

        public float Range
        {
            get { return range; }
        }

        public float Damage
        {
            get { return damage; }
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null && rightHand != null)
            {
                Instantiate(equippedPrefab, isRightHanded ? rightHand : leftHand);
            }

            if (animator != null && animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}