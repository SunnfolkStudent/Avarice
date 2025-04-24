using New_Systems;
using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class Explosion : MonoBehaviour
    {
        private ObjectPool _objectPool;
        private Animator _animator;
        private AudioSource _audioSource;
        public AudioClip[] explosion;

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _audioSource.PlayOneShot(explosion[Random.Range(0,explosion.Length)]);
            StartCoroutine(_objectPool.ReturnPooledObject(gameObject, _animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }
}