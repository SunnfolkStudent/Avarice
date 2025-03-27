using UnityEngine;

namespace Scripts.Systems
{
    public class BloodParticleReset : MonoBehaviour
    {
        private ObjectPool _pool;
        public GameObject bloodSplatter;
        [Header("Audio")]
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] deathClips;


        private void Awake()
        {
            _pool = ObjectPool.Instance;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            StartCoroutine(_pool.ReturnPooledObject(gameObject, 3f));
            _audioSource.PlayOneShot(deathClips[Random.Range(0, deathClips.Length)]);
        }

        public void SpawnBloodSplatter()
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        }
    }
}