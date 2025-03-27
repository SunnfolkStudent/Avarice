using System.Collections;
using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    public class CollectorController: MonoBehaviour
    {
        public Transform stairsTarget;
        public Transform currentTarget;
        private NavMeshAgent _agent;
        public bool carryingTreasure;
        private ObjectPool _objectPool;

        private Vector2 _velocity = Vector2.zero;
        
        private Animator _animator;
        public bool archer;
        private bool _go;
        private SpriteRenderer _spriteRenderer;
        private bool _isDead = false;

    
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _objectPool = ObjectPool.Instance;
            _animator.Play($"Walk");
        }

        private void OnEnable()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.updatePosition = false;
            carryingTreasure = false;
            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            _isDead = false;
        }

        public void SetTarget(Transform target, Transform spawn)
        {
            currentTarget = target;
            stairsTarget = spawn;
            StartCoroutine(WaitForActivate());
        }
        
        private void Update()
        {
            if (!_go) return;
            _agent.SetDestination(currentTarget.position);
            transform.position = Vector2.SmoothDamp(transform.position, _agent.nextPosition, ref _velocity, 0.1f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag($"Hoard")) return;
            if (archer) return;
            
            carryingTreasure = true;
            
            _agent.obstacleAvoidanceType = carryingTreasure ? ObstacleAvoidanceType.NoObstacleAvoidance : ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            currentTarget = stairsTarget;
            _agent.speed /= 2;
            _animator.Play($"Loot");
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.CompareTag("Player") && !_isDead)
            {
                _objectPool.SpawnFromPools("BloodParticles", transform, Quaternion.identity);
                _isDead = true;
            }
            if ((other.CompareTag($"Stairs") && carryingTreasure) || other.CompareTag("Player"))
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