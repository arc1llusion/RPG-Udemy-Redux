using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Actions.IMainActions
{
    private Actions actions;
    private Mover mover;

    private bool moving = false;

    private Vector2 mousePosition;

    private ChangePlayerPositionSignal changePlayerPositionSignal;

    void Start()
    {
        mover = GetComponent<Mover>();

        changePlayerPositionSignal = Signals.Get<ChangePlayerPositionSignal>();
    }

    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new Actions();
        }
        actions.Main.SetCallbacks(this);
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
        actions = null;
    }

    private void Update()
    {
        if (moving)
        {
            MoveToCursor();
        }
    }
    private void LateUpdate()
    {
        changePlayerPositionSignal.Dispatch(transform.position);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moving = true;
        }
        else if (context.canceled)
        {
            moving = false;
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void MoveToCursor()
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            mover.MoveTo(hitInfo.point);
        }
    }
}
