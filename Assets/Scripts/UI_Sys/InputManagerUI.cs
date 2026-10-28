using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InputManagerUI : MonoBehaviour
{
    [SerializeField] private CtxDataBase _ctx;
    [SerializeField] private GameObject _spawnA;
    [SerializeField] private GameObject _spawnB;
    public static InputManagerUI instance;
    public bool p1SelectionInput { get; private set; }
    private bool p1Joined;
    private bool p2Joined;

    private PlayerInput p1UIInput;
    private PlayerInput p2UIInput;

    [SerializeField] private GameObject UIinputPrefab1;
    [SerializeField] private GameObject UIinputPrefab2;

    [Header("ELEMENTOS UI")]
    public GameObject Join1A;
    public GameObject Join2A;
    public GameObject CtrlsP1;
    public GameObject CtrlsP2;
    string _p1Using;
    string _p2Using;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (!p1Joined && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Join1A.SetActive(false);
            p1Joined = true;
            _p1Using = "1PKeyboard";
            InitializePlayer1(_p1Using);
            return;
        }
        /*else if (!p1Joined && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Join1A.SetActive(false);
            p1Joined = true;
            _p1Using = "1PGamepad";
            InitializePlayer1(_p1Using);
            return;
        }*/

        if (!p2Joined && Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            Join2A.SetActive(false);
            p2Joined = true;
            _p2Using = "2PKeyboard";
            InitializePlayer2(_p2Using);
            return;
        }
    }

    void InitializePlayer1(string controlSchemeName)
    {
        if (_p1Using == "1PKeyboard")
        {
            var player1 = PlayerInput.Instantiate(UIinputPrefab1, controlScheme: controlSchemeName, pairWithDevice: Keyboard.current);
            p1UIInput = player1.GetComponent<PlayerInput>();
            CharSelection _charSelection;
            _charSelection = player1.GetComponent<CharSelection>();
            _charSelection.EnableCharSelection(_p1Using);
            _ctx.P1ControlSchemeName = controlSchemeName;
        }

        if (_p1Using == "1PGamepad")
        {
            var player1 = PlayerInput.Instantiate(UIinputPrefab1, controlScheme: controlSchemeName, pairWithDevice: Gamepad.current);
            p1UIInput = player1.GetComponent<PlayerInput>();
            CharSelection _charSelection;
            _charSelection = player1.GetComponent<CharSelection>();
            _charSelection.EnableCharSelection(_p1Using);
            _ctx.P1ControlSchemeName = controlSchemeName;
        }
    }

    void InitializePlayer2(string controlSchemeName)
    {
        if ( _p2Using == "2PKeyboard")
        {
            var player2 = PlayerInput.Instantiate(UIinputPrefab2, controlScheme: controlSchemeName, pairWithDevice: Keyboard.current);
            p2UIInput = player2.GetComponent<PlayerInput>();
            CharSelection _charSelection;
            _charSelection = player2.GetComponent<CharSelection>();
            _charSelection.EnableCharSelection(_p2Using);
            _ctx.P2ControlSchemeName = controlSchemeName;
        }

        if (_p2Using == "2PGamepad")
        {
            var player2 = PlayerInput.Instantiate(UIinputPrefab2, controlScheme: controlSchemeName, pairWithDevice: Gamepad.current);
            p2UIInput = player2.GetComponent<PlayerInput>();
            CharSelection _charSelection;
            _charSelection = player2.GetComponent<CharSelection>();
            _charSelection.EnableCharSelection(_p2Using);
            _ctx.P2ControlSchemeName = controlSchemeName;
        }
    }

    public void CreateP1(GameObject characterPrefab, int charIndex)
    {
        if (p1UIInput == null) return;

        InputDevice[] p1Devices = p1UIInput.devices.ToArray();
        string currentScheme = p1UIInput.currentControlScheme;

        p1UIInput.enabled = false;
        p1UIInput.gameObject.SetActive(false);
        if (charIndex == 0) _ctx.P1CharIndex = 0;
        else if (charIndex == 1) _ctx.P1CharIndex = 1;
        else if (charIndex == 2) _ctx.P1CharIndex = 2;

        PlayerInput p1Gameplay = PlayerInput.Instantiate(
            characterPrefab,
            playerIndex: 0,
            controlScheme: currentScheme,
            pairWithDevices: p1Devices
        );
        p1Gameplay.transform.position = _spawnA.transform.position;
        p1Gameplay.gameObject.tag = "Player1";
        p1Gameplay.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();

        p1Gameplay.SwitchCurrentActionMap("Gameplay");
        CtrlsP1.SetActive(true);
    }

    public void CreateP2(GameObject characterPrefab, int charIndex)
    {
        if (p2UIInput == null) return;

        InputDevice[] p2Devices = p2UIInput.devices.ToArray();
        string currentScheme = p2UIInput.currentControlScheme;

        p2UIInput.enabled = false;
        p2UIInput.gameObject.SetActive(false);
        if (charIndex == 0) _ctx.P2CharIndex = 0;
        else if (charIndex == 1) _ctx.P2CharIndex = 1;
        else if (charIndex == 2) _ctx.P2CharIndex = 2;

        PlayerInput p2Gameplay = PlayerInput.Instantiate(
            characterPrefab,
            playerIndex: 0,
            controlScheme: currentScheme,
            pairWithDevices: p2Devices
        );
        p2Gameplay.transform.position = _spawnB.transform.position;
        p2Gameplay.gameObject.tag = "Player2";
        p2Gameplay.GetComponent<PlayerStateMachine>().SelfRecognitionAsP2();

        p2Gameplay.SwitchCurrentActionMap("Gameplay");
        CtrlsP2.SetActive(true);
    }
}
