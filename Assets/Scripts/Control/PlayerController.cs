using deVoid.Utils;
using RPG.Combat;
using RPG.Movement;
using RPG.Movement.Signals;
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
        //private bool mouseLeftButtonPerformed = false;

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
            //mouseLeftButtonPerformed = false;
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();

                if(target != null)
                {
                    //if(mouseLeftButtonPerformed)
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        fighter.Attack();
                    }
                }
            }
        }

        private void InteractWithMovement()
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

            //if(context.performed)
            //{
            //    mouseLeftButtonPerformed = true;
            //}
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        private void MoveToCursor()
        {
            if (Physics.Raycast(GetMouseRay(), out var hitInfo))
            {
                mover.MoveTo(hitInfo.point);
            }
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }
    }
}