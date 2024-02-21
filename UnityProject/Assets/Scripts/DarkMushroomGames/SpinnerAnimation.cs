using UnityEngine;

namespace DarkMushroomGames
{
    public class SpinnerAnimation : MonoBehaviour
    {
        [SerializeField]
        private float xSpeed = 0f;
        [SerializeField]
        private float ySpeed= 0f;
        [SerializeField]
        private float zSpeed= 0f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
        }
    }
}
