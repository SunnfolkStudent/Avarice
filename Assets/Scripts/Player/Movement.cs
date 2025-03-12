using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float friction;
        
        //TODO: Add Friction
        //TODO: Add Acceleration
        //TODO: Add Charge
        
        private Rigidbody2D _rigidbody2D;
        

        private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();

        public void UpdateMovement(Vector2 moveDirection /*bool sprint*/)
        {
            _rigidbody2D.linearVelocity = moveDirection * moveSpeed;
        }
    }
}