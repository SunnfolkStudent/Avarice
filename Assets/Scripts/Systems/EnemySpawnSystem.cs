using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Systems
{
    public class EnemySpawnSystem : MonoBehaviour
    {
        private ObjectPooler _objectPooler;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<Transform> hoardPoints;
        [SerializeField] private float spawnInterval;
        private float _spawnIntervalTimer;
        public int spawnCount;
        
        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
            _spawnIntervalTimer = Time.time + spawnInterval;
        }

        private void FixedUpdate()
        {
            if (!(Time.time > _spawnIntervalTimer)) return;
            
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            int randomHoardPoint = Random.Range(0, hoardPoints.Count);
            for (int i = 0; i < spawnCount; i++)
            {
                _objectPooler.SpawnFromPool("Collector", spawnPoints[randomSpawnPoint], Quaternion.identity, hoardPoints[randomHoardPoint]);
            }
            _spawnIntervalTimer = Time.time + spawnInterval;
        }

        public static void DisableEnemies(GameObject enemy)
        {
            enemy.SetActive(false);
        }
    }
}