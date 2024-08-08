using System;
using UnityEngine;

namespace DarkMushroomGames
{
    public class ContactAttack : MonoBehaviour
    {
        [SerializeField,Tooltip("The amount of damage each contact does.")]
        private int damage;

        [SerializeField, Tooltip("The amount of time that has to lapse before the attack can happen again.")]
        private float attackCooldown = 2f;

        [SerializeField, Tooltip("The magnitude of the impact force the attack transfers.")]
        private float impactMagnitude;

        private float _remainingCooldown = 0f;
        public void Awake()
        {
            bool setupCorrect = false;
            var collider = GetComponent<Collider>();
            if (collider != null)
            {
                if (collider.isTrigger)
                    setupCorrect = true;
            }
            
            Debug.Assert(setupCorrect,
                "The attack requires some kind of collider set to trigger in order to function properly.");
        }

        public void Update()
        {
            _remainingCooldown -= Time.deltaTime;
            _remainingCooldown = Mathf.Clamp(_remainingCooldown, 0, float.MaxValue);
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _remainingCooldown <= 0)
            {
                _remainingCooldown = attackCooldown;

                var hp = other.transform.root.GetComponent<HitPoints>();
                hp.SubtractHitPoints(damage);
            }
        }
    }
}