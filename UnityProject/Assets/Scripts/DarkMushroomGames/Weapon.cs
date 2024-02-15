using System;
using UnityEditor;
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
        
        public void Awake()
        {
            Debug.Assert(loadedAmmo != null,
                "The ammo should always be set to something. A gun without ammo will break the game.", gameObject);
        }

        /// <summary>
        /// The method to call when the weapon's primary fire ability is used.
        /// </summary>
        public void PrimaryFire()
        {
            Debug.Log("==== Primary Fire. ====");
            var bullet = Instantiate(loadedAmmo, weaponMuzzle.position, Quaternion.identity);
            bullet.transform.forward = weaponMuzzle.forward;
        }
        
    }
}

