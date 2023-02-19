using deVoid.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour, Actions.IMainActions
{
    private Actions actions;

    private NavMeshAgent agent;
    private Animator animator;

    private Vector2 mousePosition;

    private bool moving = false;

    private ChangePlayerPositionSignal changePlayerPositionSignal;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        changePlayerPositionSignal = Signals.Get<ChangePlayerPositionSignal>();
    }

    void Update()
    {
        UpdateAnimator();

        if(moving)
        {
            MoveToCursor();
        }
    }

    private void LateUpdate()
    {
        changePlayerPositionSignal.Dispatch(transform.position);
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

    private void UpdateAnimator()
    {
        var localSpace = transform.InverseTransformDirection(agent.velocity);
        animator.SetFloat("forwardSpeed", localSpace.z);
    }

    private void MoveToCursor()
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            agent.destination = hitInfo.point;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moving = true;
        }
        else if(context.canceled)
        {
            moving = false;
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
