using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Scripts.Enemies
{
    public class Movement: MonoBehaviour, IPooledObject
    {
        //TODO: ADD NAVMESH
        //TODO: Add Move to closest Treasure
        //TODO: Add State machine (enums)
        //TODO: Add Animation
        //TODO: Add Move to Closest Door if Have Treasure
        
        //TODO: Check if Player can obstruct NavMesh dynamically?
        
        [SerializeField] Transform treasureTarget;
        [SerializeField] Transform doorTarget;
        private Transform _currentTarget;
        private NavMeshAgent _agent;
        public bool carryingTreasure;

        private float _holdingSpeed = 2.5f;

        public void OnObjectSpawn()
        {
            SetDestinations();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _currentTarget = treasureTarget;
        }

        private void SetDestinations()
        {
            treasureTarget = GameObject.FindGameObjectWithTag("Treasure").transform;
            doorTarget = GameObject.FindGameObjectWithTag("Door").transform;
        }

        private void Update()
        {
            _agent.SetDestination(_currentTarget.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag($"Treasure"))
            {
                _currentTarget = doorTarget;
                carryingTreasure = true;
                _agent.speed = _holdingSpeed;
            }
            else if (other.gameObject.CompareTag($"Door") && carryingTreasure)
            {
                EnemySpawnSystem.DisableEnemies(gameObject);
            }
        }
    }
}