using System.Collections.Generic;
using Scripts.Systems;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string key;
        public Transform[] spawnPoint;
        public Transform[] targets;
        public int spawnCount;
        public int spawnCounter;
        public float spawnInterval;
        public float spawnIntervalTimer;
        public float timeToNextWave;
    }
    
    public List<Wave> waves;
    private ObjectPool _objectPool;
    public int currentWave;
    public int loopWave;

    private void Start()
    {
        _objectPool = ObjectPool.Instance;
        waves[currentWave].spawnCounter = waves[currentWave].spawnCount;
    }

    private void FixedUpdate()
    {
        if (!(Time.time > waves[currentWave].spawnIntervalTimer)) return;
        
        var target = waves[currentWave].targets[Random.Range(0, waves[currentWave].targets.Length)];
        var spawn = waves[currentWave].spawnPoint[Random.Range(0, waves[currentWave].spawnPoint.Length)];
        
        var clone = _objectPool.SpawnFromPools(waves[currentWave].key, spawn, Quaternion.identity);
        clone.GetComponent<Scripts.Enemies.Movement>().SetTarget(target, spawn);
        
        if (waves[currentWave].spawnCounter > 0)
        {
            waves[currentWave].spawnCounter--;
            waves[currentWave].spawnIntervalTimer = Time.time + waves[0].spawnInterval;
        }
        else
        {
            if (currentWave < waves.Count-1)
            {
                currentWave += 1;
            }
            else if (currentWave == loopWave)
            {
                currentWave = loopWave;
            }
            else
            {
                currentWave = 0;
            }
            
            waves[currentWave].spawnCounter = waves[currentWave].spawnCount;
            waves[currentWave].spawnIntervalTimer = Time.time + waves[currentWave].timeToNextWave;
        }
    }
}