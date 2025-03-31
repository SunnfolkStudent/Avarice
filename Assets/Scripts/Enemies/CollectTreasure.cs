using UnityEngine;
using Scripts.New_Systems;

namespace Scripts.Enemies
{
    public class CollectTreasure: MonoBehaviour
    {
        [SerializeField] private int gold = 0;
        [SerializeField] private int stealAmount = 10;
        public int stolenTreasure;
        
        public Hoard _hoardPilfered;
        
        public void PickupTreasure(Hoard hoard)
        {
            _hoardPilfered = hoard;
            if (_hoardPilfered.currentHoardValue < stealAmount)
            {
                _hoardPilfered.currentHoardValue -= _hoardPilfered.currentHoardValue;
                stolenTreasure += _hoardPilfered.currentHoardValue;
            }
            else
            {
                _hoardPilfered.currentHoardValue -= stealAmount;
                stolenTreasure += stealAmount;
            }
        }
    }
}