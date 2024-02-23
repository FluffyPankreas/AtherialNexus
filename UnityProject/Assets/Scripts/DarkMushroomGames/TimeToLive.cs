using UnityEngine;

namespace DarkMushroomGames
{
    public class TimeToLive : MonoBehaviour
    {
        [SerializeField, Tooltip("Time in seconds that the object will stay alive.")]
        private float timeToLive = 10f;

        public void Update()
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
