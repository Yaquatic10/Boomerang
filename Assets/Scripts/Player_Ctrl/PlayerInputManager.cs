using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public GameObject spawnPointA;
    [SerializeField] public GameObject spawnPointB;

    private bool p1Joined = false;
    private bool p2Joined = false;
    private bool wasdPaired = false;
    private bool arrowsPaired = false;

    Gamepad p1Gamepad = null;
    Gamepad p2Gamepad = null;


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null) return;

        if (!p1Joined)
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame && !wasdPaired)
            {
                var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PKeyboard", pairWithDevice: Keyboard.current);

                player1.transform.position = spawnPointA.transform.position;
                p1Joined = true;
                wasdPaired = true;
                return;
            }

            if(Keyboard.current.rightShiftKey.wasPressedThisFrame && !arrowsPaired)
            {
                var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PKeyboard", pairWithDevice: Keyboard.current);

                player1.transform.position = spawnPointA.transform.position;
                p1Joined = true;
                arrowsPaired = true;
                return;
            }
        }

        if (!p2Joined)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !wasdPaired)
            {
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "1PKeyboard", pairWithDevice: Keyboard.current);
                player2.transform.position = spawnPointB.transform.position;
                p2Joined = true;
                wasdPaired = true;
                return;
            }

            if (Keyboard.current.rightShiftKey.wasPressedThisFrame && !arrowsPaired)
            {
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PKeyboard", pairWithDevice: Keyboard.current);

                player2.transform.position = spawnPointB.transform.position;
                p2Joined = true;
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
                p1Joined = true;
                p1Gamepad = gamePad;
                return;
            }

            if (!p2Joined && gamePad.buttonSouth.wasPressedThisFrame && gamePad != p1Gamepad)
            {
                var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "2PGamepad", pairWithDevice: Gamepad.current);
                player2.transform.position = spawnPointB.transform.position;
                p2Joined = true;
                p2Gamepad = gamePad;
                return;
            }
        }
    }

}
