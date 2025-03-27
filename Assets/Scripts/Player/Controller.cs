using UnityEngine;
using UnityEngine.AI;


namespace Scripts.Player
{
    [RequireComponent(typeof(Movement), typeof(GetInput), typeof(Attacks))]
    [RequireComponent(typeof(Animation), typeof(PlayerCollision))]
    public class Controller : MonoBehaviour
    {
        private GetInput _input;
        private Movement _movement;
        private Attacks _attacks;
        private Animation _animation;
        private PlayerCollision _collision;
        private NavMeshObstacle _navMeshObstacle;
        private readonly PlayerCollision _playerCollision;

     
        private void Start()
        {
            _input = GetComponent<GetInput>();
            _movement = GetComponent<Movement>();
            _attacks = GetComponent<Attacks>();
            _animation = GetComponent<Animation>();
            _collision = GetComponent<PlayerCollision>();
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        private void Update()
        {
            _animation.UpdateAnimation(_input.MoveDirection, _movement.IsDashing, _movement.IsStunned);
            
            if (_movement.IsStunned) return;
            if (_movement.IsDashing) return;
            _movement.UpdateMovement(_input.DashAttack,_input.MoveDirection);
            //_attacks.UpdateFireball(_input.FireBallAttack, _input.MoveDirection);
            _navMeshObstacle.enabled = _input.MoveDirection == Vector2.zero;
        }
    }
}