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
            if (other.CompareTag($"Enemy"))
            {
                /*if (other.GetComponent<Enemies.Movement>().carryingTreasure)
                {
                    other.GetComponent<CollectTreasure>().DropTreasure();
                }*/
                //other.gameObject.SetActive(false); 
            }

            /*if (other.CompareTag("Treasure"))
            {
                var tm = other.GetComponent<DroppedTreasure>();
                tm._parentTreasure.treasureValue += tm.treasureValue;
                Object.Destroy(other.gameObject);
            }*/
        }
    }
}