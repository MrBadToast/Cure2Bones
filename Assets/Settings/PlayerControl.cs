// GENERATED AUTOMATICALLY FROM 'Assets/Settings/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1ac69a3d-485e-4658-b825-21161c05216d"",
            ""actions"": [
                {
                    ""name"": ""move"",
                    ""type"": ""Value"",
                    ""id"": ""a369aff4-4660-43c6-9b3e-a9eeedbac631"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""lookaround_x"",
                    ""type"": ""Value"",
                    ""id"": ""34bf0f53-bef6-4d80-aca5-1e783fc2cdb8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""lookaround_y"",
                    ""type"": ""Value"",
                    ""id"": ""0736c9fa-1e3f-4339-9701-1c97e00b33ec"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""5d9711fe-811c-49f1-a752-dcbe21415bf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""pick"",
                    ""type"": ""Button"",
                    ""id"": ""d6a749a5-f7cc-45ad-81d0-93be49c0d3fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""attack"",
                    ""type"": ""Button"",
                    ""id"": ""4cfac173-ae8c-4b70-a0d3-2a79d5348fe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""charge"",
                    ""type"": ""Button"",
                    ""id"": ""52b9d537-9dec-47b9-8cc7-38a34482ec4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""28c8ea22-13a2-4db5-88d6-c173878489e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""45f3928d-4864-430d-ae47-ee64d6e49df3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0c1694f9-5083-4f5c-8450-f232d224efa0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b334358a-2128-4401-b175-031b38832daf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bf6c2e3b-5440-4d1b-9ae1-2958424b4b7a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""550c3d98-3c4a-4b2c-b2d9-4440ef163bd7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""089ad968-6068-4527-bfa2-bcc7946b6682"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookaround_x"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40987a54-a2e3-4d21-98cd-13c6765591a9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f9b67a0-57b1-4d31-bcc6-f85a6bbb78a6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16eb4c2d-af33-4221-bfd1-f6f3aa4c28b3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae1ea0d9-47e9-4839-a7d4-fd4548a2d1ab"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87d88726-21ec-4b20-8c61-9e043681b2d6"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookaround_y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""650845e7-20e9-4f2a-84b8-e6497c417c23"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_move = m_Player.FindAction("move", throwIfNotFound: true);
        m_Player_lookaround_x = m_Player.FindAction("lookaround_x", throwIfNotFound: true);
        m_Player_lookaround_y = m_Player.FindAction("lookaround_y", throwIfNotFound: true);
        m_Player_jump = m_Player.FindAction("jump", throwIfNotFound: true);
        m_Player_pick = m_Player.FindAction("pick", throwIfNotFound: true);
        m_Player_attack = m_Player.FindAction("attack", throwIfNotFound: true);
        m_Player_charge = m_Player.FindAction("charge", throwIfNotFound: true);
        m_Player_interact = m_Player.FindAction("interact", throwIfNotFound: true);
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
    private readonly InputAction m_Player_move;
    private readonly InputAction m_Player_lookaround_x;
    private readonly InputAction m_Player_lookaround_y;
    private readonly InputAction m_Player_jump;
    private readonly InputAction m_Player_pick;
    private readonly InputAction m_Player_attack;
    private readonly InputAction m_Player_charge;
    private readonly InputAction m_Player_interact;
    public struct PlayerActions
    {
        private @PlayerControl m_Wrapper;
        public PlayerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @move => m_Wrapper.m_Player_move;
        public InputAction @lookaround_x => m_Wrapper.m_Player_lookaround_x;
        public InputAction @lookaround_y => m_Wrapper.m_Player_lookaround_y;
        public InputAction @jump => m_Wrapper.m_Player_jump;
        public InputAction @pick => m_Wrapper.m_Player_pick;
        public InputAction @attack => m_Wrapper.m_Player_attack;
        public InputAction @charge => m_Wrapper.m_Player_charge;
        public InputAction @interact => m_Wrapper.m_Player_interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @lookaround_x.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_x;
                @lookaround_x.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_x;
                @lookaround_x.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_x;
                @lookaround_y.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_y;
                @lookaround_y.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_y;
                @lookaround_y.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookaround_y;
                @jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @pick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @pick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @pick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPick;
                @attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @charge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @charge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @charge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @move.started += instance.OnMove;
                @move.performed += instance.OnMove;
                @move.canceled += instance.OnMove;
                @lookaround_x.started += instance.OnLookaround_x;
                @lookaround_x.performed += instance.OnLookaround_x;
                @lookaround_x.canceled += instance.OnLookaround_x;
                @lookaround_y.started += instance.OnLookaround_y;
                @lookaround_y.performed += instance.OnLookaround_y;
                @lookaround_y.canceled += instance.OnLookaround_y;
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
                @pick.started += instance.OnPick;
                @pick.performed += instance.OnPick;
                @pick.canceled += instance.OnPick;
                @attack.started += instance.OnAttack;
                @attack.performed += instance.OnAttack;
                @attack.canceled += instance.OnAttack;
                @charge.started += instance.OnCharge;
                @charge.performed += instance.OnCharge;
                @charge.canceled += instance.OnCharge;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLookaround_x(InputAction.CallbackContext context);
        void OnLookaround_y(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPick(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnCharge(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
