// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Player_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player_Input"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""41518d1c-b7ed-4a20-b30e-515b7c186e73"",
            ""actions"": [
                {
                    ""name"": ""MovementHorizontal"",
                    ""type"": ""Value"",
                    ""id"": ""06004a74-3741-461a-b6c0-b23924d30585"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e65b8e14-b3da-4c32-92b3-6e7676a50f42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Wait/Follow"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0e13151-17fc-4430-9476-943dd115bbd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""2612685f-67f1-4230-b26a-e8175336c75d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4ff7a92a-e6ad-4427-957c-ed504df8e3ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Blowdart"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a24e89dd-1b16-4e2f-93a8-3b0fbb06bcec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Flute"",
                    ""type"": ""Button"",
                    ""id"": ""829d86af-d2e3-4bc8-a4d8-28d3b8279e08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Crounch"",
                    ""type"": ""Button"",
                    ""id"": ""131523cb-d247-4c0e-bf46-5b3cd2ec2321"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""2f69a443-3d13-4983-9f6a-1d7bb48eb931"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""eef7d287-7e20-411b-af3b-f321abe9f9f5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a2658fce-0590-478b-913e-16c5a6b58729"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""MovementHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e50093e2-fc7d-46f1-97a2-86acdfcd27b0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""MovementHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c17cfdfc-ace9-4736-a27b-f0965030f836"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Wait/Follow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f8dfdaf-35be-489c-a982-2bd3b29024a4"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""708b1114-05ab-48d2-b4aa-b58cd5d32ac8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""b8404db4-7789-4f93-8085-27cf2bbca04d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ce331aff-4160-46a8-b975-d216f7bfbba9"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""MovementVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""209fde69-6578-47d6-b58b-88f8453a9e2b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""MovementVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b04b859e-d411-4d86-a092-469ce8eee615"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Blowdart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40860d6c-3fd8-441e-bc3e-53b5cdf479e3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Flute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22114182-48ed-4de6-b657-9553d8bc2e31"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Crounch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dac45b9-2c7f-4d59-97ca-144d6ca40edd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""ControlScheme"",
            ""bindingGroup"": ""ControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MovementHorizontal = m_Player.FindAction("MovementHorizontal", throwIfNotFound: true);
        m_Player_MovementVertical = m_Player.FindAction("MovementVertical", throwIfNotFound: true);
        m_Player_WaitFollow = m_Player.FindAction("Wait/Follow", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Blowdart = m_Player.FindAction("Blowdart", throwIfNotFound: true);
        m_Player_Flute = m_Player.FindAction("Flute", throwIfNotFound: true);
        m_Player_Crounch = m_Player.FindAction("Crounch", throwIfNotFound: true);
        m_Player_Restart = m_Player.FindAction("Restart", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MovementHorizontal;
    private readonly InputAction m_Player_MovementVertical;
    private readonly InputAction m_Player_WaitFollow;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Blowdart;
    private readonly InputAction m_Player_Flute;
    private readonly InputAction m_Player_Crounch;
    private readonly InputAction m_Player_Restart;
    public struct PlayerActions
    {
        private @Player_Input m_Wrapper;
        public PlayerActions(@Player_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementHorizontal => m_Wrapper.m_Player_MovementHorizontal;
        public InputAction @MovementVertical => m_Wrapper.m_Player_MovementVertical;
        public InputAction @WaitFollow => m_Wrapper.m_Player_WaitFollow;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Blowdart => m_Wrapper.m_Player_Blowdart;
        public InputAction @Flute => m_Wrapper.m_Player_Flute;
        public InputAction @Crounch => m_Wrapper.m_Player_Crounch;
        public InputAction @Restart => m_Wrapper.m_Player_Restart;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MovementHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementHorizontal;
                @MovementHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementHorizontal;
                @MovementHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementHorizontal;
                @MovementVertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementVertical;
                @MovementVertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementVertical;
                @MovementVertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementVertical;
                @WaitFollow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWaitFollow;
                @WaitFollow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWaitFollow;
                @WaitFollow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWaitFollow;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Blowdart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlowdart;
                @Blowdart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlowdart;
                @Blowdart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlowdart;
                @Flute.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlute;
                @Flute.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlute;
                @Flute.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFlute;
                @Crounch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrounch;
                @Crounch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrounch;
                @Crounch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrounch;
                @Restart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovementHorizontal.started += instance.OnMovementHorizontal;
                @MovementHorizontal.performed += instance.OnMovementHorizontal;
                @MovementHorizontal.canceled += instance.OnMovementHorizontal;
                @MovementVertical.started += instance.OnMovementVertical;
                @MovementVertical.performed += instance.OnMovementVertical;
                @MovementVertical.canceled += instance.OnMovementVertical;
                @WaitFollow.started += instance.OnWaitFollow;
                @WaitFollow.performed += instance.OnWaitFollow;
                @WaitFollow.canceled += instance.OnWaitFollow;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Blowdart.started += instance.OnBlowdart;
                @Blowdart.performed += instance.OnBlowdart;
                @Blowdart.canceled += instance.OnBlowdart;
                @Flute.started += instance.OnFlute;
                @Flute.performed += instance.OnFlute;
                @Flute.canceled += instance.OnFlute;
                @Crounch.started += instance.OnCrounch;
                @Crounch.performed += instance.OnCrounch;
                @Crounch.canceled += instance.OnCrounch;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_ControlSchemeSchemeIndex = -1;
    public InputControlScheme ControlSchemeScheme
    {
        get
        {
            if (m_ControlSchemeSchemeIndex == -1) m_ControlSchemeSchemeIndex = asset.FindControlSchemeIndex("ControlScheme");
            return asset.controlSchemes[m_ControlSchemeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovementHorizontal(InputAction.CallbackContext context);
        void OnMovementVertical(InputAction.CallbackContext context);
        void OnWaitFollow(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBlowdart(InputAction.CallbackContext context);
        void OnFlute(InputAction.CallbackContext context);
        void OnCrounch(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
