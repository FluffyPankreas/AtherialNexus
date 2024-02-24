using System;
using UnityEngine;

namespace DarkMushroomGames
{
    [RequireComponent(typeof(Collider))]
    public class DamageOnContact : MonoBehaviour
    {
        [SerializeField,Tooltip("The amount of damage to do on contact.")]
        private int damage;
        
        [SerializeField,Tooltip("The layers that will trigger the contact.")]
        private LayerMask contactLayers;

        public void OnCollisionEnter(Collision collision)
        {
            if (contactLayers.Includes(collision.gameObject.layer))
            {
                var hp = collision.gameObject.GetComponent<HitPoints>();
                hp.SubtractHitPoints(damage);
            }
        }
    }
}
