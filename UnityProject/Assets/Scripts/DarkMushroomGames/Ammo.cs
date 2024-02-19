using System;
using UnityEngine;


namespace DarkMushroomGames
{
    /// <summary>
    /// Base class for ammo.
    /// </summary>
    public class Ammo : MonoBehaviour
    {
        public void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
