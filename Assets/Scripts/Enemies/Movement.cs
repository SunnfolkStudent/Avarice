using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    public class Movement: MonoBehaviour, IPooledObject
    {
        //TODO: Add State machine (enums)
        //TODO: Add Animation
        //TODO: Check if Player can obstruct NavMesh dynamically?
        
        public Transform stairsTarget;
        private Transform _currentTarget;
        private NavMeshAgent _agent;
        public bool carryingTreasure;
        ObjectPool _objectPool;
        private float _speed = 2f;

        private void Start()
        {
            _objectPool = ObjectPool.Instance;
            _speed = _agent.speed;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            OnObjectSpawn();
        }

        public void SetTarget(Transform target, Transform spawn)
        {
            _currentTarget = target;
            stairsTarget = spawn;
        }

        public void OnObjectSpawn()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
            carryingTreasure = false;
        }
        
        private void Update()
        {
            _agent.SetDestination(_currentTarget.position);
            _agent.obstacleAvoidanceType = carryingTreasure ? ObstacleAvoidanceType.NoObstacleAvoidance : ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Hoard")) return;
            
            _currentTarget = stairsTarget;
            carryingTreasure = true;
            _agent.speed = _agent.speed/2;
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.CompareTag($"Stairs") && carryingTreasure)
            {
                _objectPool.ReturnPooledObject(gameObject);
            }
        }
    }
}