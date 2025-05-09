using System.Collections.Generic;
using Enemies;
using Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace New_Systems
{
    public class WaveManager : MonoBehaviour
    {
        public int currentWave;
        [System.Serializable]
        public class Wave
        {
            public string key;
            public Transform[] spawnPoint;
            public Transform[] targets;
            public int spawnCount;
            [HideInInspector]public int spawnCounter;
            public float spawnInterval;
            [HideInInspector]public float spawnIntervalTimer;
            public float timeToNextWave;
        }
    
        public List<Wave> waves;
        private ObjectPool _objectPool;
        private UISystem _uiSystem;

        private void Start()
        {
            _objectPool = ObjectPool.Instance;
            waves[currentWave].spawnCounter = waves[currentWave].spawnCount;
            _uiSystem = FindAnyObjectByType<UISystem>();
        }

        private void FixedUpdate()
        {
            if (!(Time.time > waves[currentWave].spawnIntervalTimer)) return;
        
            var target = waves[currentWave].targets[Random.Range(0, waves[currentWave].targets.Length)];
            var spawn = waves[currentWave].spawnPoint[Random.Range(0, waves[currentWave].spawnPoint.Length)];
        
            var clone = _objectPool.SpawnFromPools(waves[currentWave].key, spawn, Quaternion.identity);
            if (waves[currentWave].key == "Collector")
            {
                print("Collector");
                clone.TryGetComponent(out CollectorController controller);
                controller.SetTarget(target, spawn);
                //clone.GetComponent<Scripts.Enemies.CollectorController>().SetTarget(target, spawn);
            }
            else if (waves[currentWave].key == "Archer")
            {
                print("Archer");
                //clone.GetComponent<Scripts.Enemies.ArcherController>().SetTarget(target);
                clone.TryGetComponent( out ArcherController controller);
                controller.SetTarget(target);
            }
        
        
            if (waves[currentWave].spawnCounter > 0)
            {
                waves[currentWave].spawnIntervalTimer = Time.time + waves[currentWave].spawnInterval;
                waves[currentWave].spawnCounter--;
            }
            else
            {
                if (currentWave < waves.Count-1)
                {
                    currentWave += 1;
                }
                else
                {
                    //currentWave = 0;
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                    var scoreManager = FindFirstObjectByType<GameManager>();
                    scoreManager.SetScore(_uiSystem.TreasureScore);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name != "Level 6" ? "LevelClear" : "Victory");
                }
            
                waves[currentWave].spawnCounter = waves[currentWave].spawnCount;
                waves[currentWave].spawnIntervalTimer = Time.time + waves[currentWave].timeToNextWave;
            }
        }
    }
}