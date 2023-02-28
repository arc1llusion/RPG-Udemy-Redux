using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (weapon != null && other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(this.gameObject);
            }
        }
    }
}