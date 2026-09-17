using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerCtrlTest : MonoBehaviour
{
    //PlayerInput _playerInput;
    //CharacterController _controller;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    Vector2 _moveInput;
    
    void Awake()
    {
        //_playerInput = new PlayerInput();
        //_controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();

        //_playerInput.Gameplay.Move.started += on1kMove;
    }


    void FixedUpdate()
    {
        // Movement
        rb.linearVelocity = new Vector2(_moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (_moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_moveInput.x), 1, 1);
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log("Detectado movimiento en el eje X");
    }
}
