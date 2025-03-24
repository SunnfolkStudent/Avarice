using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class Animation : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private static readonly int Charge = Animator.StringToHash("Charge");
        private static readonly int Blast = Animator.StringToHash("Blast");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        public enum AnimationState
        {
            Idle = 0,
            Walk = 1,
            Dash = 2,
            Charge = 3,
            Blast = 4,
        }
        public AnimationState state;

        private void Start() => _animator = GetComponent<Animator>();

        public void UpdateAnimation(Vector2 moveDirection, bool dashing)
        {
            if (moveDirection != Vector2.zero)
            {
                _animator.SetFloat(Horizontal, moveDirection.x);
                _animator.SetFloat(Vertical, moveDirection.y);
                transform.localScale = new Vector3(Mathf.Sign(moveDirection.x), 1, 1);
            }

            if (dashing)
            {
                _animator.Play(Dash);
            }

            _animator.Play(moveDirection != Vector2.zero ? Walk : Idle);
        }
        
    }
}