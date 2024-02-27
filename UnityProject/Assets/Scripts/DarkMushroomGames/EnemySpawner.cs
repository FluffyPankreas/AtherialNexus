using UnityEngine;

namespace DarkMushroomGames
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Tooltip("The enemy prefab that will be spawned.")]
        private Enemy enemyPrefab;

        [SerializeField, Tooltip("The amount of time in seconds that has to elapse before the next enemy will spawn.")]
        private float spawnCooldown = 5f;

        [SerializeField, Tooltip("The point at which the enemy will be spawned.")]
        private Transform spawnPoint;

        [SerializeField,Tooltip("The maximum amount of spawns that this spawner will create at any one time.")]
        private int maxSpawns = 15;

        [SerializeField,Tooltip("The amount of enemies to spawn at the start.")]
        private int spawnsAtStart = 10;
        
        [SerializeField,Tooltip("The transform that will be the spawned unit's parent.")]
        private Transform spawnedEnemies;
        
        private float _currentCooldown;
        
        
        public void Start()
        {
            _currentCooldown = spawnCooldown;
            for (int i = 0; i < spawnsAtStart; i++)
            {
                CreateNewEnemy();
            }
        }

        public void Update()
        {
            if (spawnedEnemies.childCount < maxSpawns)
            {
                _currentCooldown -= Time.deltaTime;
                if (_currentCooldown <= 0)
                {
                    CreateNewEnemy();
                    _currentCooldown = spawnCooldown;
                }
            }
        }

        private void CreateNewEnemy()
        {
            var newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, spawnedEnemies);
            newEnemy.SetAnchor(transform);
        }
    }
}
