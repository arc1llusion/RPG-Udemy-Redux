using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                int j = (i + 1) % transform.childCount;

                Gizmos.DrawSphere(GetWaypointPosition(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }

        private Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
