using DarkMushroomGames;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The camera that the player will be using.")]
    private Camera fpsCamera;
    
    [SerializeField, Tooltip("The force of the jump action.")]
    private float jumpForce = 5f;

    [SerializeField, Tooltip("The movement speed of the player.")]
    private float moveSpeed = 5f;

    [SerializeField, Tooltip("The mouse sensitivity.")]
    private float mouseSensitivity = 10f;
    

    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;

    private float _xRotation = 0f;
    private bool _isJumping;

    private Weapon _equippedWeapon;
    
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _equippedWeapon = GetComponentInChildren<Weapon>(true);
    }

    public void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }

        if (_isJumping && _rigidbody.velocity.y == 0)
        {
             _isJumping = false;
        }
    }

    public void FixedUpdate()
    {
        HandleMouseLook();
        HandleMovement();
    }

    public void OnEnable()
    {
        _playerControls.Default.Enable();
        _playerControls.Default.Jump.started += Jump;
        _playerControls.Default.PrimaryFire.performed += PrimaryFire;
        _playerControls.Default.SecondaryFire.performed += SecondaryFire;
    }

    public void OnDisable()
    {
        _playerControls.Default.Jump.started -= Jump;
        _playerControls.Default.PrimaryFire.performed -= PrimaryFire;
        _playerControls.Default.SecondaryFire.performed -= SecondaryFire;
        _playerControls.Default.Disable();
    }


    private void Jump(InputAction.CallbackContext context)
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleMovement()
    {
        var readInput = _playerControls.Default.Movement.ReadValue<Vector2>();
        
        var direction = Vector3.zero;
        if (readInput.y > 0)
            direction += transform.forward;
        if (readInput.y < 0)
            direction -= transform.forward;

        if (readInput.x > 0)
            direction += transform.right;
        if (readInput.x < 0)
            direction -= transform.right;
            
        direction = direction.normalized;

        _rigidbody.velocity = new Vector3(direction.x * moveSpeed, _rigidbody.velocity.y, direction.z * moveSpeed);
    }

    private void HandleMouseLook()
    {
        var readMouseDelta = _playerControls.Default.MouseLook.ReadValue<Vector2>();

        var mouseX = readMouseDelta.x * mouseSensitivity * Time.fixedDeltaTime;
        var mouseY = readMouseDelta.y * mouseSensitivity * Time.fixedDeltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        fpsCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void PrimaryFire(InputAction.CallbackContext context)
    {
        _equippedWeapon.PrimaryFire();
    }

    private void SecondaryFire(InputAction.CallbackContext context)
    {
        _equippedWeapon.SecondaryFire();
    }
}
