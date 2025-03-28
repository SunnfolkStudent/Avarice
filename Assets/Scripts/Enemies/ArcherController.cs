using System;
using System.Collections;
using Scripts.New_Systems;
using UnityEngine;
using UnityEngine.AI;


namespace Scripts.Enemies
{
    public class ArcherController: MonoBehaviour
    {
        [Header("Targets")]
        private Transform _target;
        private Transform _currentTarget;
        
        private Vector2 _velocity = Vector2.zero;
        private bool _go;
        
        [Header("Components")]
        [SerializeField] private GameObject bloodSplatter;
        private NavMeshAgent _agent;
        private ObjectPool _objectPool;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        
        [Header("Ranges")] 
        [SerializeField] private float followRange = 8f;
        [SerializeField] private float attackRange = 6f;
        [SerializeField] private float fleeRange = 4f;
        
        [Header("Attack")]
        [SerializeField] private float attackInterval;
        private float _attackIntervalCounter;
        private bool _isDead;
  
        [Header("Audio")]
        private AudioSource _audioSource;
        public AudioClip shoot;
        
        private enum States
        {
            Follow,
            Attack,
            Flee,
        }
        
        private States _state;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _objectPool = ObjectPool.Instance;
            _animator.Play($"Walk");
            _state = States.Follow;
        }

        private void OnEnable()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
            _isDead = false;
        }

        public void SetTarget(Transform target)
        {
            _currentTarget = target;
            _target = target;
            StartCoroutine(WaitForActivate());
        }
        
        private void Update()
        {
            if (!_go) return;

            print(_state);
            switch (_state)
            {
                case States.Follow:
                    FollowState();
                    break;
                case States.Flee:
                    FleeState();
                    break;
                case States.Attack:
                    AttackingState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AttackingState()
        {
            if (_attackIntervalCounter < Time.time)
            {
                var targetPos = _target.position;
                var clone = _objectPool.SpawnFromPools("Arrows", transform, Quaternion.identity);
                clone.GetComponent<ArrowProjectile>().SetDirection(targetPos - transform.position);
                clone.transform.rotation = Quaternion.FromToRotation(transform.position, targetPos);
                StartCoroutine(_objectPool.ReturnPooledObject(clone, 5f));
                _attackIntervalCounter = Time.time + attackInterval;
                _audioSource.PlayOneShot(shoot);
            }
           
            
            #region SETSTATE
            
            if (Vector2.Distance(_target.position, transform.position) > followRange)
            {
                _agent.enabled = true;
                _state = States.Follow;
            }
            else if (Vector2.Distance(_target.position, transform.position) < fleeRange)
            {
                _agent.enabled = true;
                _state = States.Flee;
            }

            #endregion
        }

        private void FleeState()
        {
            Vector3 direction = transform.position - _target.position;
            Vector3 newPosition = transform.position + direction;
            
            SetMovement(newPosition);
            #region SETSTATE
            
            if (Vector2.Distance(_target.position,transform.position) > attackRange)
            {
                _agent.enabled = false;
                _state = States.Attack;
            }
            else if (Vector2.Distance(_target.position, transform.position) > followRange)
            {
                _agent.enabled = true;
                _state = States.Follow;
            }

            #endregion
        }

        private void FollowState()
        {
            SetMovement(_currentTarget.position);

            #region SETSTATE

            if (Vector2.Distance(_target.position, transform.position) < attackRange)
            {
                _agent.enabled = false;
                _state = States.Attack;
            }
            else if (Vector2.Distance(_target.position, transform.position) < fleeRange)
            {
                _agent.enabled = true;
                _state = States.Flee;
            }

            #endregion
        }

        private void SetMovement(Vector3 target)
        {
            _agent.SetDestination(target);
            transform.position = Vector2.SmoothDamp(transform.position, _agent.nextPosition, ref _velocity, 0.1f);
        }


        private void OnTriggerEnter2D(Collider2D other)
        { 
            if ((other.CompareTag("Player") || other.CompareTag("Fireball")) && !_isDead)
            {
                _objectPool.SpawnFromPools("BloodParticles", transform, Quaternion.identity);
                ResetAgent(false);
                _objectPool.ReturnPooledObject(gameObject);
                _isDead = true;
            }
            /*else if (other.CompareTag("Fireball") && !_isDead)
            {
                _objectPool.SpawnFromPools("Skeletons", transform, Quaternion.identity);
                ResetAgent(false);
                _objectPool.ReturnPooledObject(gameObject);
                _isDead = true;
            }*/
        }

        private void ResetAgent(bool setValue)
        {
            _agent.enabled = setValue;
            _go = setValue;
            _spriteRenderer.enabled = setValue;
        }

        private IEnumerator WaitForActivate()
        {
            yield return new WaitForSeconds(.5f);
            _animator.Play($"Walk");
            ResetAgent(true);
        }
    }
}