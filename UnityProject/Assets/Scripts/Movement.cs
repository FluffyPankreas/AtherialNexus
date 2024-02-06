using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 _horizontalInput;
    public void ReceiveInput(Vector2 horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
}
