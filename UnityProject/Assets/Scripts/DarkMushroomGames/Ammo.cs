using System;
using UnityEngine;


namespace DarkMushroomGames
{
    /// <summary>
    /// Base class for ammo.
    /// </summary>
    public class Ammo : MonoBehaviour
    {
        [SerializeField,Tooltip("The amount of damage the bullet does.")]
        private int ammoDamage = 1;
        
        public void OnCollisionEnter(Collision collision)
        {
            var hp = collision.gameObject.GetComponent<HitPoints>();
            if(hp!=null)
                hp.SubtractHitPoints(ammoDamage);
            Destroy(gameObject);
        }
    }
}
