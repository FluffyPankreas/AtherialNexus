using System;
using UnityEngine;

namespace DarkMushroomGames
{
        public class PlayerOutOfBounds : MonoBehaviour
        {
                public void OnCollisionEnter(Collision collision)
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        collision.transform.position = Vector3.zero;
                    }
                }
        }
}
