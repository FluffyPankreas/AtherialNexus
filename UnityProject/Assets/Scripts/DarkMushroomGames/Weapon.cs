using DarkMushroomGames.Managers;
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

        [SerializeField,Tooltip("The time it takes for the secondary fire to come off of cooldown.")]
        private float secondaryFireCooldown = 5f;

        [SerializeField,Tooltip("The range of the secondary fire.")]
        private float secondaryFireRange = 10f;

        [SerializeField,Tooltip("The line render to show the secondary fire.")]
        private LineRenderer secondaryFireEffect;

        public LayerMask testMask;

        public Camera cameraTest;
        private float _remainingSecondaryFireCooldown;
        
        public void Awake()
        {
            Debug.Assert(loadedAmmo != null,
                "The ammo should always be set to something. A gun without ammo will break the game.", gameObject);

            _remainingSecondaryFireCooldown = 0;
            secondaryFireEffect.positionCount = 2;
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

                var startPosition = weaponMuzzle.position;
                var direction = weaponMuzzle.forward;
            
                var shootingRay = new Ray(startPosition, direction);    
                var hits = Physics.RaycastAll(shootingRay, secondaryFireRange, testMask);

                secondaryFireEffect.SetPosition(0, weaponMuzzle.InverseTransformPoint(shootingRay.GetPoint(0)));
                secondaryFireEffect.SetPosition(1,
                    weaponMuzzle.InverseTransformPoint(shootingRay.GetPoint(secondaryFireRange)));

                foreach (var hit in hits)
                {
                    var colliderGameObject = hit.collider.gameObject;
                    Debug.Log(colliderGameObject.name, colliderGameObject);
                
                    colliderGameObject.GetComponent<HitPoints>().SubtractHitPoints(1);
                }
            }
            else
            {
                Debug.Log("The gun can't be fired. Still on cooldown.");
            }
        }
    }
}


