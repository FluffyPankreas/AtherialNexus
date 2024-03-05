using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames
{
    public class LookAt : MonoBehaviour
    {
        [FormerlySerializedAs("_target")] [SerializeField, Tooltip("The target that the object will face.")]
        private Transform target;
        
        public void Awake()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        public void Update()
        {
            transform.LookAt(target.position);
        }
    }
}
