using New_Systems;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private ObjectPool _objectPool;
        private AudioSource _audioSource;
        public AudioClip hit;
        public AudioClip[] collectTreasure;

        public Collider2D VerticalCollider2D, HorizontalCollider2D;
        public Collider2D VerticalTrigger, HorizontalTrigger;
        private Vector2 Up = Vector2.up, Down = Vector2.down, Left = Vector2.left, Right = Vector2.right, 
            RightUp = new Vector2(1,1), LeftUp = new Vector2(-1,1), RightDown = new Vector2(1,-1), LeftDown = new Vector2(-1,-1);
        
        
        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
            _audioSource = GetComponent<AudioSource>();
            VerticalCollider2D.enabled = true;
            HorizontalCollider2D.enabled = false;
            VerticalTrigger.enabled = true;
            HorizontalTrigger.enabled = false;
        }

        public void UpdateCollision(Vector2 moveDirection)
        {
            if (moveDirection == Vector2.zero) return;
            
            if (moveDirection == Left || moveDirection == Right)
            {
                VerticalCollider2D.enabled = false;
                VerticalTrigger.enabled = false;
                HorizontalCollider2D.enabled = true;
                HorizontalTrigger.enabled = true;
            }
            else
            {
                VerticalCollider2D.enabled = true;
                VerticalTrigger.enabled = true;
                HorizontalCollider2D.enabled = false;
                HorizontalTrigger.enabled = false;
            }
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