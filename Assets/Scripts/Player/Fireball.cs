using System.Collections.Generic;
using New_Systems;
using UnityEngine;

namespace Player
{
    public class Fireball : MonoBehaviour
    {
        public Vector2[] directions = new []
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
            new Vector2(1,1), new Vector2(-1,1), new Vector2(1,-1),
            new Vector2(-1,-1)
        };
        
        public float speed = 6;
        public float timeToExplosion = 1f;
        private float _timeToExplosionCounter;
        
        private ObjectPool _objectPool;
        private Rigidbody2D _rigidbody2D;
        

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _timeToExplosionCounter = Time.time + timeToExplosion;
        }

        private List<int> _collisionID;

        public void SetMovement(Vector2 direction)
        {
            _rigidbody2D.linearVelocity = direction * speed;
        }

        private void Update()
        {
            if (_timeToExplosionCounter < Time.time)
            {
                _objectPool.SpawnFromPools("Explosions", transform, Quaternion.identity);
                _objectPool.ReturnPooledObject(gameObject);
            }
            foreach (var direction in directions)
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
            {
                _timeToExplosionCounter = 0;
            }
        }

        /*public void OnDetonation()
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
           
        }*/

        private void OnGUI()
        {
            foreach (var direction in directions)
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }
    }
}