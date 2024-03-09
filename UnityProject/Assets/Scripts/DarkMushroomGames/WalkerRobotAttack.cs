using System;
using DarkMushroomGames;
using DarkMushroomGames.Managers;
using UnityEngine;


namespace DarkMushroomGames
{
    [RequireComponent(typeof(Enemy))]
    public class WalkerRobotAttack : MonoBehaviour
    {
        [SerializeField,Tooltip("The muzzle for the enemy's range attack.")]
        private Transform muzzle;

        [SerializeField, Tooltip("The ammo to instantiate at the muzzle when ranged attack is used.")]
        private Ammo ammo;

        [SerializeField,Tooltip("The cooldown on the ranged attack of the enemy.")]
        private float attackCooldown = 3f;

        [SerializeField,Tooltip("The clip that will play when the weapon is fired.")]
        private AudioClip fireSoundEffect;

        private float _attackCooldownLeft = 0f;
        private Enemy _enemy;

        public void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void Update()
        {
            if (_enemy.IsChasing)
            {
                if (Vector3.Distance(_enemy.Target.position, transform.position) <= _enemy.AttackRange)
                {
                    if (_attackCooldownLeft <= 0)
                    {
                        Attack();
                    }
                }
            }

            _attackCooldownLeft -= Time.deltaTime;
            _attackCooldownLeft = Mathf.Clamp(_attackCooldownLeft, 0, attackCooldown);
        }

        private void Attack()
        {
            SoundManager.Instance.PlaySoundEffectClip(fireSoundEffect, transform);
            var bullet = Instantiate(ammo, muzzle.position, Quaternion.identity);
            bullet.transform.forward = muzzle.forward;

            _attackCooldownLeft = attackCooldown;
        }
    }
}
