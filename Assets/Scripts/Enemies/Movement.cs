using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    public class Movement: MonoBehaviour
    {
        //TODO: ADD NAVMESH
        //TODO: Add Move to closest Treasure
        //TODO: Add Statemachine (enums)
        //TODO: Add Animation
        //TODO: Add Move to Closest Door if Have Treasure
        
        //TODO: Check if Player can obstruct NavMesh dynamically?
        
        [SerializeField] Transform treasureTarget;
        [SerializeField] Transform doorTarget;
        private Transform _currentTarget;
        private NavMeshAgent _agent;
        private bool _carryingTreasure;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _currentTarget = treasureTarget;
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
                _carryingTreasure = true;
            }
            else if (other.gameObject.CompareTag($"Door") && _carryingTreasure)
            {
                Destroy(gameObject);
            }
        }
    }
}