using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private Movement playerMovement;
    private PlayerControls _playerControls;

    private Vector2 _horizontalInput;
    
    public void Awake()
    {
        _playerControls = new PlayerControls();
        
    }

    public void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Default.HorizontalMovement.performed += ReadHorizontalInput;
    }

    public void Update()
    {
        playerMovement.ReceiveInput(_horizontalInput);
    }

    public void OnDisable()
    {
        _playerControls.Default.HorizontalMovement.performed -= ReadHorizontalInput;
    }

    private void ReadHorizontalInput(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>();
    }
        
}
