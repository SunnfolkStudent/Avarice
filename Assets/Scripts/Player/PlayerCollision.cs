using New_Systems;
using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private ObjectPool _objectPool;
        private AudioSource _audioSource;
        public AudioClip hit;
        public AudioClip[] collectTreasure;

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Arrow"))
            {
                SendMessage("TakeDamage");
                _audioSource.PlayOneShot(hit);
            }

            if (other.CompareTag("Treasure"))
            {
                other.GetComponent<TreasureDrop>().ReturnToOriginHoard();
                _audioSource.PlayOneShot(collectTreasure[Random.Range(0, collectTreasure.Length)]);
                print("collected once?");
            }
        }
    }
}