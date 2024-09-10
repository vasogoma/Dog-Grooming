// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Dog/DogInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DogInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DogInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DogInputActions"",
    ""maps"": [
        {
            ""name"": ""DogCommands"",
            ""id"": ""a1801ffd-f38d-4155-a92d-11ceddd44b52"",
            ""actions"": [
                {
                    ""name"": ""GoToBathtub"",
                    ""type"": ""Button"",
                    ""id"": ""52f20d74-2049-48ac-b33b-17eb058e07f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoToTable"",
                    ""type"": ""Button"",
                    ""id"": ""a263325c-6a72-4a04-a460-9152ca27332c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoToWaitingArea"",
                    ""type"": ""Button"",
                    ""id"": ""4b96d3a1-d85e-4466-8140-bbae1ed8ab27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cd2facfb-e19f-49dd-a979-bcab44255cd2"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToBathtub"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9364f167-f3d8-41c8-a84c-5b138e295637"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToTable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17e7d743-92d4-4e8f-ac6c-cc991ee1e256"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoToWaitingArea"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DogCommands
        m_DogCommands = asset.FindActionMap("DogCommands", throwIfNotFound: true);
        m_DogCommands_GoToBathtub = m_DogCommands.FindAction("GoToBathtub", throwIfNotFound: true);
        m_DogCommands_GoToTable = m_DogCommands.FindAction("GoToTable", throwIfNotFound: true);
        m_DogCommands_GoToWaitingArea = m_DogCommands.FindAction("GoToWaitingArea", throwIfNotFound: true);
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

    // DogCommands
    private readonly InputActionMap m_DogCommands;
    private IDogCommandsActions m_DogCommandsActionsCallbackInterface;
    private readonly InputAction m_DogCommands_GoToBathtub;
    private readonly InputAction m_DogCommands_GoToTable;
    private readonly InputAction m_DogCommands_GoToWaitingArea;
    public struct DogCommandsActions
    {
        private @DogInputActions m_Wrapper;
        public DogCommandsActions(@DogInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @GoToBathtub => m_Wrapper.m_DogCommands_GoToBathtub;
        public InputAction @GoToTable => m_Wrapper.m_DogCommands_GoToTable;
        public InputAction @GoToWaitingArea => m_Wrapper.m_DogCommands_GoToWaitingArea;
        public InputActionMap Get() { return m_Wrapper.m_DogCommands; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DogCommandsActions set) { return set.Get(); }
        public void SetCallbacks(IDogCommandsActions instance)
        {
            if (m_Wrapper.m_DogCommandsActionsCallbackInterface != null)
            {
                @GoToBathtub.started -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToBathtub;
                @GoToBathtub.performed -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToBathtub;
                @GoToBathtub.canceled -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToBathtub;
                @GoToTable.started -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToTable;
                @GoToTable.performed -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToTable;
                @GoToTable.canceled -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToTable;
                @GoToWaitingArea.started -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToWaitingArea;
                @GoToWaitingArea.performed -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToWaitingArea;
                @GoToWaitingArea.canceled -= m_Wrapper.m_DogCommandsActionsCallbackInterface.OnGoToWaitingArea;
            }
            m_Wrapper.m_DogCommandsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GoToBathtub.started += instance.OnGoToBathtub;
                @GoToBathtub.performed += instance.OnGoToBathtub;
                @GoToBathtub.canceled += instance.OnGoToBathtub;
                @GoToTable.started += instance.OnGoToTable;
                @GoToTable.performed += instance.OnGoToTable;
                @GoToTable.canceled += instance.OnGoToTable;
                @GoToWaitingArea.started += instance.OnGoToWaitingArea;
                @GoToWaitingArea.performed += instance.OnGoToWaitingArea;
                @GoToWaitingArea.canceled += instance.OnGoToWaitingArea;
            }
        }
    }
    public DogCommandsActions @DogCommands => new DogCommandsActions(this);
    public interface IDogCommandsActions
    {
        void OnGoToBathtub(InputAction.CallbackContext context);
        void OnGoToTable(InputAction.CallbackContext context);
        void OnGoToWaitingArea(InputAction.CallbackContext context);
    }
}
