using System.Collections;
using New_Systems;
using Systems;
using UnityEngine;

namespace Player
{
    public class Attacks : MonoBehaviour
    {
        public float fireballInterval;
        [SerializeField] private float _fireballTimeCounter;
        
        [SerializeField] private Transform[] fireballSpawnPoints;
        public Transform _fireballSpawnPoint;
        private ObjectPool _objectPool;
        public Vector2 _storedDirection;
        
        private AudioSource _audioSource;
        public AudioClip shootClip;
        
        private UISystem uiSystem;
        private readonly Vector2[] _directions = new []
        {
            Vector2.up, Vector2.right, Vector2.down, Vector2.left, 
            new Vector2(1,1), new Vector2(-1,1), new Vector2(1,-1),
            new Vector2(-1,-1)
        };
        
        private void Start()
        {
            _objectPool = ObjectPool.Instance;
            _audioSource = GetComponent<AudioSource>();
            _fireballSpawnPoint = fireballSpawnPoints[0];
            uiSystem = FindFirstObjectByType<UISystem>();
            _storedDirection = Vector2.up;
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
                uiSystem.FireFireball();
                for (int i = 0; i < _directions.Length; i++)
                {
                    if (_storedDirection == _directions[i])
                    {
                        _fireballSpawnPoint = fireballSpawnPoints[i];
                        _audioSource.PlayOneShot(shootClip);
                        
                        var clone = _objectPool.SpawnFromPools("Fireball", _fireballSpawnPoint, Quaternion.identity );
                        clone.TryGetComponent(out Fireball fireball);
                        fireball.SetMovement(_storedDirection);
                        //clone.GetComponent<Fireball>().SetMovement(_storedDirection);
                        _fireballTimeCounter = Time.time + fireballInterval;
                        StartCoroutine(nameof(ChargeFireball));
                    }
                }
                
            }
        }
        private IEnumerator ChargeFireball()
        {
            yield return new WaitForSeconds(fireballInterval-0.2f);
            uiSystem.ChargeFireball();
        }
    }
}