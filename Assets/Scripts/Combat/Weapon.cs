using RPG.Core;
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

        [SerializeField]
        private Projectile projectile = null;

        public float Range
        {
            get { return range; }
        }

        public float Damage
        {
            get { return damage; }
        }

        public bool HasProjectile
        {
            get { return projectile != null; }
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform instTransform = GetTransform(rightHand, leftHand);
            Projectile projectileInstance = Instantiate(projectile, instTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target, damage);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return (isRightHanded ? rightHand : leftHand);
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null && rightHand != null)
            {
                Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
            }

            if (animator != null && animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}