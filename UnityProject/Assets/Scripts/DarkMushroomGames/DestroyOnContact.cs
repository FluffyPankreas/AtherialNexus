using UnityEngine;

namespace DarkMushroomGames
{
    public class DestroyOnContact : MonoBehaviour
    {
        [SerializeField,Tooltip("The layers that will trigger the contact.")]
        private LayerMask contactLayers;
        
        public void OnCollisionEnter(Collision collision)
        {
            if (contactLayers.Includes(collision.gameObject.layer))
            {
                Destroy(gameObject);
            }
        }
    }
}
