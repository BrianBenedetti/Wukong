// GENERATED AUTOMATICALLY FROM 'Assets/Lee's Folder/Menu Assets/InputActions/MenuInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MenuInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""0aed571d-02f0-4706-8d43-de19f835a2ab"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""42f8f6b7-4d06-4561-b363-d51976d56f08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""BackB"",
                    ""type"": ""Button"",
                    ""id"": ""9c7000a3-8fe3-4724-b139-7a35a04cc150"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""16f31004-8aba-4087-9e18-3f522578f0ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""133bb6c3-e43e-435f-ac57-930f30599a4e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08da1485-2b74-46d4-90fe-af89a90fab23"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5a17607-55bd-46d2-b2d5-6967f86bc892"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e5f07ae-c2ac-411e-bdc2-e87b3ffd0f0d"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/button2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cee3014-c592-4572-9aff-5e1c7a7aaa5a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2847a3e6-9462-49d8-9203-c601848d14c7"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72cad36b-8f5f-4a3b-89a5-6e1e78b9f21e"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/button3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f511c02a-eca7-44f1-bf02-ca61d2b49c3f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d607fcb9-6e83-4c7e-a4a1-5fd5ba31dd0f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""e35955cb-2ed2-4081-a7d4-1d3a4f535317"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""12c6b0cc-85e5-45e1-8b8a-73945590b30d"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bb81b38a-29da-479c-b866-06ec6b730e2b"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboardpress"",
                    ""id"": ""f96fa434-c85b-4dd7-8c7e-f03a6dbaa47b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f17bd168-a207-46ba-81f1-99344727be4a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""411c9d37-d8ce-46a9-ba5c-8546bc6c4550"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b284c3df-55dc-47c0-8072-f187480dfe8c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""884ac665-e597-4299-9046-f7b4bf4f5d28"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""adb67cae-8e88-45b6-b7a0-e41f42f0b243"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""PauseInput"",
            ""id"": ""9c314b70-1765-4e64-9550-518dca5f2853"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2c8df1d4-322b-43f3-a107-9930c7c5e63a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""select"",
                    ""type"": ""Button"",
                    ""id"": ""6488f2ac-74ab-4824-ac2b-87175d0d74bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Resume"",
                    ""type"": ""Button"",
                    ""id"": ""518a9dc6-941b-48df-8d28-26a28d855936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd13d512-2dce-4290-8d95-2bc2cb6bc524"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""464c2543-71ac-4ec7-bd31-c6436085bcd8"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a42d1870-af99-4be3-ad27-7f5f36c07c48"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fe69dd8-1ebe-4bbe-99c5-a4d053914672"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""507d0241-bf56-4d66-a133-6341c4c32ec9"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Resume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0e10d21-58ca-4132-928e-aba887d005c3"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Resume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_Select = m_PlayerInput.FindAction("Select", throwIfNotFound: true);
        m_PlayerInput_BackB = m_PlayerInput.FindAction("BackB", throwIfNotFound: true);
        m_PlayerInput_Move = m_PlayerInput.FindAction("Move", throwIfNotFound: true);
        // PauseInput
        m_PauseInput = asset.FindActionMap("PauseInput", throwIfNotFound: true);
        m_PauseInput_Pause = m_PauseInput.FindAction("Pause", throwIfNotFound: true);
        m_PauseInput_select = m_PauseInput.FindAction("select", throwIfNotFound: true);
        m_PauseInput_Resume = m_PauseInput.FindAction("Resume", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_Select;
    private readonly InputAction m_PlayerInput_BackB;
    private readonly InputAction m_PlayerInput_Move;
    public struct PlayerInputActions
    {
        private @MenuInput m_Wrapper;
        public PlayerInputActions(@MenuInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_PlayerInput_Select;
        public InputAction @BackB => m_Wrapper.m_PlayerInput_BackB;
        public InputAction @Move => m_Wrapper.m_PlayerInput_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSelect;
                @BackB.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnBackB;
                @BackB.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnBackB;
                @BackB.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnBackB;
                @Move.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @BackB.started += instance.OnBackB;
                @BackB.performed += instance.OnBackB;
                @BackB.canceled += instance.OnBackB;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);

    // PauseInput
    private readonly InputActionMap m_PauseInput;
    private IPauseInputActions m_PauseInputActionsCallbackInterface;
    private readonly InputAction m_PauseInput_Pause;
    private readonly InputAction m_PauseInput_select;
    private readonly InputAction m_PauseInput_Resume;
    public struct PauseInputActions
    {
        private @MenuInput m_Wrapper;
        public PauseInputActions(@MenuInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_PauseInput_Pause;
        public InputAction @select => m_Wrapper.m_PauseInput_select;
        public InputAction @Resume => m_Wrapper.m_PauseInput_Resume;
        public InputActionMap Get() { return m_Wrapper.m_PauseInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseInputActions set) { return set.Get(); }
        public void SetCallbacks(IPauseInputActions instance)
        {
            if (m_Wrapper.m_PauseInputActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnPause;
                @select.started -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnSelect;
                @select.performed -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnSelect;
                @select.canceled -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnSelect;
                @Resume.started -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnResume;
                @Resume.performed -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnResume;
                @Resume.canceled -= m_Wrapper.m_PauseInputActionsCallbackInterface.OnResume;
            }
            m_Wrapper.m_PauseInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @select.started += instance.OnSelect;
                @select.performed += instance.OnSelect;
                @select.canceled += instance.OnSelect;
                @Resume.started += instance.OnResume;
                @Resume.performed += instance.OnResume;
                @Resume.canceled += instance.OnResume;
            }
        }
    }
    public PauseInputActions @PauseInput => new PauseInputActions(this);
    public interface IPlayerInputActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnBackB(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IPauseInputActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnResume(InputAction.CallbackContext context);
    }
}
