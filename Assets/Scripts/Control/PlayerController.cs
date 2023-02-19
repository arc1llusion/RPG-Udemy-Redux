using deVoid.Utils;
using RPG.Combat;
using RPG.Movement;
using RPG.Movement.Signals;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour, Actions.IMainActions
    {
        private Actions actions;
        private Mover mover;
        private Fighter fighter;

        private bool moving = false;
        private bool overTarget = false;

        private Vector2 mousePosition;

        private ChangePlayerPositionSignal changePlayerPositionSignal;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
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
            if(InteractWithMovement())
            {
                return;
            }

            Debug.Log("Nothing to do");
        }

        private void InteractWithCombat()
        {
            DetermineTarget(() =>
            {
                fighter.Attack();
            });
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out var hitInfo))
            {
                if (moving && !overTarget)
                {
                    mover.MoveTo(hitInfo.point);
                }
                return true;
            }

            return false;
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

            if (context.performed)
            {
                InteractWithCombat();
            }
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
            overTarget = false;

            DetermineTarget(() =>
            {
                overTarget = true;
                //TODO: Cursor affordance
            });
        }

        private void DetermineTarget(Action cb)
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();

                if (target != null)
                {
                    cb?.Invoke();
                }
            }
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
    }
}