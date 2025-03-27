using System;
using System.Collections.Generic;
using Scripts.Enemies;
using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class Fireball : MonoBehaviour, IPooledObject
    {
        public Vector2[] directions = new []
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
            new Vector2(1,1), new Vector2(-1,1), new Vector2(1,-1),
            new Vector2(-1,-1)
        };

        private ObjectPool _objectPool;

        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }

        private List<int> _collisionID;
        
        public void OnObjectSpawn()
        {
            
        }

        private void Update()
        {
            foreach (var direction in directions)
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }

        public void OnDetonation()
        {
            var get = Physics2D.OverlapCircleAll(transform.position, 5f);
            
            foreach (var collision in get)
            {
                if (collision.CompareTag("Enemy"))
                {
                    _collisionID.Add(collision.gameObject.GetInstanceID());
                }
            }
            foreach (var direction in directions)
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 2f, direction);
                Debug.DrawRay(transform.position, direction, Color.red);
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.transform.GetInstanceID() == _collisionID[^1])
                    {
                        _objectPool.ReturnPooledObject(hit.transform.gameObject);
                    }
                }
            }
           
        }

        private void OnGUI()
        {
            foreach (var direction in directions)
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }
    }
}