using UnityEngine;

namespace AetherialNexus
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField, Tooltip("The number ")]
        private TMPro.TextMeshPro floatingNumber;

        [SerializeField, Tooltip("Floating upwards speed of floating text.")]
        private float floatingSpeed = 5f;
        
        private Transform _player;

        public void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            transform.LookAt(_player);
            transform.Rotate(Vector3.up, 180);

            transform.Translate(0, floatingSpeed * Time.deltaTime, 0);
        }
        
        public void SetNumber(int newNumber)
        {
            floatingNumber.text = newNumber.ToString();
        }
    }
}
