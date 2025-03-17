using Scripts.Enemies;
using Scripts.Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Player
{
    [RequireComponent(typeof(Movement), typeof(GetInput), typeof(Attacks))]
    [RequireComponent(typeof(Animation))]
    public class Controller : MonoBehaviour
    {
        private GetInput _input;
        private Movement _movement;
        private Attacks _attacks;
        private Animation _animation;
        private NavMeshObstacle _navMeshObstacle;

        private void Start()
        {
            _input = GetComponent<GetInput>();
            _movement = GetComponent<Movement>();
            _attacks = GetComponent<Attacks>();
            _animation = GetComponent<Animation>();
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        private void Update()
        {
            _movement.UpdateMovement(_input.MoveDirection);
            
            _navMeshObstacle.enabled = _input.MoveDirection == Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"Enemy"))
            {
                if (other.GetComponent<Enemies.Movement>().carryingTreasure)
                {
                    other.GetComponent<CollectTreasure>().DropTreasure();
                }
                other.gameObject.SetActive(false); 
            }

            if (other.CompareTag("DroppedTreasure"))
            {
                var tm = other.GetComponent<DroppedTreasure>();
                tm._parentTreasure.treasureValue += tm.treasureValue;
                Destroy(other.gameObject);
            }
        }
    }
}