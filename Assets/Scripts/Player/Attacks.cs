using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class Attacks : MonoBehaviour
    {
        ObjectPool _objectPool;
        public Transform fireballSpawnPoint;
        public float fireballLifeTime;
        public float fireballInterval;
        private float _fireballTimeCounter;
        
        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }

        public void SpawnFireBall()
        {
            
        }
        
        public void UpdateFireball(bool shoot, Vector2 moveDirection)
        {
            if (shoot && _fireballTimeCounter < Time.time)
            {
                var fireball = _objectPool.SpawnFromPools("Fireball", fireballSpawnPoint, Quaternion.identity );
                StartCoroutine(_objectPool.ReturnPooledObject(fireball, fireballLifeTime));
                _fireballTimeCounter = Time.time + fireballInterval;
            }
        }
        
    }
}