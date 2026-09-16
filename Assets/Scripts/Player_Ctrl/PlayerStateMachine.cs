using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D _rbChar;

    //Variables de control de movimientos.
    Vector2 _movementInput;
    float _horizontalInput;
    float _appliedMovementX;
    float _appliedMovementY;

    //Booleanos para control de estado del personaje.
    bool _isMovementPressed;
    bool _isJumpPressed;
    bool _isItemPressed;
    bool _isBoomerangPressed;

    //PROVISIONAL: Variables para ajuste de parámetros de movimiento.
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    //Variables de estado
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //SETTERS Y GETTERS
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }    //Setter + Getter para el current state.
    public float AppliedMovementX {  get { return _appliedMovementX; } set { _appliedMovementX = value;  } }
    public float AppliedMovementY { get { return _appliedMovementY; } set { _appliedMovementY = value; } }


    void Awake()
    {
        _rbChar = GetComponent<Rigidbody2D>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    public void onMovementInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _movementInput.x != 0;
    }

    public void movePlayer()
    {
        _rbChar.linearVelocity = new Vector2(_movementInput.x * moveSpeed, _rbChar.linearVelocity.y);

        if (_movementInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_movementInput.x), 1, 1);
        }
    }

    void FixedUpdate()
    {
        movePlayer();
    }
}
