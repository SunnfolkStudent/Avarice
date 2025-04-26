using New_Systems;
using UnityEngine;

namespace Enemies
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] private float arrowSpeed; 
        private Rigidbody2D _rigidbody;
        private ObjectPool _objectPool;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _objectPool = ObjectPool.Instance;
        }

        public void SetDirection(Vector2 direction)
        {
            _rigidbody.linearVelocity = direction.normalized * arrowSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Wall"))
            {
                _objectPool.ReturnPooledObject(gameObject);
            }
        }
    }
}