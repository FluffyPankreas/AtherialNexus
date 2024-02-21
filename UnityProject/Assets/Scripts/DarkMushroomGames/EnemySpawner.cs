using System;
using UnityEngine;

namespace DarkMushroomGames
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField,Tooltip("The enemy prefab that will be spawned.")]
        private Enemy enemyPrefab;

        [SerializeField, Tooltip("The amount of time in seconds that has to elapse before the next enemy will spawn.")]
        private float spawnCooldown =5f;

        private float _currentCooldown;

        public void Start()
        {
            _currentCooldown = spawnCooldown;
        }
        public void Update()
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0)
            {
                CreateNewEnemy();
                _currentCooldown = spawnCooldown;
            }
        }

        private void CreateNewEnemy()
        {
            
        }
    }
}
