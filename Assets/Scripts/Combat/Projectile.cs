using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private Transform target = null;

        [SerializeField]
        private float speed = 5.0f;

        // Update is called once per frame
        void Update()
        {
            if(target != null)
            {
                transform.LookAt(GetAimLocation());
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }

        private Vector3 GetAimLocation()
        {
            var targetCollider = target.GetComponent<CapsuleCollider>();

            if(targetCollider == null)
            {
                return target.position;
            }

            return target.position + (Vector3.up * (targetCollider.height / 2));
        }
    }
}