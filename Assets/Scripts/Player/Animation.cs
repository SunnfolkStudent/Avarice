using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class Animation : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Sprint = Animator.StringToHash("Sprint");
        private static readonly int Charge = Animator.StringToHash("Charge");
        private static readonly int Chomp = Animator.StringToHash("Chomp");
        private static readonly int Blast = Animator.StringToHash("Blast");

        private void Start() => _animator = GetComponent<Animator>();

        public void UpdateAnimation(Vector2 moveDirection)
        {
            _animator.Play(Walk);
        }
    }
}