using System.Collections;
using New_Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class CollectorController: MonoBehaviour
    {
        public Transform stairsTarget;
        public Transform currentTarget;
      
        public bool carryingTreasure;
       
        private Vector2 _velocity = Vector2.zero;
        private bool _go;
        

        private bool _isDead;
        private ObjectPool _objectPool;
        private NavMeshAgent _agent;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private CollectTreasure _collectTreasure;
        
        [Header("Audio")]
        private AudioSource _audioSource;
        public AudioClip[] collectGoldSfx;
    
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collectTreasure = GetComponent<CollectTreasure>();
            _audioSource = GetComponent<AudioSource>();
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
            
            _collectTreasure.PickupTreasure(other.transform.GetComponent<Hoard>());
            _audioSource.PlayOneShot(collectGoldSfx[Random.Range(0, collectGoldSfx.Length)]);
            carryingTreasure = true;
            
            _agent.obstacleAvoidanceType = carryingTreasure ? ObstacleAvoidanceType.NoObstacleAvoidance : ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            currentTarget = stairsTarget;
            _agent.speed /= 2;
            _animator.Play($"Loot");
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if ((other.CompareTag("Player")/* || other.CompareTag("Fireball")*/) && !_isDead)
            {
                IfCarryingTreasure();
                
                var rand = Random.Range(0, 2);
                print(rand +"blood");
                _objectPool.SpawnFromPools(rand == 0 ? "BloodParticles" : "BloodParticles2", transform, Quaternion.identity);

                _isDead = true;
                ResetAgent(false);
                _objectPool.ReturnPooledObject(gameObject);
            }
            else if (other.CompareTag("Fireball") && !_isDead)
            {
                IfCarryingTreasure();
                
                var rand = Random.Range(0, 2);
                print(rand +"bone");
                _objectPool.SpawnFromPools(rand == 0 ? "Skeletons1" : "Skeletons2", transform, Quaternion.identity);
                ResetAgent(false);
                _isDead = true;
                
            }
            
            if ((other.CompareTag($"Stairs") && carryingTreasure))
            {
                ResetAgent(false);
                _objectPool.ReturnPooledObject(gameObject);
                _agent.speed = 3;
            }
        }

        private void IfCarryingTreasure()
        {
            if (carryingTreasure)
            {
                var clone = _objectPool.SpawnFromPools("Treasure", transform, Quaternion.identity);
                var script = clone.GetComponent<TreasureDrop>();
                script.treasureValue = _collectTreasure.stolenTreasure;
                script.originHoard = _collectTreasure._hoardPilfered;
                _agent.speed = 3;
            }
        }

        private void ResetAgent(bool setValue)
        {
            _agent.enabled = setValue;
            _go = setValue;
            _spriteRenderer.enabled = setValue;
            _collectTreasure.stolenTreasure = 0;
        }

        private IEnumerator WaitForActivate()
        {
            yield return new WaitForSeconds(.5f);
            _animator.Play($"Walk");
            ResetAgent(true);
        }
    }
}