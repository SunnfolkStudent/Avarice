using Scripts.Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Enemies
{
    public class CollectTreasure: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private int purse = 10;
        [SerializeField] private int bagSpace = 30;
        [FormerlySerializedAs("treasure")] [SerializeField] private GameObject treasureToDrop;
        private TreasureManager _treasurePicked;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void PickupTreasure(ref int tv, TreasureManager treasureParent)
        {
            _spriteRenderer.color = Color.yellow;
            _treasurePicked = treasureParent;
            tv -= bagSpace;
        }

        public void DropTreasure()
        {
            var clone = Instantiate(treasureToDrop, transform.position, Quaternion.identity);
            clone.GetComponent<Treasure>().parentTreasure = _treasurePicked;
        }
    }
}