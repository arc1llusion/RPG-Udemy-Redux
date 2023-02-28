using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField]
        private float speed = 5.0f;

        private Health target;

        // Update is called once per frame
        void Update()
        {
            if(target != null)
            {
                transform.LookAt(GetAimLocation());
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }

        public void SetTarget(Health target)
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()
        {
            var targetCollider = target.GetComponent<CapsuleCollider>();

            if(targetCollider == null)
            {
                return target.transform.position;
            }

            return target.transform.position + (Vector3.up * (targetCollider.height / 2));
        }
    }
}