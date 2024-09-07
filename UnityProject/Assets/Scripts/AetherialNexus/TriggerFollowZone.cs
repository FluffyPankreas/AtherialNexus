using UnityEngine;
using UnityEngine.Events;

namespace AetherialNexus
{
    public class TriggerFollowZone : MonoBehaviour
    {
        public UnityEvent<GameObject> onTriggerCallback;
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            
            onTriggerCallback.Invoke(other.gameObject);
        }
    }
}
