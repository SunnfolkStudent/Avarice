using UnityEngine;

namespace New_Systems
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
            print("returned more thaan once?");
            originHoard.currentHoardValue += treasureValue;
            _objectPool.ReturnPooledObject(gameObject);
        }
    }
}