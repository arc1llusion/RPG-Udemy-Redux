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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

            Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                
                if (!fighter.CanAttack(target.gameObject)) continue;

                if (actions.Main.Movement.WasPressedThisFrame())
                {
                    fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out var hitInfo))
            {
                if (actions.Main.Movement.ReadValue<float>() == 1)
                {
                    mover.StartMoveAction(hitInfo.point);
                }
                return true;
            }

            return false;
        }

        private void LateUpdate()
        {
            changePlayerPositionSignal.Dispatch(gameObject, transform.position);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
    }
}