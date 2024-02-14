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

        /// <summary>
        /// The method to call when the weapon's primary fire ability is used.
        /// </summary>
        public void PrimaryFire()
        {
            Debug.Log("Primary Fire.");
        }
        
    }
}
