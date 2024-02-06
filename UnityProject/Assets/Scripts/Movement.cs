using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField,Tooltip("The movement speed of the player in m/s")] private float moveSpeed = 11f;
    private CharacterController _characterController;
    
    private Vector2 _horizontalInput;

    public void Awake()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    public void Update()
    {
        var direction = new Vector3(_horizontalInput.x, 0, _horizontalInput.y).normalized;
        var horizontalVelocity = Time.deltaTime * moveSpeed * direction;
        _characterController.Move(horizontalVelocity);
    }
    
    
    public void ReceiveInput(Vector2 horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
    
    
}
