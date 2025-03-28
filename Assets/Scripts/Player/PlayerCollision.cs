using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private ObjectPool _objectPool;

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Arrow"))
            {
                SendMessage("TakeDamage");
            }

            if (other.CompareTag("Treasure"))
            {
                other.GetComponent<TreasureDrop>().ReturnToOriginHoard();
            }
        }
    }
}