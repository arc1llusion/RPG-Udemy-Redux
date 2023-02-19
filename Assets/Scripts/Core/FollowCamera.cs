using deVoid.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private ChangePlayerPositionSignal changePlayerPositionSignal;
    // Start is called before the first frame update
    void Start()
    {
        changePlayerPositionSignal = Signals.Get<ChangePlayerPositionSignal>();
        changePlayerPositionSignal.AddListener(Follow);
    }

    private void Follow(Vector3 position)
    {
        transform.position = position;
    }

    private void OnDestroy()
    {
        if (changePlayerPositionSignal != null)
        {
            changePlayerPositionSignal.RemoveListener(Follow);
        }
    }
}
