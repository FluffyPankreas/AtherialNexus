using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AetherialNexus
{
    [CreateAssetMenu(fileName = "ResourceDropRange", menuName = "Resource Drops", order = 1)]
    public class ResourceDropRange : ScriptableObject
    {
        [SerializeField, Tooltip("The min.")] private int minimum;

        [SerializeField, Tooltip("tHJE MAX")] private int maximum;


        public int amount { get; private set; }
        public int tier { get; private set; }
        public string type { get; private set; }



        public void Awake()
        {
            Debug.Log("Awake called.");
            amount = Random.Range(minimum, maximum);
            tier = Random.Range(0, 3);
            type = "MechanicalParts";
        }

    }
}
