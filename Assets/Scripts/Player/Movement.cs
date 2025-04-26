using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody2D _rigidbody2D;
        
        [Header("Dash & Stun")]
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 0.5f;
        [SerializeField] private float dashCooldown = 15f;
        [SerializeField] private float stunDuration = 1.5f;
        private bool _canDash = true;
        private Vector2 _moveDirection;
        private Vector2 _storedDirection;
        
        public bool IsDashing {get; private set;}
        public bool IsStunned { get; private set; }
        
        [Header("Audio")]
        private AudioSource _audioSource;
        [SerializeField] private AudioClip dashClip;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _storedDirection = Vector2.up;
        }

        private void Start() => _rigidbody2D = GetComponent<Rigidbody2D>();
        

        public void UpdateMovement(bool dash, Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
            
            if (_moveDirection != Vector2.zero)
            {
                _storedDirection = _moveDirection;
            }
            
            _rigidbody2D.linearVelocity = _moveDirection * moveSpeed;

            if (!dash || !_canDash) return;
            
            _audioSource.PlayOneShot(dashClip);
            StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            _canDash = false;
            IsDashing = true;
            _rigidbody2D.linearVelocity = _storedDirection * dashSpeed;
            yield return new WaitForSeconds(dashDuration);
            IsDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            _canDash = true;
        }

        private IEnumerator Stun()
        {
            IsStunned = true;
            StopCoroutine(Dash());
            _canDash = false;
            IsDashing = false;
            _rigidbody2D.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(stunDuration);
            IsStunned = false;
            _canDash = true;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Wall")) return;
            if (!IsDashing) return;
            StartCoroutine(Stun());
        }
    }
}