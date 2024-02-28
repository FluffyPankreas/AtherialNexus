using System;
using UnityEngine;

namespace DarkMushroomGames
{
        public class PlayerOutOfBounds : MonoBehaviour
        {
            private Vector3 _startingPosition;
            public void Start()
            {
                _startingPosition = GameObject.FindWithTag("Player").transform.position;

            }
                public void OnCollisionEnter(Collision collision)
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        collision.transform.position = _startingPosition;
                    }
                }
        }
}
