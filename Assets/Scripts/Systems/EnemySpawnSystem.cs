using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    public class EnemySpawnSystem : MonoBehaviour
    {
        private ObjectPooler _objectPooler;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float spawnInterval;
        private float _spawnIntervalTimer;
        
        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
            _spawnIntervalTimer = Time.time + spawnInterval;
        }

        private void FixedUpdate()
        {
            if (!(Time.time > _spawnIntervalTimer)) return;
            
            _objectPooler.SpawnFromPool("Collector", spawnPoints[0], Quaternion.identity);
            _spawnIntervalTimer = Time.time + spawnInterval;
        }

        public static void DisableEnemies(GameObject enemy)
        {
            enemy.SetActive(false);
        }
    }
}