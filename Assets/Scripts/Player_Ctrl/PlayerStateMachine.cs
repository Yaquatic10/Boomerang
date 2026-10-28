using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerStateMachine : MonoBehaviour
{
    public HealthManager _healthManager;
    private CtxDataBase _db;
    
    //Objetos para verificación de contacto con el suelo (ground).
    [Header("Ground Check")]
    public Transform groundCheckPoint; //Espacio reservado para vincular un EmptyObject (desde el editor de Unity)
    public float groundCheckRadius = 0.2f; //Radio usado para crear una esfera virtual que fungirá como collider para la detección.
    public LayerMask groundLayer;   //Menú desplegable, modificado desde el Edito de Unity para establecer que layer corresponde al suelo.

    //Wall checker.
    [Header("Wall Check")]
    public Transform _wallCheckPointRgt;
    public Transform _wallCheckPointLft;
    public float _wallCheckRadius = 0.2f;
    public LayerMask _wallLayer;

    private Animator _animCtrl;
    private Rigidbody2D _rbChar; //Referencia al componente RigidBody2D del player (Cargar un componente de este tipo desde el editor de Unity).

    [Header("FRICTION MATERIALS")]
    public PhysicsMaterial2D _materialSinFriccion;
    private PhysicsMaterial2D _materialConFriccion;
    public PhysicsMaterial2D _frictionA;
    public PhysicsMaterial2D _frictionB;
    public PhysicsMaterial2D _frictionC;

    [Header("BOOMERANG SETUP")]
    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;


    //Espacios reservador para control de Animaciones.
    int _isWalkingHash;


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
    bool _isHookedRgt;
    bool _isHookedLft;
    bool _isJumping = false;
    bool _isWallJumping = false;

    //PROVISIONAL: Variables para ajuste de parámetros de movimiento.
    [Header("Movement")]
    public float _moveSpeed = 5f;

    //Control de salto, algunas se reubicarán y conectarán con el archivo de metadátos de cada personaje jugable.
    [Header("Salto")]
    public float _jumpHeight = 3f;
    public float _maxJumpTime = 0.5f;
    public float _gravityScale = 5f;
    public float _fallingGravityScale = 8f;
    public float _jumpForce;
    public float _wallJumpForce = 15f;
    public int _WallJumpSign;
    float _tiempoBaseAlVertice = 0.5f;
    float _tiempoAlVertice;
    float _gravedadNecesaria;
    float _gravedadBase;
    float _jumpTimeCounter;
    float _currentGravityScale;

    //Booleanos para control de modificadores de stats.
    float _speedBuff1 = 1.5f;
    bool _speedBuffEnabled = false;
    float _currentMoveMultiplier = 1;

    public int _charIndex = 1;
    int _isPlayer;
    int _dmgType;

    //Referencias especiales para el funcionamiento de la Máquina de Estados.
    PlayerBaseState _currentState;

    PlayerStateFactory _states;

    GameObject _currentBoomerang;

    //INICIAN SETTERS Y GETTERS---------------------------------------------------------------------------------
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }    //Setter + Getter para el current state.
    public PlayerStateFactory States { get { return _states; } set { _states = value; } }

    public HealthManager HealthManager {  get { return _healthManager; } set { _healthManager = value; } }
    public CtxDataBase DB {  get { return _db; } set { _db = value; } }

    //GETTERS PARA CONTROLES DE ANIMACION.
    public int IsWalkingHash { get { return _isWalkingHash; } set { _isWalkingHash = value; } }

    //SETTERS Y GETTERS DE MOVIMIENTO FINAL.
    public float AppliedMovementX { get { return _appliedMovementX; } set { _appliedMovementX = value; } }
    public float AppliedMovementY { get { return _appliedMovementY; } set { _appliedMovementY = value; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float JumpHeight { get { return _jumpHeight; } }
    public float JumpForce { get { return _jumpForce; } set { _jumpForce = value; } }
    public float GravityScale { get { return _gravityScale; } }
    public float FallingGravityScale { get { return _fallingGravityScale; } }
    public float CurrentGravityScale { get { return _currentGravityScale; } set { _currentGravityScale = value; } }
    public float MaxJumpTime { get { return _maxJumpTime; } set { _maxJumpTime = value; } }
    public float JumpTimeCounter { get { return _jumpTimeCounter; } set { _jumpTimeCounter = value; } }
    public float TiempoBaseAlVertice { get { return _tiempoBaseAlVertice; } }
    public float TiempoAlVertice { get { return _tiempoAlVertice; } set { _tiempoAlVertice = value; } }
    public float GravedadNecesaria { get { return _gravedadNecesaria; } set { _gravedadNecesaria = value; } }
    public float GravedadBase { get { return _gravedadBase; } }
    public float WallJumpForce { get { return _wallJumpForce; } }
    //GETTERS DE REGISTRO DE INPUTS.
    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public bool IsWallJumping { get { return _isWallJumping; } set { _isWallJumping = value; } }
    public bool IsBoomerangPressed { get { return _isBoomerangPressed; } set { _isBoomerangPressed = true; } }
    public bool IsItemPressed { get { return _isItemPressed; } }
    public Vector2 MovementInput { get { return _movementInput; } set { _movementInput = value; } }



    // GETTERS Y SETTERS DE COMPROBACIÓN DE ESTADOS.
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsHookedRgt { get { return _isHookedRgt; } set { _isHookedRgt = value; } }
    public bool IsHookedLft { get { return _isHookedLft; } set { _isHookedLft = value; } }
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public bool SpeedBuffEnabled { get { return _speedBuffEnabled; } set { _speedBuffEnabled = value; } }
    public Transform GroundCheckPoint { get { return groundCheckPoint; } }
    public float GroundCheckRadius { get { return groundCheckRadius; } }
    public LayerMask GroundLayer { get { return groundLayer; } }

    public Transform WallCheckPointRgt { get { return _wallCheckPointRgt; } }
    public Transform WallCheckPointLft { get { return _wallCheckPointLft; } }
    public float WallCheckRadius { get { return _wallCheckRadius; } }
    public LayerMask WallLayer { get { return _wallLayer; } }

    // OTROS SETTERS Y GETTERS
    public float SpeedBuff1 { get { return _speedBuff1; } }
    public float CurrentMoveMultiplier { get { return _currentMoveMultiplier; } set { _currentMoveMultiplier = value; } }
    public Animator AnimCtrl { get { return _animCtrl; } set { _animCtrl = value; } }
    public Rigidbody2D RbChar { get { return _rbChar; } set { _rbChar = value; } }
    public PhysicsMaterial2D FrictionMat { get { return _materialConFriccion; } set { _materialConFriccion = value; } }
    public PhysicsMaterial2D FrictionlessMat { get { return _materialSinFriccion; } }
    public PhysicsMaterial2D FrictionA { get { return _frictionA; } }
    public PhysicsMaterial2D FrictionB { get { return _frictionB; } }
    public PhysicsMaterial2D FrictionC { get { return _frictionC; } }
    public int WallJumpOrient { get { return _WallJumpSign; } set { _WallJumpSign = value; } }

    public int IsPlayer {  get { return _isPlayer; } set { _isPlayer = value; } }
    public int DmgType {  get { return _dmgType; } set { _dmgType = value; } }
    public GameObject currentBoomerang {  get { return _currentBoomerang;} set { _currentBoomerang = value; } }

    //FINALIZAN SETTERS Y GETTERS-------------------------------------------------------------------------------

    void Awake()
    {
        RbChar = GetComponent<Rigidbody2D>(); //Se reconoce el RigidBody 2D del personaje.
        AnimCtrl = GetComponent<Animator>();
        HealthManager = FindAnyObjectByType<HealthManager>();
        DB = FindAnyObjectByType<CtxDataBase>();
        RbChar.sharedMaterial = FrictionlessMat;
        States = new PlayerStateFactory(this); //Se invoca la Fabrica de Estados.
        CurrentState = States.Airborne();
        CurrentState.EnterState(); //Se inicializa el estado de Airborne.
        SpeedBuffEnabled = false;
        CurrentGravityScale = GravityScale;
        
        //Configuración de Animaciones.
        //IsWalkingHash = _animCtrl.StringToHash("isWalking");
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

    public void onBoomerangInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsBoomerangPressed = true;
            if (currentBoomerang == null)
            {
                currentBoomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, boomerangSpawnPoint.rotation);
                currentBoomerang.GetComponent<Boomerang>().ThrowingPlayer = boomerangSpawnPoint.gameObject;
                currentBoomerang.GetComponent<Boomerang>().ownerRecognition(IsPlayer);
            }
        }
        else if (context.canceled)
        {
            IsBoomerangPressed = false;
        }
    }

    private void FixedUpdate()  //Método UPDATE global de la máquina de estados.
    {
        RbChar.linearVelocity = new Vector2(AppliedMovementX,RbChar.linearVelocity.y);   //Aplicación de movimiento.
        CurrentState.UpdateStates();

       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DmgTester"))
        {
            DmgType = 1;
            HealthManager.ApplyDamage(IsPlayer, DmgType);
        }

        if (other.CompareTag("Boomerang") && IsPlayer == 2)
        {
            DmgType = 0;
            HealthManager.ApplyDamage(IsPlayer, DmgType);
        }

        if (other.CompareTag("Boomerang 2") && IsPlayer == 1)
        {
            DmgType = 0;
            HealthManager.ApplyDamage(IsPlayer, DmgType);
        }
    }

    public void SelfRecognitionAsP1()
    {
        IsPlayer = 1;
        Debug.Log("ERES PLAYER 1");
        switch (DB.P1CharIndex)
        {
            case (0):
                {
                    FrictionMat = FrictionA;
                    break;
                }
            case (1):
                {
                    FrictionMat = FrictionB;
                    break;
                }
            case (2):
                {
                    FrictionMat = FrictionC;
                    break;
                }
        }
    }

    public void SelfRecognitionAsP2()
    {
        IsPlayer = 2;
        Debug.Log("ERES PLAYER 2");
        switch (DB.P2CharIndex)
        {
            case (0):
                {
                    FrictionMat = FrictionA;
                    break;
                }
            case (1):
                {
                    FrictionMat = FrictionB;
                    break;
                }
            case (2):
                {
                    FrictionMat = FrictionC;
                    break;
                }
        }
    }
}
