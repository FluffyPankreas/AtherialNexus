using System;
using DarkMushroomGames.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DarkMushroomGames
{
    /// <summary>
    /// Base class for ammo.
    /// </summary>
    public class Ammo : MonoBehaviour
    {
        [SerializeField,Tooltip("The amount of dice rolled for damage.")]
        private int damageDiceCount = 1;

        [SerializeField,Tooltip("The size of each dice to roll for damage.")]
        private int damageDiceSize = 6;
        
        [SerializeField,Tooltip("The clip to play when the ammo hits something.")]
        private AudioClip hitSound;

        [SerializeField,Tooltip("The layer mask that this ammo will try to do damage to.")]
        private LayerMask damageMask;
        
        public void OnCollisionEnter(Collision collision)
        {
            // TODO: Find a better way to do this.
            var player = GameObject.FindWithTag("Player");

            if ((damageMask & (1 << collision.collider.gameObject.layer)) != 0)
            {
                if (hitSound != null)
                {
                    SoundManager.Instance.PlaySoundEffectClip(hitSound, player.transform);
                }
                else
                {
                    Debug.LogWarning("No sound effect setup for the ammo.", gameObject);
                }

                int ammoDamage = 0;
                for (int i = 0; i < damageDiceCount; i++)
                {
                    ammoDamage += Random.Range(0, damageDiceSize) + 1;
                }
                
                var hp = collision.gameObject.GetComponent<HitPoints>();
                if(hp!=null)
                    hp.SubtractHitPoints(ammoDamage);
            }

            Destroy(gameObject);
        }
    }
}
