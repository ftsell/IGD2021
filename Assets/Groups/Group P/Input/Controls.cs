// GENERATED AUTOMATICALLY FROM 'Assets/Groups/Group P/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GroupP
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""463b321a-4db2-47dd-be92-f9fb8535bee4"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""d9f17c30-a143-4064-a89c-502a633205d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""85f0683f-f0c3-426a-8958-75dd70c6dcfe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""53ff5f7e-2308-498b-9fdd-e05fe874c257"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""6c79a9c1-2c51-48bb-b9aa-5e5e21f92ccd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCursor"",
                    ""type"": ""Value"",
                    ""id"": ""06518e7a-b6a2-4a58-be1f-b8888c8772c8"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7349430b-e514-4176-a2a6-997c28b32532"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD;KeyboardNum;KeyboardZGHJ;KeyboardPLÖÄ"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f3355a5-4fab-4045-9b0d-65f48d001c03"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77443c6f-ca6f-45b5-a423-797ef1404650"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55688cb2-1a1f-4e6f-83d5-7d10dbd2c752"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a110c80-25f5-42f9-ae0d-39fd27cefcf0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56a224d2-c894-466d-9144-048cf7eeb694"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de57e54e-7aba-41fc-83f2-701549e5387f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6617e752-af33-4b57-addc-1053fb265caa"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c724dc4-3ace-4a41-a2b1-5a50a790e8f8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""636d5fcf-08d5-417a-b454-45492291ce38"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee3ebba3-71ff-4318-81fb-dd59ad5a8360"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1f12057-a18a-4ebe-8eef-d83379b80115"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""360107c8-4784-4308-b35b-5a5d3f548e3a"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e77c4a96-1b8f-4046-b7f0-b35b3eb9e10e"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""604f3457-53b9-41fa-8cb1-80cd4c248fdb"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd8cef48-f536-4fa4-9676-a134999df074"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d1040f-dd00-4337-acc7-257cfe4af39e"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""335a82d9-611d-469f-ac13-1400e2930400"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6449c09-ef69-4cc3-9fbf-83485e8ea5f3"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0dc0157-60d6-404a-81cd-2f9c99469710"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe1dab5e-d690-4b45-ab38-13a351ee0258"",
                    ""path"": ""<Keyboard>/semicolon"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbae5da5-2cfb-410b-937c-1774ab9216da"",
                    ""path"": ""<Keyboard>/quote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59f795d3-15af-4716-ab0f-68b74aa173bc"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""376a1035-f8b1-456c-9f42-f2f99e628df9"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2e2dc07-35f5-49e5-95f7-3e5d05fff0a5"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1568f4c4-a825-4abb-a20f-e871cd702c8b"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a3e9db3-07d7-463c-af13-941127588256"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beb0b0d9-5d34-4e31-a053-db4077b0a585"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32724c78-965b-4ae7-9c06-f8301558d19c"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c2e7abc-809d-458e-b7d5-49071f5bc5f2"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00f40931-20ce-4136-b3f2-f5be29f7c6bc"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""304cae92-c6fb-4f26-82aa-6b56c0ae4563"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e1cbe3f-cc53-4f05-86b2-65004c126ee4"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c2d1030-bbe6-4d1f-8f6e-408b64383a83"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bb6b0ed-1916-49b0-a9ec-ec3652dbd336"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a71aee7-c523-4b17-9012-bbb1244a75cd"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c69cc569-a6a4-4501-aec2-fbf71eff2b33"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""886d738c-709e-4bc2-a966-5005179fbd45"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e9275f5-5ca7-41ca-a420-29a82be24361"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee6bb28a-b8d5-4c3f-b6db-8f856b640b54"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc36c1a9-e5e0-4bc5-8b71-14bcae133d62"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d11ec779-6180-4870-be62-525bc7a5885c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88923d4f-7aac-4f11-b627-7a4077cf6faa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""179c2478-b5ef-464b-b81c-809d4610b46e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67fdcb0c-6e95-4435-8785-7d68326b618a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b102bd6-b26b-48b1-a13c-4f3556b633b8"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52baf35c-de54-4640-ab16-39a66b5ce7e4"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b6854aa-10b5-4a5b-bb79-765d1186d578"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a0eb765-aaa7-472a-8390-cc2ac5cde548"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e556224-add7-470b-aeab-c3c747372fd7"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a53e094-0942-49f5-b503-990eeb7a4c76"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7253aa8f-8381-4bde-8481-783d27614eb3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c75121d9-11fc-4431-a354-4239effad7d2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a513e95e-d5bd-4e2c-8a9f-2e629a5bd363"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a0cd868-102e-409c-b502-f89a2d53cf80"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ccc8cf2-ca62-478c-b17c-8b7ffa043b0d"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62a11c6f-6f4f-4fbd-873a-6b5c0da633c8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c818921-035c-43fc-8825-80fb03d057f8"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b55141c6-367c-469f-bd29-ee89045bff08"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8f343fb-d771-43e1-8b78-8a43f93ada21"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ac46d3f-cbd7-4f85-8e75-09b08e532837"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Ingame"",
            ""id"": ""3adf9f16-1260-4c9d-82dc-41c3961a53ba"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""81e001f2-d0c9-4b5b-958c-505c90a35c44"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NorthPress"",
                    ""type"": ""Button"",
                    ""id"": ""83114928-12f1-4006-96e8-1c2cd279dff7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""NorthRelease"",
                    ""type"": ""Button"",
                    ""id"": ""6570f3bd-3a73-48e3-90c2-a2bead98bddf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""EastPress"",
                    ""type"": ""Button"",
                    ""id"": ""40c78e3b-d7b8-41c0-9ea3-75e6b6f43f63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""EastRelease"",
                    ""type"": ""Button"",
                    ""id"": ""c0e1a67d-580a-4876-99db-28c4ff1ae382"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""SouthPress"",
                    ""type"": ""Button"",
                    ""id"": ""530c49a9-a4a7-4eef-a74c-1c9e4700e3de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SouthRelease"",
                    ""type"": ""Button"",
                    ""id"": ""74328aec-ae69-4a61-82e1-718f95a41a38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""WestPress"",
                    ""type"": ""Button"",
                    ""id"": ""26c1e719-bc99-4afe-95f9-29e9704abc33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""WestRelease"",
                    ""type"": ""Button"",
                    ""id"": ""0e67dc2b-9baa-41c7-85b7-01481a97b1bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""809e7133-2413-4ec2-92a3-eba7f433aa4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpPress"",
                    ""type"": ""Button"",
                    ""id"": ""068b276c-07aa-4a3f-8e9d-bd8f2e0e3898"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LeftPress"",
                    ""type"": ""Button"",
                    ""id"": ""302c977c-33b7-4357-a25c-37a180d26f4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RightPress"",
                    ""type"": ""Button"",
                    ""id"": ""4d9db543-5c91-4145-b191-20934086ef94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DownPress"",
                    ""type"": ""Button"",
                    ""id"": ""dc478f70-2cff-48b0-bf53-63602956002b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""654a85a2-87e9-45a8-b40d-d01d5fed7b2e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98db7e4c-82f6-47dd-82c4-1816e0a5b2c8"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NorthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6ad969e-c09e-404a-8ed3-4ab386d67c36"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""NorthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""165fff8f-1ac1-47d6-8e1f-d57379a343f0"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""NorthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f93d5915-1c75-496a-9b9c-4d7bf6cd192c"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""NorthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdcf3c3b-1c51-4d66-ba40-d709a3bd1ffb"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""NorthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bdeff8a-46a5-46c2-9004-987a5b9cae0b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f46eba1-1e1c-4ead-b0ba-57ba9ff02fe4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD;KeyboardNum;KeyboardZGHJ;KeyboardPLÖÄ"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3359a7ba-82ff-4eb2-857e-19c88ce71c9a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""WestPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70fc8f7a-0e2b-499a-a3f8-3f8d93f58745"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""WestPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""379e9169-1837-4163-852d-097763184d78"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""WestPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44571425-6da9-46fe-bd9b-c3c8937c0f17"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""WestPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60f57515-3b21-4761-9534-c3b9779c5fc8"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""WestPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""549ae250-f11e-47ae-a919-ed46daf319b3"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""WestRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""603ef004-5843-4c78-8d08-50e0d59a22cb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""WestRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cd8a652-1e06-4613-a580-17dc6444271e"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""WestRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fa1f139-26cc-48a5-bc52-73370b7dc427"",
                    ""path"": ""<Keyboard>/backslash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""WestRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89d259a3-e3dc-4ddf-b8c7-530ef5a845e5"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""WestRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""366dd9d8-c7a9-40df-a6e4-986b48b4a14c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""EastPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52292e66-166b-410b-9d1e-fb54c6006419"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""EastPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d246820d-ad55-4428-81e2-e86a5dd1b633"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""EastPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3657f33c-53ad-44d4-b79a-27d93a75882d"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""EastPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b6c2b24-2b82-46aa-9bc2-3b698b986534"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""EastPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8e2a17f-9f9c-4e21-8dee-d9944fe089ce"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""EastRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2d60596-5e07-40b9-946a-9d89be9a6258"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""EastRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcb487d5-e5ce-4201-a6dc-aa854febdeea"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""EastRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd824ae8-580e-4828-bc2a-f0a3e36c4926"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""EastRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""430e62a1-bd02-491a-8a77-4f980bdd177e"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""EastRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcbeeb9f-8501-48b8-9352-0f2c733e12c5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NorthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe324ca6-3e34-4c3f-9d51-17138fdca32d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""NorthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60524d28-dc9d-4ff1-a788-11f0aa62360b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""NorthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3ae1e19-7d4e-44c9-ba85-cd1069eab6ce"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""NorthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d503fbaf-725e-4f23-80e9-676b554c4cf8"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""NorthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d817defa-bf7b-4bad-9603-52e6843fdbc5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SouthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25aace29-0d05-423d-9976-d6ae32cdaf0d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""SouthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""446affce-ace3-42ad-96a8-975c875156ec"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""SouthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3f97167-3265-4800-9e51-ec9f24350570"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""SouthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f020395-0ccf-431f-bc67-8a1170c9b9b4"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""SouthRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7fd134c-1cc1-44e9-8208-0a0348ae892a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SouthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96ad9d73-3b42-4645-a1ed-bf8639e91401"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""SouthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b47e7e2-4be1-4094-8ba6-b8b8feb20f59"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""SouthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3176d34-82c2-407e-aa29-9bbe9a7a3373"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""SouthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc88b22a-6244-40c7-9b1d-96b8cd05f657"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""SouthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f73cde66-f61d-4035-9f14-88bda2450810"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""UpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64906940-b0a5-455e-a361-6c648dcccdec"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""UpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32f2c2f7-a5c8-4e54-a851-263047763845"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""UpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fd63156-19b4-4128-aa88-50bc360e3a44"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""UpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""028520bf-1e04-445a-9de4-f535e8c6cf9e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e0c17a3-9dbc-411f-b32c-eb4395e0d913"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""LeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4304d09f-f316-41c0-8a6a-497ef607b5b2"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""LeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""244ed5be-58d3-47a8-b274-ab24c4ed8ca5"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""LeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""502a0609-7420-493b-8881-618304f587c9"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""LeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1657c9fd-c5c6-471c-8631-6e559b772051"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0bc9643-ede3-4ebc-82e7-9582d74379b0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""RightPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34c5fe40-484f-4e1d-95b7-9b3180c9b82c"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""RightPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c13bcf7-de05-4c81-bf31-08718e14ffb5"",
                    ""path"": ""<Keyboard>/quote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""RightPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fa5be0d-9c93-4c44-9cba-86c012566a80"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""RightPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dd307e3-8bd4-4cab-a09c-aa558663d2df"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b76c8192-248d-4cb2-a20e-27b6f6c8ad12"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardWASD"",
                    ""action"": ""DownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb4a2d90-9bca-479d-ae85-71dee7746a15"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardZGHJ"",
                    ""action"": ""DownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0212fd55-0b74-4cab-ac67-9567d231420f"",
                    ""path"": ""<Keyboard>/semicolon"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPLÖÄ"",
                    ""action"": ""DownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b51f93f-b8f6-42fa-89e3-98cc68078aa8"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardNum"",
                    ""action"": ""DownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fb0a486-9b8c-49b3-8feb-bdd2ae02e6a6"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DownPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardWASD"",
            ""bindingGroup"": ""KeyboardWASD"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardNum"",
            ""bindingGroup"": ""KeyboardNum"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
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
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardZGHJ"",
            ""bindingGroup"": ""KeyboardZGHJ"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardPLÖÄ"",
            ""bindingGroup"": ""KeyboardPLÖÄ"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_Start = m_Menu.FindAction("Start", throwIfNotFound: true);
            m_Menu_Join = m_Menu.FindAction("Join", throwIfNotFound: true);
            m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
            m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
            m_Menu_MoveCursor = m_Menu.FindAction("MoveCursor", throwIfNotFound: true);
            // Ingame
            m_Ingame = asset.FindActionMap("Ingame", throwIfNotFound: true);
            m_Ingame_Move = m_Ingame.FindAction("Move", throwIfNotFound: true);
            m_Ingame_NorthPress = m_Ingame.FindAction("NorthPress", throwIfNotFound: true);
            m_Ingame_NorthRelease = m_Ingame.FindAction("NorthRelease", throwIfNotFound: true);
            m_Ingame_EastPress = m_Ingame.FindAction("EastPress", throwIfNotFound: true);
            m_Ingame_EastRelease = m_Ingame.FindAction("EastRelease", throwIfNotFound: true);
            m_Ingame_SouthPress = m_Ingame.FindAction("SouthPress", throwIfNotFound: true);
            m_Ingame_SouthRelease = m_Ingame.FindAction("SouthRelease", throwIfNotFound: true);
            m_Ingame_WestPress = m_Ingame.FindAction("WestPress", throwIfNotFound: true);
            m_Ingame_WestRelease = m_Ingame.FindAction("WestRelease", throwIfNotFound: true);
            m_Ingame_Menu = m_Ingame.FindAction("Menu", throwIfNotFound: true);
            m_Ingame_UpPress = m_Ingame.FindAction("UpPress", throwIfNotFound: true);
            m_Ingame_LeftPress = m_Ingame.FindAction("LeftPress", throwIfNotFound: true);
            m_Ingame_RightPress = m_Ingame.FindAction("RightPress", throwIfNotFound: true);
            m_Ingame_DownPress = m_Ingame.FindAction("DownPress", throwIfNotFound: true);
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

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private readonly InputAction m_Menu_Start;
        private readonly InputAction m_Menu_Join;
        private readonly InputAction m_Menu_Submit;
        private readonly InputAction m_Menu_Cancel;
        private readonly InputAction m_Menu_MoveCursor;
        public struct MenuActions
        {
            private @Controls m_Wrapper;
            public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Start => m_Wrapper.m_Menu_Start;
            public InputAction @Join => m_Wrapper.m_Menu_Join;
            public InputAction @Submit => m_Wrapper.m_Menu_Submit;
            public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
            public InputAction @MoveCursor => m_Wrapper.m_Menu_MoveCursor;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    @Start.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
                    @Start.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
                    @Start.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
                    @Join.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                    @Join.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                    @Join.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                    @Submit.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                    @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @MoveCursor.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveCursor;
                    @MoveCursor.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveCursor;
                    @MoveCursor.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveCursor;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Start.started += instance.OnStart;
                    @Start.performed += instance.OnStart;
                    @Start.canceled += instance.OnStart;
                    @Join.started += instance.OnJoin;
                    @Join.performed += instance.OnJoin;
                    @Join.canceled += instance.OnJoin;
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                    @MoveCursor.started += instance.OnMoveCursor;
                    @MoveCursor.performed += instance.OnMoveCursor;
                    @MoveCursor.canceled += instance.OnMoveCursor;
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);

        // Ingame
        private readonly InputActionMap m_Ingame;
        private IIngameActions m_IngameActionsCallbackInterface;
        private readonly InputAction m_Ingame_Move;
        private readonly InputAction m_Ingame_NorthPress;
        private readonly InputAction m_Ingame_NorthRelease;
        private readonly InputAction m_Ingame_EastPress;
        private readonly InputAction m_Ingame_EastRelease;
        private readonly InputAction m_Ingame_SouthPress;
        private readonly InputAction m_Ingame_SouthRelease;
        private readonly InputAction m_Ingame_WestPress;
        private readonly InputAction m_Ingame_WestRelease;
        private readonly InputAction m_Ingame_Menu;
        private readonly InputAction m_Ingame_UpPress;
        private readonly InputAction m_Ingame_LeftPress;
        private readonly InputAction m_Ingame_RightPress;
        private readonly InputAction m_Ingame_DownPress;
        public struct IngameActions
        {
            private @Controls m_Wrapper;
            public IngameActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Ingame_Move;
            public InputAction @NorthPress => m_Wrapper.m_Ingame_NorthPress;
            public InputAction @NorthRelease => m_Wrapper.m_Ingame_NorthRelease;
            public InputAction @EastPress => m_Wrapper.m_Ingame_EastPress;
            public InputAction @EastRelease => m_Wrapper.m_Ingame_EastRelease;
            public InputAction @SouthPress => m_Wrapper.m_Ingame_SouthPress;
            public InputAction @SouthRelease => m_Wrapper.m_Ingame_SouthRelease;
            public InputAction @WestPress => m_Wrapper.m_Ingame_WestPress;
            public InputAction @WestRelease => m_Wrapper.m_Ingame_WestRelease;
            public InputAction @Menu => m_Wrapper.m_Ingame_Menu;
            public InputAction @UpPress => m_Wrapper.m_Ingame_UpPress;
            public InputAction @LeftPress => m_Wrapper.m_Ingame_LeftPress;
            public InputAction @RightPress => m_Wrapper.m_Ingame_RightPress;
            public InputAction @DownPress => m_Wrapper.m_Ingame_DownPress;
            public InputActionMap Get() { return m_Wrapper.m_Ingame; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(IngameActions set) { return set.Get(); }
            public void SetCallbacks(IIngameActions instance)
            {
                if (m_Wrapper.m_IngameActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnMove;
                    @NorthPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthPress;
                    @NorthPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthPress;
                    @NorthPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthPress;
                    @NorthRelease.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthRelease;
                    @NorthRelease.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthRelease;
                    @NorthRelease.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnNorthRelease;
                    @EastPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastPress;
                    @EastPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastPress;
                    @EastPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastPress;
                    @EastRelease.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastRelease;
                    @EastRelease.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastRelease;
                    @EastRelease.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnEastRelease;
                    @SouthPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthPress;
                    @SouthPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthPress;
                    @SouthPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthPress;
                    @SouthRelease.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthRelease;
                    @SouthRelease.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthRelease;
                    @SouthRelease.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnSouthRelease;
                    @WestPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestPress;
                    @WestPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestPress;
                    @WestPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestPress;
                    @WestRelease.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestRelease;
                    @WestRelease.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestRelease;
                    @WestRelease.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnWestRelease;
                    @Menu.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnMenu;
                    @UpPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnUpPress;
                    @UpPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnUpPress;
                    @UpPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnUpPress;
                    @LeftPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnLeftPress;
                    @LeftPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnLeftPress;
                    @LeftPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnLeftPress;
                    @RightPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnRightPress;
                    @RightPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnRightPress;
                    @RightPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnRightPress;
                    @DownPress.started -= m_Wrapper.m_IngameActionsCallbackInterface.OnDownPress;
                    @DownPress.performed -= m_Wrapper.m_IngameActionsCallbackInterface.OnDownPress;
                    @DownPress.canceled -= m_Wrapper.m_IngameActionsCallbackInterface.OnDownPress;
                }
                m_Wrapper.m_IngameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @NorthPress.started += instance.OnNorthPress;
                    @NorthPress.performed += instance.OnNorthPress;
                    @NorthPress.canceled += instance.OnNorthPress;
                    @NorthRelease.started += instance.OnNorthRelease;
                    @NorthRelease.performed += instance.OnNorthRelease;
                    @NorthRelease.canceled += instance.OnNorthRelease;
                    @EastPress.started += instance.OnEastPress;
                    @EastPress.performed += instance.OnEastPress;
                    @EastPress.canceled += instance.OnEastPress;
                    @EastRelease.started += instance.OnEastRelease;
                    @EastRelease.performed += instance.OnEastRelease;
                    @EastRelease.canceled += instance.OnEastRelease;
                    @SouthPress.started += instance.OnSouthPress;
                    @SouthPress.performed += instance.OnSouthPress;
                    @SouthPress.canceled += instance.OnSouthPress;
                    @SouthRelease.started += instance.OnSouthRelease;
                    @SouthRelease.performed += instance.OnSouthRelease;
                    @SouthRelease.canceled += instance.OnSouthRelease;
                    @WestPress.started += instance.OnWestPress;
                    @WestPress.performed += instance.OnWestPress;
                    @WestPress.canceled += instance.OnWestPress;
                    @WestRelease.started += instance.OnWestRelease;
                    @WestRelease.performed += instance.OnWestRelease;
                    @WestRelease.canceled += instance.OnWestRelease;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @UpPress.started += instance.OnUpPress;
                    @UpPress.performed += instance.OnUpPress;
                    @UpPress.canceled += instance.OnUpPress;
                    @LeftPress.started += instance.OnLeftPress;
                    @LeftPress.performed += instance.OnLeftPress;
                    @LeftPress.canceled += instance.OnLeftPress;
                    @RightPress.started += instance.OnRightPress;
                    @RightPress.performed += instance.OnRightPress;
                    @RightPress.canceled += instance.OnRightPress;
                    @DownPress.started += instance.OnDownPress;
                    @DownPress.performed += instance.OnDownPress;
                    @DownPress.canceled += instance.OnDownPress;
                }
            }
        }
        public IngameActions @Ingame => new IngameActions(this);
        private int m_KeyboardWASDSchemeIndex = -1;
        public InputControlScheme KeyboardWASDScheme
        {
            get
            {
                if (m_KeyboardWASDSchemeIndex == -1) m_KeyboardWASDSchemeIndex = asset.FindControlSchemeIndex("KeyboardWASD");
                return asset.controlSchemes[m_KeyboardWASDSchemeIndex];
            }
        }
        private int m_KeyboardNumSchemeIndex = -1;
        public InputControlScheme KeyboardNumScheme
        {
            get
            {
                if (m_KeyboardNumSchemeIndex == -1) m_KeyboardNumSchemeIndex = asset.FindControlSchemeIndex("KeyboardNum");
                return asset.controlSchemes[m_KeyboardNumSchemeIndex];
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
        private int m_KeyboardZGHJSchemeIndex = -1;
        public InputControlScheme KeyboardZGHJScheme
        {
            get
            {
                if (m_KeyboardZGHJSchemeIndex == -1) m_KeyboardZGHJSchemeIndex = asset.FindControlSchemeIndex("KeyboardZGHJ");
                return asset.controlSchemes[m_KeyboardZGHJSchemeIndex];
            }
        }
        private int m_KeyboardPLÖÄSchemeIndex = -1;
        public InputControlScheme KeyboardPLÖÄScheme
        {
            get
            {
                if (m_KeyboardPLÖÄSchemeIndex == -1) m_KeyboardPLÖÄSchemeIndex = asset.FindControlSchemeIndex("KeyboardPLÖÄ");
                return asset.controlSchemes[m_KeyboardPLÖÄSchemeIndex];
            }
        }
        public interface IMenuActions
        {
            void OnStart(InputAction.CallbackContext context);
            void OnJoin(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnMoveCursor(InputAction.CallbackContext context);
        }
        public interface IIngameActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnNorthPress(InputAction.CallbackContext context);
            void OnNorthRelease(InputAction.CallbackContext context);
            void OnEastPress(InputAction.CallbackContext context);
            void OnEastRelease(InputAction.CallbackContext context);
            void OnSouthPress(InputAction.CallbackContext context);
            void OnSouthRelease(InputAction.CallbackContext context);
            void OnWestPress(InputAction.CallbackContext context);
            void OnWestRelease(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnUpPress(InputAction.CallbackContext context);
            void OnLeftPress(InputAction.CallbackContext context);
            void OnRightPress(InputAction.CallbackContext context);
            void OnDownPress(InputAction.CallbackContext context);
        }
    }
}
