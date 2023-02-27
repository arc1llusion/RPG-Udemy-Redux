using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private GameObject weaponPrefab = null;

        [SerializeField]
        AnimatorOverrideController animatorOverride = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null && handTransform != null)
            {
                Instantiate(weaponPrefab, handTransform);
                if (animator != null && animatorOverride != null)
                {
                    animator.runtimeAnimatorController = animatorOverride;
                }
            }
        }
    }
}