using System;
using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Scripts.Enemies
{
    public class Movement: MonoBehaviour, IPooledObject
    {
        //TODO: Add State machine (enums)
        //TODO: Add Animation
        //TODO: Check if Player can obstruct NavMesh dynamically?
        
        [HideInInspector] public Transform hoardTarget;
        [HideInInspector] public Transform stairsTarget;
        private Transform _currentTarget;
        private NavMeshAgent _agent;
        public bool carryingTreasure;

        private float _holdingSpeed = 2.5f;

        public void OnObjectSpawn()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _currentTarget = hoardTarget;
        }
        
        private void Update()
        {
            _agent.SetDestination(_currentTarget.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag($"Hoard"))
            {
                _currentTarget = stairsTarget;
                carryingTreasure = true;
                _agent.speed = _holdingSpeed;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.CompareTag($"Stairs") && carryingTreasure)
            {
                EnemySpawnSystem.DisableEnemies(gameObject);
            }
        }
    }
}