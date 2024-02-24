using Unity.Mathematics;
using UnityEngine;

namespace DarkMushroomGames
{
    public class ProjectileEnemyAttack : MonoBehaviour
    {
        [SerializeField, Tooltip("The cooldown for the weapon fire rate.")]
        private float attackCooldown = 5f;

        [SerializeField,Tooltip("The attack range of the enemy.")]
        private float attackRange;

        [SerializeField, Tooltip("The point at which the projectile will be spawned.")]
        private Transform attackOrigin;

        [SerializeField,Tooltip("The projectile that this enemy will fire.")]
        private Transform projectilePrefab;

        private bool _isAttacking;
        private Transform _target;
        private Transform _firingPosition;

        public void Update()
        {
            FindTarget();

            if (_target != null)
            {
                var distanceToTarget = Vector3.Distance(transform.position, _target.position);
                if (!_isAttacking && (distanceToTarget <= attackRange))
                {
                    Attack();
                }
            }
        }

        private void FindTarget()
        {
            // look for target in range.
            if (_target == null)
            {
                _target = GameObject.FindWithTag("Player").transform;
            }
        }

        private void Attack()
        {
            _isAttacking = true;

            Invoke(nameof(ResetAttack), attackCooldown);
            var direction = _target.position - attackOrigin.position;
            var projectile = Instantiate(projectilePrefab.gameObject, attackOrigin.position, quaternion.identity);
            projectile.transform.forward = direction;
        }

        private void ResetAttack()
        {
            _isAttacking = false;
        }
    }
}
