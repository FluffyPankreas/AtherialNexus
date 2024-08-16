using DarkMushroomGames.Managers;
using HighlightPlus;
using UnityEngine;

namespace DarkMushroomGames
{
    /// <summary>
    /// Represents a weapon the player can wield. Includes information like fire rate and ammo loaded.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField,Tooltip("The point at which the ammo that is fired will get instantiated. ")]
        private Transform weaponMuzzle;

        [SerializeField,Tooltip("The ammo that is loaded into the gun.")]
        private Ammo loadedAmmo;

        [SerializeField,Tooltip("The sound effect for when the gun's primary fire is used.")]
        private AudioClip primaryFireSoundEffect;
        
        [SerializeField,Tooltip("The sound effect for when the gun's secondary fire is used.")]
        private AudioClip secondaryFireSoundEffect;

        [SerializeField,Tooltip("The sound effect for when the gun's secondary fire cannot used.")]
        private AudioClip secondaryFireMissFire;

        [SerializeField,Tooltip("The time it takes for the secondary fire to come off of cooldown.")]
        private float secondaryFireCooldown = 5f;

        [SerializeField,Tooltip("The range of the secondary fire.")]
        private float secondaryFireRange = 10f;

        [SerializeField,Tooltip("The line render to show the secondary fire.")]
        private LineRendererController secondaryFireEffect;

        [SerializeField,Tooltip("The mask to check the raycast against.")]
        private LayerMask secondaryFireHitMask;

        [SerializeField,Tooltip("The amount of damage the secondary fire deals to enemies.")]
        private int secondaryFireDamage;

        [SerializeField, Tooltip("The amount of damage dropoff for each enemy penetrated.")]
        private int secondaryFireDamageDropoff;
        
        private float _remainingSecondaryFireCooldown;
        
        public void Awake()
        {
            Debug.Assert(loadedAmmo != null,
                "The ammo should always be set to something. A gun without ammo will break the game.", gameObject);

            _remainingSecondaryFireCooldown = 0;
        }

        public void Update()
        {
            _remainingSecondaryFireCooldown -= Time.deltaTime;
            _remainingSecondaryFireCooldown =
                Mathf.Clamp(_remainingSecondaryFireCooldown, 0, _remainingSecondaryFireCooldown);
        }

        /// <summary>
        /// The method to call when the weapon's primary fire ability is used.
        /// </summary>
        public void PrimaryFire()
        {
            var bullet = Instantiate(loadedAmmo, weaponMuzzle.position, Quaternion.identity);
            bullet.transform.forward = weaponMuzzle.forward;

            SoundManager.Instance.PlaySoundEffectClip(primaryFireSoundEffect, transform);
        }

        public void SecondaryFire()
        {
            if (_remainingSecondaryFireCooldown <= 0)
            {
                _remainingSecondaryFireCooldown = secondaryFireCooldown;

                SoundManager.Instance.PlaySoundEffectClip(secondaryFireSoundEffect, transform);
                var startPosition = weaponMuzzle.position;
                var direction = weaponMuzzle.forward;

                Debug.DrawRay(startPosition, direction * secondaryFireRange, Color.red, 10);
            
                var shootingRay = new Ray(startPosition, direction);    
                var hits = Physics.RaycastAll(shootingRay, secondaryFireRange, secondaryFireHitMask);

                var fireEffect = Instantiate(secondaryFireEffect);
                fireEffect.SetPosition(0, shootingRay.GetPoint(0));
                fireEffect.SetPosition(1,shootingRay.GetPoint(secondaryFireRange));

                var remainingDamage = secondaryFireDamage;
                foreach (var hit in hits)
                {
                    var colliderGameObject = hit.collider.gameObject;
                
                    colliderGameObject.GetComponent<HitPoints>().SubtractHitPoints(remainingDamage);
                    var hitEffect = colliderGameObject.GetComponent<HighlightEffect>();
                    if (hitEffect != null)
                    {
                        hitEffect.HitFX();
                    }
                    remainingDamage -= secondaryFireDamageDropoff;
                    remainingDamage = Mathf.Clamp(remainingDamage, 0, secondaryFireDamage);
                }
            }
            else
            {
                SoundManager.Instance.PlaySoundEffectClip(secondaryFireMissFire, transform);
            }
        }
    }
}


