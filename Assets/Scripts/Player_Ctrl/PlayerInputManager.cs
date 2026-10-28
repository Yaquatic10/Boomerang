using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public GameObject spawnPointA;
    [SerializeField] public GameObject spawnPointB;
    private CtxDataBase ctx;

    public GameObject[] _player1;
    public GameObject[] _player2;

    [SerializeField] public HealthManager _healthManager;

    private GameObject[] _spawners;

    private bool p1Joined = false;
    private bool p2Joined = false;
    private bool wasdPaired = false;
    private bool arrowsPaired = false;

    private bool canSelect1;
    private bool canSelect2;

    Gamepad p1Gamepad = null;
    Gamepad p2Gamepad = null;


    // Update is called once per frame
    void Update()
    {
        /*if (Keyboard.current == null) return;

        if (!p1Joined)
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame && !wasdPaired)
            {
                Debug.Log("JUGADOR 1 USANDO: WASD");
                var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PKeyboard", pairWithDevice: Keyboard.current);
                player1.transform.position = spawnPointA.transform.position;

                player1.gameObject.tag = "Player1";
                player1.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
                //_healthManager.Player1Initialize();
                
                p1Joined = true;
                ctx.IsP1 = true;
                wasdPaired = true;
                return;
            }

            if(Keyboard.current.rightShiftKey.wasPressedThisFrame && !arrowsPaired)
            {
                Debug.Log("JUGADOR 1 USANDO: ARROWS");
                var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PKeyboard", pairWithDevice: Keyboard.current);
                player1.transform.position = spawnPointA.transform.position;

                player1.gameObject.tag = "Player1";
                player1.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
                //_healthManager.Player1Initialize();

                p1Joined = true;
                ctx.IsP1 = true;
                arrowsPaired = true;
                return;
            }
        }

        if (!p2Joined)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !wasdPaired)
            {
                Debug.Log("JUGADOR 2 USANDO: WASD");
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PKeyboard", pairWithDevice: Keyboard.current);
                player2.transform.position = spawnPointB.transform.position;

                player2.gameObject.tag = "Player2";
                player2.GetComponent<PlayerStateMachine>().SelfRecognitionAsP2();
                //_healthManager.Player2Initialize();
                
                Join2A.SetActive(false);
                Join2B.SetActive(false);
                Join2C.SetActive(false);

                if (!p1Joined)
                {
                    Join1A.SetActive(false);
                    Join1B.SetActive(false);
                    Join1C.SetActive(true);
                }
                //InitializePlayer("1PKeyboard", Keyboard.current);

                p2Joined = true;
                ctx.IsP1 = false;
                wasdPaired = true;
                return;
            }

            if (Keyboard.current.rightShiftKey.wasPressedThisFrame && !arrowsPaired)
            {
                Debug.Log("JUGADOR 2 USANDO: ARROWS");
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PKeyboard", pairWithDevice: Keyboard.current);
                player2.transform.position = spawnPointB.transform.position;

                player2.gameObject.tag = "Player2";
                player2.GetComponent<PlayerStateMachine>().SelfRecognitionAsP2();
                //_healthManager.Player2Initialize();

                Join2A.SetActive(false);
                Join2B.SetActive(false);
                Join2C.SetActive(false);

                if (!p1Joined)
                {
                    Join1A.SetActive(false);
                    Join1B.SetActive(true);
                    Join1C.SetActive(false);
                }
                //InitializePlayer("2PKeyboard", Keyboard.current);

                p2Joined = true;
                ctx.IsP1 = false;
                arrowsPaired = true;
                return;
            }
        }

        foreach (var gamePad in Gamepad.all)
        {
            if (!p1Joined && gamePad.buttonSouth.wasPressedThisFrame)
            {
                var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PGamepad", pairWithDevice: Gamepad.current);
                player1.transform.position = spawnPointA.transform.position;

                player1.gameObject.tag = "Player2";
                player1.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
                //_healthManager.Player1Initialize();

                Join1A.SetActive(false);
                Join1B.SetActive(false);
                Join1C.SetActive(false);

                //InitializePlayer("1PGamepad", Keyboard.current);

                p1Joined = true;
                ctx.IsP1 = true;
                p1Gamepad = gamePad;
                return;
            }

            if (!p2Joined && gamePad.buttonSouth.wasPressedThisFrame && gamePad != p1Gamepad)
            {
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PGamepad", pairWithDevice: Gamepad.current);
                player2.transform.position = spawnPointB.transform.position;

                player2.gameObject.tag = "Player2";
                player2.GetComponent<PlayerStateMachine>().SelfRecognitionAsP2();
                //_healthManager.Player2Initialize();

                Join2A.SetActive(false);
                Join2B.SetActive(false);
                Join2C.SetActive(false);
                //InitializePlayer("2PGamepad", Keyboard.current);

                p2Joined = true;
                ctx.IsP1 = false;
                p2Gamepad = gamePad;
                return;
            }
        }*/
    }

    private void Awake()
    {
        _spawners = GameObject.FindGameObjectsWithTag("Respawn");
        ctx = FindAnyObjectByType<CtxDataBase>();

    }
    public void InitializeP1()
    {
        if (ctx.P1ControlSchemeName == "1PKeyboard")
        {
            var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PKeyboard", pairWithDevice: Keyboard.current);
            player1.transform.position = _spawners[0].transform.position;

            player1.gameObject.tag = "Player1";
            player1.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
            _healthManager.Player1Initialize();
            return;
        }

        else if (ctx.P1ControlSchemeName == "1PGamepad")
        {
            var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PGamepad", pairWithDevice: Gamepad.current);
            player1.transform.position = _spawners[0].transform.position;

            player1.gameObject.tag = "Player2";
            player1.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
            _healthManager.Player1Initialize();
            return;
        }
    }

    public void InitializeP2()
    {
        if (ctx.P2ControlSchemeName == "P2Keyboard")
        {
            var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PKeyboard", pairWithDevice: Keyboard.current);
            player2.transform.position = _spawners[1].transform.position;

            player2.gameObject.tag = "Player2";
            player2.GetComponent<PlayerStateMachine>().SelfRecognitionAsP2();
            _healthManager.Player2Initialize();
            return;
        }

        else if (ctx.P2ControlSchemeName == "2PGamepad")
        {
            var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PGamepad", pairWithDevice: Gamepad.current);
            player2.transform.position = _spawners[1].transform.position;

            player2.gameObject.tag = "Player2";
            player2.GetComponent<PlayerStateMachine>().SelfRecognitionAsP1();
            _healthManager.Player1Initialize();
            return;
        }
    }
}
