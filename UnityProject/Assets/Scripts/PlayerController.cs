using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("The force of the jump action.")]
    private float jumpForce = 5f;

    [SerializeField, Tooltip("The movement speed of the player.")]
    private float moveSpeed = 5f;

    private Rigidbody _rigidbody;
    private PlayerControls _playerControls;
    
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new PlayerControls();
    }

    public void FixedUpdate()
    {
        var readInput = _playerControls.Default.Movement.ReadValue<Vector2>();
        _rigidbody.AddForce(new Vector3(readInput.x, 0, readInput.y) * moveSpeed, ForceMode.Impulse);
    }

    public void OnEnable()
    {
        _playerControls.Default.Enable();
        _playerControls.Default.Jump.started += Jump;
    }

    public void OnDisable()
    {
        _playerControls.Default.Jump.started -= Jump;
        _playerControls.Default.Disable();
    }


    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump." + context.phase);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        var readInput = context.ReadValue<Vector2>();
        var direction = new Vector3(readInput.x, 0, readInput.y).normalized;
        _rigidbody.AddForce(direction * moveSpeed,ForceMode.Impulse);
        
        
        Debug.Log(context);
    }
}
