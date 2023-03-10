//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Core/Actions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Actions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""01fc9efa-9892-45fb-b94d-97730de08321"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""a338df69-dd61-46da-b5e6-56aa88502d4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""4efbeec9-5949-4040-a953-e3e3bdea3e7d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b06d233f-1bef-4981-9ca7-62dda5a6da92"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31252f44-eba8-48a1-bb9a-2762c5294f97"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SaveSystem"",
            ""id"": ""6350ff97-25ad-40bd-aec4-b69ae0387a73"",
            ""actions"": [
                {
                    ""name"": ""Saving"",
                    ""type"": ""Button"",
                    ""id"": ""1b878dd7-b6b1-4dc3-8211-2990c2c4b7d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Loading"",
                    ""type"": ""Button"",
                    ""id"": ""dd721af3-a654-48b2-ad30-83af7f8e86a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7507abff-9dc9-4d70-af5a-9d009db96f83"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Saving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb9dffcc-fe5d-4c70-94d1-a8965465a66a"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Loading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Movement = m_Main.FindAction("Movement", throwIfNotFound: true);
        m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
        // SaveSystem
        m_SaveSystem = asset.FindActionMap("SaveSystem", throwIfNotFound: true);
        m_SaveSystem_Saving = m_SaveSystem.FindAction("Saving", throwIfNotFound: true);
        m_SaveSystem_Loading = m_SaveSystem.FindAction("Loading", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Movement;
    private readonly InputAction m_Main_MousePosition;
    public struct MainActions
    {
        private @Actions m_Wrapper;
        public MainActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Main_Movement;
        public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMovement;
                @MousePosition.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public MainActions @Main => new MainActions(this);

    // SaveSystem
    private readonly InputActionMap m_SaveSystem;
    private ISaveSystemActions m_SaveSystemActionsCallbackInterface;
    private readonly InputAction m_SaveSystem_Saving;
    private readonly InputAction m_SaveSystem_Loading;
    public struct SaveSystemActions
    {
        private @Actions m_Wrapper;
        public SaveSystemActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Saving => m_Wrapper.m_SaveSystem_Saving;
        public InputAction @Loading => m_Wrapper.m_SaveSystem_Loading;
        public InputActionMap Get() { return m_Wrapper.m_SaveSystem; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SaveSystemActions set) { return set.Get(); }
        public void SetCallbacks(ISaveSystemActions instance)
        {
            if (m_Wrapper.m_SaveSystemActionsCallbackInterface != null)
            {
                @Saving.started -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnSaving;
                @Saving.performed -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnSaving;
                @Saving.canceled -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnSaving;
                @Loading.started -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnLoading;
                @Loading.performed -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnLoading;
                @Loading.canceled -= m_Wrapper.m_SaveSystemActionsCallbackInterface.OnLoading;
            }
            m_Wrapper.m_SaveSystemActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Saving.started += instance.OnSaving;
                @Saving.performed += instance.OnSaving;
                @Saving.canceled += instance.OnSaving;
                @Loading.started += instance.OnLoading;
                @Loading.performed += instance.OnLoading;
                @Loading.canceled += instance.OnLoading;
            }
        }
    }
    public SaveSystemActions @SaveSystem => new SaveSystemActions(this);
    public interface IMainActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface ISaveSystemActions
    {
        void OnSaving(InputAction.CallbackContext context);
        void OnLoading(InputAction.CallbackContext context);
    }
}
