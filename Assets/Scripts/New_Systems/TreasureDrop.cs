using UnityEngine;

namespace Scripts.New_Systems
{
    public class TreasureDrop : MonoBehaviour
    {
        public Hoard originHoard;
        public int treasureValue;
        private ObjectPool _objectPool;

        private void Awake()
        {
            _objectPool = ObjectPool.Instance;
        }

        public void ReturnToOriginHoard()
        {
            originHoard.currentHoardValue += treasureValue;
            _objectPool.ReturnPooledObject(gameObject);
        }
    }
}