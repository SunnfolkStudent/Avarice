using System.Collections.Generic;
using Scripts.Enemies;
using UnityEngine;

namespace Scripts.Systems
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int count;
        }

        #region Singleton
        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;
       
        
        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.count; i++)
                {
                    GameObject clone = Instantiate(pool.prefab);
                    clone.SetActive(false);
                    objectPool.Enqueue(clone);
                }
                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string key, Transform spawnPoint, Quaternion spawnRotation)
        {
            if (!poolDictionary.ContainsKey(key))
            {
                Debug.LogError($"Pool key {key} not found");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[key].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = spawnPoint.position;
            objectToSpawn.transform.rotation = spawnRotation;

            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawn();
            }

            poolDictionary[key].Enqueue(objectToSpawn);
            
            return objectToSpawn;
        }

        public static void DisableEnemies(GameObject enemy)
        {
            enemy.SetActive(false);
        }
    }
}