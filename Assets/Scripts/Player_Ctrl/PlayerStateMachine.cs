using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerStateMachine : MonoBehaviour
{
    //Objetos para verificación de contacto con el suelo (ground).
    [Header("Ground Check")]
    public Transform groundCheckPoint; //Espacio reservado para vincular un EmptyObject (desde el editor de Unity)
    public float groundCheckRadius = 0.2f; //Radio usado para crear una esfera virtual que fungirá como collider para la detección.
    public LayerMask groundLayer;   //Menú desplegable, modificado desde el Edito de Unity para establecer que layer corresponde al suelo.

    private Rigidbody2D _rbChar; //Referencia al componente RigidBody2D del player (Cargar un componente de este tipo desde el editor de Unity).

    //Variables de control de movimientos.
    Vector2 _movementInput; //Variable reservada para almacenar la señal ingresada por el jugador desde su control/teclado.
    float _appliedMovementX;    //Variables separadas por eje para procesar los movimientos finales que se aplican al RigidBody del player.
    float _appliedMovementY;

    //Booleanos para control de estado del personaje.
    bool _isMovementPressed;
    bool _isJumpPressed;
    bool _isItemPressed;
    bool _isBoomerangPressed;
    bool _isGrounded;
    bool _isJumping = false;

    //PROVISIONAL: Variables para ajuste de parámetros de movimiento.
    [Header("Movement")]
    public float _moveSpeed = 5f;

    //Control de salto, algunas se reubicarán y conectarán con el archivo de metadátos de cada personaje jugable.
    [Header("Salto")]
    public float _jumpHeight = 3f;
    public float _maxJumpTime = 0.5f;
    public float _gravityScale = 5f;
    public float _fallingGravityScale = 8f;
    public float _jumpForce = 15f;

    float _jumpTimeCounter;
    float _currentGravityScale;

    //Booleanos para control de modificadores de stats.
    float _speedBuff1 = 1.5f;
    bool _speedBuffEnabled = false;
    float _currentMoveMultiplier = 1;

    //Referencias especiales para el funcionamiento de la Máquina de Estados.
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    

    //INICIAN SETTERS Y GETTERS---------------------------------------------------------------------------------
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }    //Setter + Getter para el current state.
    public PlayerStateFactory States {  get { return _states; } set { _states = value; } }

            //SETTERS Y GETTERS DE MOVIMIENTO FINAL.
    public float AppliedMovementX {  get { return _appliedMovementX; } set { _appliedMovementX = value;  } }
    public float AppliedMovementY {  get { return _appliedMovementY; } set { _appliedMovementY = value; } }
    public float MoveSpeed {  get { return _moveSpeed; } }
    public float JumpHeight {  get { return _jumpHeight; } }
    public float JumpForce { get { return _jumpForce; } }
    public float GravityScale { get { return _gravityScale; } }
    public float FallingGravityScale {  get { return _fallingGravityScale; } }
    public float CurrentGravityScale {  get { return _currentGravityScale; } set { _currentGravityScale = value; } }
    public float MaxJumpTime {  get { return _maxJumpTime; } set { _maxJumpTime = value; } }
    public float JumpTimeCounter {  get { return _jumpTimeCounter; } set { _jumpTimeCounter = value; } }

    //GETTERS DE REGISTRO DE INPUTS.
    public bool IsMovementPressed {  get { return _isMovementPressed; } set { _isMovementPressed = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public bool IsBoomerangPressed {  get { return _isBoomerangPressed; } }
    public bool IsItemPressed {  get { return _isItemPressed; }  }
    public Vector2 MovementInput { get { return _movementInput; } set { _movementInput = value; } }
    


    // GETTERS Y SETTERS DE COMPROBACIÓN DE ESTADOS.
    public bool IsGrounded {  get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public bool SpeedBuffEnabled { get { return _speedBuffEnabled; } set { _speedBuffEnabled = value; } }
    public Transform GroundCheckPoint {  get { return groundCheckPoint; } }
    public float GroundCheckRadius {  get { return groundCheckRadius; } }
    public LayerMask GroundLayer {  get {  return groundLayer; }  }

    // OTROS SETTERS Y GETTERS
    public float SpeedBuff1 { get { return _speedBuff1; } }
    public float CurrentMoveMultiplier { get { return _currentMoveMultiplier; } set { _currentMoveMultiplier = value; } }
    public Rigidbody2D RbChar { get { return _rbChar; } set { _rbChar = value; } }
    

    //FINALIZAN SETTERS Y GETTERS-------------------------------------------------------------------------------

    void Awake()
    {
        RbChar = GetComponent<Rigidbody2D>(); //Se reconoce el RigidBody 2D del personaje.

        States = new PlayerStateFactory(this); //Se invoca la Fabrica de Estados.
        CurrentState = States.Airborne();
        CurrentState.EnterState(); //Se inicializa el estado de Airborne.
        SpeedBuffEnabled = false;
        CurrentGravityScale = GravityScale;
}

    public void onMovementInput(InputAction.CallbackContext context) //Callback para el input de movimiento.
    {
        MovementInput = context.ReadValue<Vector2>();
        IsMovementPressed = MovementInput.x != 0;
    }

    public void onJumpInput(InputAction.CallbackContext context)    //Callback para el input de Salto.
    {
        if (context.started)
        {
            IsJumpPressed = true;
        }
        else if (context.canceled)
        {
            IsJumpPressed = false;
        }
    }

    private void FixedUpdate()  //Método UPDATE global de la máquina de estados.
    {
        RbChar.linearVelocity = new Vector2(AppliedMovementX,RbChar.linearVelocity.y);   //Aplicación de movimiento.
        CurrentState.UpdateStates();
    }
}
