using System;
using System.Collections;
using Scripts.Systems;
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
        private NavMeshAgent _agent;
        private ObjectPool _objectPool;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;


        [Header("Ranges")] 
        private float _maxRange = 8f;
        private float _mediumRange = 6f;
        private float _minRange = 4f;
        
        [Header("Attack")]
        [SerializeField] private float attackInterval;
        private float _attackIntervalCounter;
        
        private enum States
        {
            Approach,
            Attack,
            Flee,
        }
        
        private States _state;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _objectPool = ObjectPool.Instance;
            _animator.Play($"Walk");
            _state = States.Approach;
        }

        private void OnEnable()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
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
                case States.Approach:
                    ApproachState();
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
            }
           
            
            #region SETSTATE
            
            if (Vector2.Distance(_target.position, transform.position) > _maxRange)
            {
                _agent.enabled = true;
                _state = States.Approach;
            }
            else if (Vector2.Distance(_target.position, transform.position) < _minRange)
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
            
            if (Vector2.Distance(_target.position,transform.position) > _mediumRange)
            {
                _agent.enabled = false;
                _state = States.Attack;
            }
            else if (Vector2.Distance(_target.position, transform.position) > _maxRange)
            {
                _agent.enabled = true;
                _state = States.Approach;
            }

            #endregion
        }

        private void ApproachState()
        {
            SetMovement(_currentTarget.position);

            #region SETSTATE

            if (Vector2.Distance(_target.position, transform.position) < _mediumRange)
            {
                _agent.enabled = false;
                _state = States.Attack;
            }
            else if (Vector2.Distance(_target.position, transform.position) < _minRange)
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
            if (other.CompareTag("Player"))
            {
                ResetAgent(false);
                _objectPool.ReturnPooledObject(gameObject);
            }
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