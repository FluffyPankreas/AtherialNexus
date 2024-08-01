using DarkMushroomGames;
using DarkMushroomGames.Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField, Tooltip("The modifier for using the sprint functionality.")]
    private float sprintModifier;

    [SerializeField, Tooltip("The amount of maximum stamina.")]
    private float maxStamina;

    [SerializeField, Tooltip("The rate of stamina recharge rate.")]
    private float staminaChargeRate;

    [SerializeField,Tooltip("The multiplier for stamina recharge when the player is standing still.")]
    private float staminaChargeMultiplier = 2f;

    [SerializeField, Tooltip("The rate at which the stamina is drained while sprinting.")]
    private float staminaDrainRate;

    [SerializeField,Tooltip("The UI element that displays the player's health.")]
    private Slider hitPointsSlider;
    
    [SerializeField, Tooltip("The UI element that displays the player's health.")]
    private TMP_Text hitPointsLabel;

    [SerializeField, Tooltip("The UI element that displays the player's stamina")]
    private Slider staminaSlider;
    
    [SerializeField, Tooltip("The UI element that displays the player's stamina")]
    private TMP_Text staminaLabel;

    [SerializeField,Tooltip("The custom gravity that the player experiences.")]
    private float gravity;

    [SerializeField,Tooltip("The distance at which the player can interact with objects in meters.")]
    private float interactDistance = 1f;
    
    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;

    private float _xRotation = 0f;
    private bool _isJumping = false;
    private bool _isGrounded = true;

    private Weapon _equippedWeapon;
    private HitPoints _hitPoints;
    private float _staminaLeft;
    
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
        _hitPoints = GetComponent<HitPoints>(); 

        _equippedWeapon = GetComponentInChildren<Weapon>(true);
        _staminaLeft = maxStamina;


        if (hitPointsSlider != null)
        {
            hitPointsSlider.maxValue = _hitPoints.MaxHitPoints;
        }

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;    
        }
    }

    public void Start()
    {
        GameManager.OnGamePause += OnGamePause;
        GameManager.OnGameUnPause += OnGameUnPause;
    }

    public void Update()
    {
        
        var interactionRay = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactDistance, Color.green);

        RaycastHit hitInfo;
        if (Physics.Raycast(interactionRay, out hitInfo, interactDistance,LayerMask.GetMask("Interactable")))
        {
            var interactable = hitInfo.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                Debug.Log(interactable.InterActionMessage);
            }
        }
        
        
        if (hitPointsSlider != null && staminaSlider != null)
        {
            hitPointsSlider.value = _hitPoints.HitPointsLeft;
            staminaSlider.value = _staminaLeft;

            hitPointsLabel.text = _hitPoints.HitPointsLeft.ToString();
            staminaLabel.text = _staminaLeft.ToString("0");
        }

    }

    public void FixedUpdate()
    {
        HandleMouseLook();
        HandleMovement();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isJumping && collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            _isGrounded = true;
            _isJumping = false;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            _isGrounded = false;
        }
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
    
    public void OnDestroy()
    {
        GameManager.OnGamePause -= OnGamePause;
        GameManager.OnGameUnPause -= OnGameUnPause;
    }

    public void SetMouseSensitivity(float level)
    {
        mouseSensitivity = level;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleMovement()
    {
        var readInput = _playerControls.Default.Movement.ReadValue<Vector2>();
        var isSprinting = _playerControls.Default.Sprint.ReadValue<float>() != 0;

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

        var runningSpeed = moveSpeed;
        if (readInput.y > 0 && isSprinting && _staminaLeft > 0)
        {
            runningSpeed = moveSpeed * sprintModifier;
            _staminaLeft -= staminaDrainRate * Time.deltaTime;
        }

        if (!isSprinting)
        {
            if (readInput.magnitude != 0)
            {
                _staminaLeft += staminaChargeRate * Time.deltaTime;
            }
            else
            {
                _staminaLeft += staminaChargeRate * staminaChargeMultiplier * Time.deltaTime;
            }
        }

        _staminaLeft = Mathf.Clamp(_staminaLeft, 0, maxStamina);
        if (!_isGrounded)
        {
            _rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }

        /*Vector3 directionForce = new Vector3(direction.x * runningSpeed, 0, direction.z * runningSpeed);

        _rigidbody.maxLinearVelocity = 10f;
        _rigidbody.AddForce(directionForce);

        if (direction.x == 0)
        {
            var velocity = _rigidbody.velocity;
            
            velocity = new Vector3(0, velocity.y, velocity.z);
            _rigidbody.velocity = velocity;
        }
        
        if (direction.z == 0)
        {
            var velocity = _rigidbody.velocity;
            
            velocity = new Vector3(0, velocity.y, velocity.z);
            _rigidbody.velocity = velocity;
        }*/

        _rigidbody.velocity = new Vector3(direction.x * runningSpeed, _rigidbody.velocity.y, direction.z * runningSpeed);
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

    private void OnGamePause()
    {
        _playerControls.Default.Disable();
    }

    private void OnGameUnPause()
    {
        _playerControls.Default.Enable();
    }
}
