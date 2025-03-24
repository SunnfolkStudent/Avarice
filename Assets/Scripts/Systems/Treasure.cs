using UnityEngine;

namespace Scripts.Systems
{
    public class Treasure : MonoBehaviour
    {
        // This is treasure which collected and is dropped by enemies
        public int treasureValue;
        public TreasureManager parentTreasure;
    }
}