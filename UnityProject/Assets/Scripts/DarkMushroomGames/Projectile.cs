using System;
using UnityEngine;

namespace DarkMushroomGames
{
    /// <summary>
    /// When attached to a game object it will make it into a projectile.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField, Tooltip("The projectile speed in m/s.")]
        private int moveSpeed = 5;

        public void Update()
        {
            transform.Translate(Time.deltaTime * moveSpeed * Vector3.forward);
        }
    }
}
