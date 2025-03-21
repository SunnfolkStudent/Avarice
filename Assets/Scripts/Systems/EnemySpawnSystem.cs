using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Systems
{
    public class EnemySpawnSystem : MonoBehaviour
    {
        private ObjectPooler _objectPooler;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<Transform> hoardPoints;
        [SerializeField] private float spawnCooldown;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float timeBeforeSpawn = 6;
        private float _spawnIntervalTimer;
        public int spawnCount;
        
        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
            _spawnIntervalTimer = Time.time + timeBeforeSpawn;
        }

        private void FixedUpdate()
        {
            if (!(Time.time > _spawnIntervalTimer)) return;
            
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
           // int randomHoardPoint = Random.Range(0, hoardPoints.Count);
            
            _objectPooler.SpawnFromPool("Collector", spawnPoints[randomSpawnPoint], Quaternion.identity);
            
            if (spawnCount < 3)
            {
                spawnCount++;
                _spawnIntervalTimer = Time.time + spawnInterval;
            }
            else
            {
                _spawnIntervalTimer = Time.time + spawnCooldown;
                spawnCount = 0;
            }
        }
    }
}