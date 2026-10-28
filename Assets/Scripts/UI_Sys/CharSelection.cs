using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CharSelection : MonoBehaviour
{
    [SerializeField] private GameObject _char1MenuCanvas;
    [SerializeField] private GameObject _charFirst;
    [SerializeField] private CtxDataBase ctx;
    private InputManagerUI _inputManager;
    private PlayerInput _playerInput;
    private MultiplayerEventSystem _multiplayerEventSystem;

    private string _controlSchemeName;
    private GameObject _characterPrefab;

    [Header("CHARACTER PREFABS")]
    public GameObject MonkeyPrefab;
    public GameObject ApePrefab;
    public GameObject GorilaPrefab;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _multiplayerEventSystem = GetComponent<MultiplayerEventSystem>();
        ctx = FindAnyObjectByType<CtxDataBase>();
        _inputManager = FindAnyObjectByType<InputManagerUI>();
    }
    public void EnableCharSelection(string ControlSchemeName)
    {
        _controlSchemeName = ControlSchemeName;
        _char1MenuCanvas.SetActive(true);
        if (_multiplayerEventSystem != null )
        {
            _multiplayerEventSystem.SetSelectedGameObject(_charFirst);
        }
    }

    public void onMonkeySelection()
    {
        if (!_char1MenuCanvas.activeSelf) return;

        _char1MenuCanvas.SetActive(false);

        if (_controlSchemeName == "1PKeyboard" || _controlSchemeName == "1PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = MonkeyPrefab;
            int charIndex = 0;
            _inputManager.CreateP1(_characterPrefab, charIndex);
        }

        if (_controlSchemeName == "2PKeyboard" || _controlSchemeName == "2PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = MonkeyPrefab;
            int charIndex = 0;
            _inputManager.CreateP2(_characterPrefab, charIndex);
        }
    }

    public void onApeSelection()
    {
        if (!_char1MenuCanvas.activeSelf) return;

        _char1MenuCanvas.SetActive(false);

        if (_controlSchemeName == "1PKeyboard" || _controlSchemeName == "1PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = ApePrefab;
            int charIndex = 1;
            _inputManager.CreateP1(_characterPrefab, charIndex);
        }

        if (_controlSchemeName == "2PKeyboard" || _controlSchemeName == "2PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = ApePrefab;
            int charIndex = 1;
            _inputManager.CreateP2(_characterPrefab, charIndex);
        }
    }

    public void onGorilaSelection()
    {
        if (!_char1MenuCanvas.activeSelf) return;

        _char1MenuCanvas.SetActive(false);

        if (_controlSchemeName == "1PKeyboard" || _controlSchemeName == "1PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = GorilaPrefab;
            int charIndex = 2;
            _inputManager.CreateP1(_characterPrefab, charIndex);
        }

        if (_controlSchemeName == "2PKeyboard" || _controlSchemeName == "2PGamepad")
        {
            Debug.Log("CREANDO JUGADOR");
            _characterPrefab = GorilaPrefab;
            int charIndex = 2;
            _inputManager.CreateP2(_characterPrefab, charIndex);
        }
    }
}
