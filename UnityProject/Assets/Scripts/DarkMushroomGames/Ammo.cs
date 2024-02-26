using System;
using DarkMushroomGames.Managers;
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
        
        [SerializeField,Tooltip("The clip to play when the ammo hits something.")]
        private AudioClip hitSound;
        
        public void OnCollisionEnter(Collision collision)
        {
            var hp = collision.gameObject.GetComponent<HitPoints>();
            if(hp!=null)
                hp.SubtractHitPoints(ammoDamage);

            // TODO: Find a better way to do this.
            var player = GameObject.FindWithTag("Player");

            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                SoundEffectsManager.Instance.PlaySoundEffectsClip(hitSound, player.transform);    
            }
            
            
            Destroy(gameObject);
        }
    }
}
