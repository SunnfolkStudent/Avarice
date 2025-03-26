using UnityEngine;

namespace Scripts.Enemies
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] private float arrowSpeed; 
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            _rigidbody.linearVelocity = direction.normalized * arrowSpeed;
            print("I am arrow");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}