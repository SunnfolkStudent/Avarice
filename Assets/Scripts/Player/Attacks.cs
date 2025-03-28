using Scripts.New_Systems;
using UnityEngine;

namespace Scripts.Player
{
    public class Attacks : MonoBehaviour
    {
        public float fireballInterval;
        private float _fireballTimeCounter;
        
        [SerializeField] private Transform[] fireballSpawnPoints;
        private Transform _fireballSpawnPoint;
        private ObjectPool _objectPool;
        private Vector2 _storedDirection;
        private readonly Vector2[] _directions = new []
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
            new Vector2(1,1), new Vector2(-1,1), new Vector2(1,-1),
            new Vector2(-1,-1)
        };
        
        private void Start()
        {
            _objectPool = ObjectPool.Instance;
        }
        
        
        public void UpdateFireball(bool shoot, Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
            {
                _storedDirection = moveDirection;
                _storedDirection.Normalize();
                if (_storedDirection.x != 0)
                {
                   _storedDirection.x = Mathf.Sign(_storedDirection.x);
                }
                if (_storedDirection.y != 0)
                {
                    _storedDirection.y = Mathf.Sign(_storedDirection.y);
                }
      
            }
            
            if (shoot && _fireballTimeCounter < Time.time)
            {
                for (int i = 0; i < _directions.Length; i++)
                {
                    if (_storedDirection == _directions[i])
                    {
                        _fireballSpawnPoint = fireballSpawnPoints[i];
                        var fireball = _objectPool.SpawnFromPools("Fireballs", _fireballSpawnPoint, Quaternion.identity );
                        fireball.GetComponent<Fireball>().SetMovement(_storedDirection);
                        _fireballTimeCounter = Time.time + fireballInterval;
                    }
                }
                
            }
        }
        
    }
}