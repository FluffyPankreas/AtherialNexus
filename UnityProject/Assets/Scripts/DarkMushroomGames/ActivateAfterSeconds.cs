using System;
using UnityEngine;

namespace DarkMushroomGames
{
    /// <summary>
    /// A simple script that activates the game object once a pre-determined amount of seconds have passed.
    /// </summary>
    public class ActivateTargetAfterSeconds : MonoBehaviour
    {
        [SerializeField,Tooltip("The amount of time to wait before activating the game object in seconds. ")]
        private float waitTime;

        [SerializeField,Tooltip("The target to activate.")]
        private GameObject target;

        public void Awake()
        {
            Debug.Assert(target != null,
                "The target has to be set or else null reference exceptions are going to happen.", gameObject);
        }

        public void Update()
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                target.SetActive(true);
                enabled = false;
            }
        }
    }
}
