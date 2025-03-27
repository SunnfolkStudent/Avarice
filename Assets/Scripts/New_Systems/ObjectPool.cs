using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Systems
{
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string key;
            public List<GameObject> objectPool = new List<GameObject>();
            public GameObject objectToSpawn;
            public Transform objectParent;
            public int spawnCount;
        }

        public List<Pool> pools;
        
        #region SINGLETON
        public static ObjectPool Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion

        private void Start()
        {
            InitialisePools();
        }

        private void InitialisePools()
        {
            foreach (var pool in pools)
            {
                for (int i = 0; i < pool.spawnCount; i++)
                {
                    var obj = Instantiate(pool.objectToSpawn, pool.objectParent, true);
                    obj.SetActive(false);
                    pool.objectPool.Add(obj);
                }
            }
        }
        
        private bool AreAllItemsInListActive(Pool pool)
        {
            return pool.objectPool.All(item => item.activeInHierarchy);
        }

        private GameObject GetInactiveObjectInPool(Pool pool)
        {
            return pool.objectPool.Find(item => !item.activeInHierarchy) ?? pool.objectPool.First() ?? pool.objectPool.Last();
        }
        
        public GameObject SpawnFromPools(string key, Transform spawnPoint, Quaternion spawnRotation)
        {
            GameObject objectToSpawn = null;
            foreach (var pool in pools)
            {
                if (pool.key == key)
                {
                    if (AreAllItemsInListActive(pool))
                    {
                        objectToSpawn = Instantiate(pool.objectToSpawn, pool.objectParent, true);
                        pool.objectPool.Add(objectToSpawn);
                    }
                    else
                    {
                        objectToSpawn = GetInactiveObjectInPool(pool);
                        objectToSpawn.SetActive(true);
                    }
                    
                    objectToSpawn.transform.position = spawnPoint.position;
                    objectToSpawn.transform.rotation = spawnRotation;
                }
            }
            return objectToSpawn;
        }
        
        public void ReturnPooledObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.position = Vector2.zero;
        }

        public IEnumerator ReturnPooledObject(GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);
            obj.transform.position = Vector2.zero;
        }
    }
}