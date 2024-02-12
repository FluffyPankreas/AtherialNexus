using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField,Tooltip("The movement speed of the player in m/s")] 
    private float moveSpeed = 11f;

    [SerializeField, Tooltip("The height in meters that the character can jump to.")]
    private float jumpHeight = 5f;
    
    [SerializeField,Tooltip("The gravity acting on the character controller.")] 
    private float gravity = -30f;

    [SerializeField, Tooltip("The layer mask for when the character is grounded.")]
    private LayerMask groundMask;

    private CharacterController _characterController;
    private Vector3 _horizontalDirection = Vector3.zero;
    private bool _isGrounded = true;
    private bool _jump = false;

    public void Awake()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    public void Update()
    {
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        
        var horizontalVelocity = Time.deltaTime * moveSpeed * _horizontalDirection;
        _characterController.Move(horizontalVelocity);

        if (!_isGrounded)
        {
            _characterController.Move(Time.deltaTime * gravity * Vector3.up);
        }

        if (_jump && _isGrounded)
        {
            
            _jump = false;
        }
    }
    
    
    public void ReceiveInput(Vector2 horizontalInput)
    {
        _horizontalDirection = new Vector3(horizontalInput.x, 0, horizontalInput.y).normalized;
    }

    public void OnJumpPressed()
    {
        _jump = true;
    }
    
    
}
