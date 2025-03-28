using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class Explosion : MonoBehaviour
    {
        private ObjectPool _objectPool;
        private Animator _animator;

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            StartCoroutine(_objectPool.ReturnPooledObject(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }
}