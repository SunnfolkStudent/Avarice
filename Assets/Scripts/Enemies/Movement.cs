using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    public class Movement: MonoBehaviour, IPooledObject
    {
        public Transform stairsTarget;
        public Transform currentTarget;
        private NavMeshAgent _agent;
        public bool carryingTreasure;
        private ObjectPool _objectPool;
        private float _speed = 2f;

        private Vector2 _velocity = Vector2.zero;
        
        private Animator _animator;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _objectPool = ObjectPool.Instance;
            _speed = _agent.speed;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
            _animator.Play($"Walk");
        }

        private void OnEnable()
        {
            OnObjectSpawn();
        }

        public void SetTarget(Transform target, Transform spawn)
        {
            currentTarget = target;
            stairsTarget = spawn;
            
        }

        public void OnObjectSpawn()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.ResetPath();
            _agent.SetDestination(Vector2.zero);
            _agent.speed = _speed;
            carryingTreasure = false;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
        }
        
        private void Update()
        {
            _agent.SetDestination(currentTarget.position);
            transform.position = Vector2.SmoothDamp(transform.position, _agent.nextPosition, ref _velocity, 0.1f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Hoard")) return;
            
            _agent.obstacleAvoidanceType = carryingTreasure ? ObstacleAvoidanceType.NoObstacleAvoidance : ObstacleAvoidanceType.HighQualityObstacleAvoidance;
           
            currentTarget = stairsTarget;
            carryingTreasure = true;
            _agent.speed /= 2;
            _animator.Play($"Loot");
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.CompareTag($"Stairs") && carryingTreasure)
            {
                _objectPool.ReturnPooledObject(gameObject);
            }

            if (other.CompareTag("Player"))
            {
                _objectPool.ReturnPooledObject(gameObject);
            }
        }
    }
}