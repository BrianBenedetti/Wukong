// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""be8ce5d8-9ec9-4751-9b94-551f550a1348"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ccc01c8b-c805-404e-833a-5dd17b067a59"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""72f20253-7b62-46ba-b71f-d71f0f2c4276"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""28ef0e41-242d-40ac-a130-2bf68d97ede6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary Attack"",
                    ""type"": ""Button"",
                    ""id"": ""86b76318-f0b6-4244-8d6d-e623865f7f9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c0e3837b-7c9b-4f5b-b609-4dc1fd6fa06e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8da7bd53-a507-4ad0-81ea-e5b6a0ff12e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""09c8e0c4-3bfd-449d-9529-fe1fd2b91f2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Attack"",
                    ""type"": ""Button"",
                    ""id"": ""eeb6f571-bdcf-4621-8aca-12e4da619caf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""71d090cb-bb17-4c27-86a6-0ca65e9f288f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rage"",
                    ""type"": ""Button"",
                    ""id"": ""157b5a7a-6113-41df-b97d-052a2c7e31fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use Item"",
                    ""type"": ""Button"",
                    ""id"": ""3af73615-32bc-404f-aae9-3a33f25a0f04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire Form"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ebf8eccc-32dc-489c-b079-a1885a8c01aa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Water Form"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ae1f592b-4337-413a-a445-0507dbd42965"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Air Form"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d84e4a4f-0d9d-44de-8d9c-94e2b543d54a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Normal Form"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e2e865a4-dcc5-4367-9fbb-238b3817fb77"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lock On"",
                    ""type"": ""Button"",
                    ""id"": ""d2ba923a-96e7-485e-a62f-954a47efe999"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch Target"",
                    ""type"": ""Button"",
                    ""id"": ""db8509fc-39a0-4f7c-ab19-aef1df47d41e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Cycle"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6fd161c8-53ec-43d0-a443-eb0b33a33699"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gamepad Cycle Right"",
                    ""type"": ""Button"",
                    ""id"": ""424cb6da-9907-4b12-bb56-d17e10b541e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gamepad Cycle Left"",
                    ""type"": ""Button"",
                    ""id"": ""9b37284d-e421-4945-806d-d516fc30870c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ad68c03e-c354-4014-bf83-a7c354a7ab99"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d3cb106f-bdff-40d2-b16e-2522977a5dfc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""195c00ad-045b-4bca-9a43-cb2233cc8299"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0bf71f3e-cc49-4687-9f59-ac7a099522a4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c707e0c1-1924-4727-a62e-b016656f2600"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""adbc5b81-68dc-4812-81a2-39f13e95556e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d7e0ad33-fd8b-44ce-9264-335500943e39"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc528deb-7ce4-4d6a-b596-e92ea445455c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe4ccd0a-ba3f-4731-bf5b-86a97652d3c9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ad6f3d1-8020-45a2-94d7-19a69b3f6817"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbeaa51a-de42-4f84-8751-0f82fdc2b8e1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Primary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85a1599e-e139-4416-9b4e-0b92b643d3c6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Primary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57b0948a-52df-43da-9081-04ac9b22b38e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Secondary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ecf53eb-5f11-4bb7-a7e0-2a2cb655112e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Secondary Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22e5f1b1-4736-4333-a68d-3a456427ed71"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64920c8a-2e27-4e7a-abd8-369d70364e6d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08816325-97eb-45ad-9a79-05e70ee467f4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3657bc4b-824b-4a93-9efe-f99994fb4422"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f6f3e52-d20b-4bac-8380-56e0ec60bfe0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d68e4a41-231c-4e29-bca3-5bab9a459ae1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d69a0e6d-4c2a-4d85-ac86-c97337b4b4c4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0f51d16-4800-43da-9a18-7331fa1c851b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca29d6a8-c014-4754-938a-cd9e8b4712d0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1928387f-99bb-4ac3-bcf4-8f30d1d41a7c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67e1196e-7e87-46d0-9644-2d32f5776d68"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e320654-3c30-4426-a0c9-98606162e24a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46bfb48e-6c31-413e-aa6f-ca88f7613bef"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Fire Form"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""115cecb0-4a18-40ac-8b1c-a12f5ab982e6"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Water Form"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79a0dfd1-9931-4e77-bc27-98f9a4ec5c65"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Air Form"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20dfce2d-d938-460f-aa95-9e9312d00ef9"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Normal Form"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3911b5aa-1a64-492f-a520-d0d0185a2897"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Lock On"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5970a89b-39cd-4c96-84c9-355978fb4933"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Lock On"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1de72784-37f5-4f56-8548-19f746007fc8"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96b38e93-6e45-4af8-ad0f-4786b1fc268d"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Switch Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e6f2fe6-407f-4c55-884f-845071977402"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Mouse Cycle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53624ee4-58e8-4053-87ee-1a249dd03860"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Gamepad Cycle Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1773b05-bfbb-4349-99c1-bc6435c894de"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Gamepad Cycle Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cutscene"",
            ""id"": ""d2be56f2-409e-4814-b65e-7ab37b5782b5"",
            ""actions"": [
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""d149f8bc-ca24-4a4c-8df4-cae2ac14d0f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d64c17c4-0083-4b42-a0d7-4c62e135c26b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd74d708-0d5a-4464-8536-e8d9f0db633e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4389ae78-1b1f-430d-bc0a-38c0f06d2b22"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59c80077-6076-4663-90b0-d8f8a4624ab5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5af0969b-a8ac-4e19-be75-f03c1bf5af36"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b7f2c58-68bc-411b-af41-3a6460f91006"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Camera = m_PlayerControls.FindAction("Camera", throwIfNotFound: true);
        m_PlayerControls_Jump = m_PlayerControls.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControls_PrimaryAttack = m_PlayerControls.FindAction("Primary Attack", throwIfNotFound: true);
        m_PlayerControls_SecondaryAttack = m_PlayerControls.FindAction("Secondary Attack", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        m_PlayerControls_Dodge = m_PlayerControls.FindAction("Dodge", throwIfNotFound: true);
        m_PlayerControls_SpecialAttack = m_PlayerControls.FindAction("Special Attack", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        m_PlayerControls_Rage = m_PlayerControls.FindAction("Rage", throwIfNotFound: true);
        m_PlayerControls_UseItem = m_PlayerControls.FindAction("Use Item", throwIfNotFound: true);
        m_PlayerControls_FireForm = m_PlayerControls.FindAction("Fire Form", throwIfNotFound: true);
        m_PlayerControls_WaterForm = m_PlayerControls.FindAction("Water Form", throwIfNotFound: true);
        m_PlayerControls_AirForm = m_PlayerControls.FindAction("Air Form", throwIfNotFound: true);
        m_PlayerControls_NormalForm = m_PlayerControls.FindAction("Normal Form", throwIfNotFound: true);
        m_PlayerControls_LockOn = m_PlayerControls.FindAction("Lock On", throwIfNotFound: true);
        m_PlayerControls_SwitchTarget = m_PlayerControls.FindAction("Switch Target", throwIfNotFound: true);
        m_PlayerControls_MouseCycle = m_PlayerControls.FindAction("Mouse Cycle", throwIfNotFound: true);
        m_PlayerControls_GamepadCycleRight = m_PlayerControls.FindAction("Gamepad Cycle Right", throwIfNotFound: true);
        m_PlayerControls_GamepadCycleLeft = m_PlayerControls.FindAction("Gamepad Cycle Left", throwIfNotFound: true);
        // Cutscene
        m_Cutscene = asset.FindActionMap("Cutscene", throwIfNotFound: true);
        m_Cutscene_Skip = m_Cutscene.FindAction("Skip", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Camera;
    private readonly InputAction m_PlayerControls_Jump;
    private readonly InputAction m_PlayerControls_PrimaryAttack;
    private readonly InputAction m_PlayerControls_SecondaryAttack;
    private readonly InputAction m_PlayerControls_Interact;
    private readonly InputAction m_PlayerControls_Dodge;
    private readonly InputAction m_PlayerControls_SpecialAttack;
    private readonly InputAction m_PlayerControls_Pause;
    private readonly InputAction m_PlayerControls_Rage;
    private readonly InputAction m_PlayerControls_UseItem;
    private readonly InputAction m_PlayerControls_FireForm;
    private readonly InputAction m_PlayerControls_WaterForm;
    private readonly InputAction m_PlayerControls_AirForm;
    private readonly InputAction m_PlayerControls_NormalForm;
    private readonly InputAction m_PlayerControls_LockOn;
    private readonly InputAction m_PlayerControls_SwitchTarget;
    private readonly InputAction m_PlayerControls_MouseCycle;
    private readonly InputAction m_PlayerControls_GamepadCycleRight;
    private readonly InputAction m_PlayerControls_GamepadCycleLeft;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Camera => m_Wrapper.m_PlayerControls_Camera;
        public InputAction @Jump => m_Wrapper.m_PlayerControls_Jump;
        public InputAction @PrimaryAttack => m_Wrapper.m_PlayerControls_PrimaryAttack;
        public InputAction @SecondaryAttack => m_Wrapper.m_PlayerControls_SecondaryAttack;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputAction @Dodge => m_Wrapper.m_PlayerControls_Dodge;
        public InputAction @SpecialAttack => m_Wrapper.m_PlayerControls_SpecialAttack;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputAction @Rage => m_Wrapper.m_PlayerControls_Rage;
        public InputAction @UseItem => m_Wrapper.m_PlayerControls_UseItem;
        public InputAction @FireForm => m_Wrapper.m_PlayerControls_FireForm;
        public InputAction @WaterForm => m_Wrapper.m_PlayerControls_WaterForm;
        public InputAction @AirForm => m_Wrapper.m_PlayerControls_AirForm;
        public InputAction @NormalForm => m_Wrapper.m_PlayerControls_NormalForm;
        public InputAction @LockOn => m_Wrapper.m_PlayerControls_LockOn;
        public InputAction @SwitchTarget => m_Wrapper.m_PlayerControls_SwitchTarget;
        public InputAction @MouseCycle => m_Wrapper.m_PlayerControls_MouseCycle;
        public InputAction @GamepadCycleRight => m_Wrapper.m_PlayerControls_GamepadCycleRight;
        public InputAction @GamepadCycleLeft => m_Wrapper.m_PlayerControls_GamepadCycleLeft;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Camera.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCamera;
                @Jump.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @PrimaryAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryAttack;
                @PrimaryAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryAttack;
                @PrimaryAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryAttack;
                @SecondaryAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryAttack;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Dodge.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @SpecialAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Rage.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRage;
                @Rage.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRage;
                @Rage.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRage;
                @UseItem.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUseItem;
                @FireForm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireForm;
                @FireForm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireForm;
                @FireForm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFireForm;
                @WaterForm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnWaterForm;
                @WaterForm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnWaterForm;
                @WaterForm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnWaterForm;
                @AirForm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAirForm;
                @AirForm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAirForm;
                @AirForm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAirForm;
                @NormalForm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalForm;
                @NormalForm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalForm;
                @NormalForm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalForm;
                @LockOn.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                @SwitchTarget.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchTarget;
                @SwitchTarget.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchTarget;
                @SwitchTarget.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchTarget;
                @MouseCycle.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMouseCycle;
                @MouseCycle.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMouseCycle;
                @MouseCycle.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMouseCycle;
                @GamepadCycleRight.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleRight;
                @GamepadCycleRight.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleRight;
                @GamepadCycleRight.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleRight;
                @GamepadCycleLeft.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleLeft;
                @GamepadCycleLeft.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleLeft;
                @GamepadCycleLeft.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGamepadCycleLeft;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @PrimaryAttack.started += instance.OnPrimaryAttack;
                @PrimaryAttack.performed += instance.OnPrimaryAttack;
                @PrimaryAttack.canceled += instance.OnPrimaryAttack;
                @SecondaryAttack.started += instance.OnSecondaryAttack;
                @SecondaryAttack.performed += instance.OnSecondaryAttack;
                @SecondaryAttack.canceled += instance.OnSecondaryAttack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Rage.started += instance.OnRage;
                @Rage.performed += instance.OnRage;
                @Rage.canceled += instance.OnRage;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @FireForm.started += instance.OnFireForm;
                @FireForm.performed += instance.OnFireForm;
                @FireForm.canceled += instance.OnFireForm;
                @WaterForm.started += instance.OnWaterForm;
                @WaterForm.performed += instance.OnWaterForm;
                @WaterForm.canceled += instance.OnWaterForm;
                @AirForm.started += instance.OnAirForm;
                @AirForm.performed += instance.OnAirForm;
                @AirForm.canceled += instance.OnAirForm;
                @NormalForm.started += instance.OnNormalForm;
                @NormalForm.performed += instance.OnNormalForm;
                @NormalForm.canceled += instance.OnNormalForm;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @SwitchTarget.started += instance.OnSwitchTarget;
                @SwitchTarget.performed += instance.OnSwitchTarget;
                @SwitchTarget.canceled += instance.OnSwitchTarget;
                @MouseCycle.started += instance.OnMouseCycle;
                @MouseCycle.performed += instance.OnMouseCycle;
                @MouseCycle.canceled += instance.OnMouseCycle;
                @GamepadCycleRight.started += instance.OnGamepadCycleRight;
                @GamepadCycleRight.performed += instance.OnGamepadCycleRight;
                @GamepadCycleRight.canceled += instance.OnGamepadCycleRight;
                @GamepadCycleLeft.started += instance.OnGamepadCycleLeft;
                @GamepadCycleLeft.performed += instance.OnGamepadCycleLeft;
                @GamepadCycleLeft.canceled += instance.OnGamepadCycleLeft;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Cutscene
    private readonly InputActionMap m_Cutscene;
    private ICutsceneActions m_CutsceneActionsCallbackInterface;
    private readonly InputAction m_Cutscene_Skip;
    public struct CutsceneActions
    {
        private @PlayerInputActions m_Wrapper;
        public CutsceneActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skip => m_Wrapper.m_Cutscene_Skip;
        public InputActionMap Get() { return m_Wrapper.m_Cutscene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CutsceneActions set) { return set.Get(); }
        public void SetCallbacks(ICutsceneActions instance)
        {
            if (m_Wrapper.m_CutsceneActionsCallbackInterface != null)
            {
                @Skip.started -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_CutsceneActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_CutsceneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public CutsceneActions @Cutscene => new CutsceneActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPrimaryAttack(InputAction.CallbackContext context);
        void OnSecondaryAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnRage(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnFireForm(InputAction.CallbackContext context);
        void OnWaterForm(InputAction.CallbackContext context);
        void OnAirForm(InputAction.CallbackContext context);
        void OnNormalForm(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnSwitchTarget(InputAction.CallbackContext context);
        void OnMouseCycle(InputAction.CallbackContext context);
        void OnGamepadCycleRight(InputAction.CallbackContext context);
        void OnGamepadCycleLeft(InputAction.CallbackContext context);
    }
    public interface ICutsceneActions
    {
        void OnSkip(InputAction.CallbackContext context);
    }
}
