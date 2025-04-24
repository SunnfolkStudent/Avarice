using New_Systems;
using UnityEngine;

namespace Enemies
{
    public class CollectTreasure: MonoBehaviour
    {
        public int stolenTreasure;
        private int _maxStealAmount = 10, _midStealAmount = 5, _minStealAmount = 1 ;
        private int _currentStealAmount;
        [HideInInspector] public Hoard _hoardPilfered;
        
        public void PickupTreasure(Hoard hoard)
        {
            _hoardPilfered = hoard;
            switch (_hoardPilfered.currentHoardValue)
            {
                case < 0:
                    return;
                case < 50:
                    _currentStealAmount = _minStealAmount;
                    break;
                case < 300:
                    _currentStealAmount = _midStealAmount;
                    break;
                case > 450:
                    _currentStealAmount = _maxStealAmount;
                    break;
            }

            if (_hoardPilfered.currentHoardValue < _currentStealAmount)
            {
                _hoardPilfered.currentHoardValue -= _hoardPilfered.currentHoardValue;
                stolenTreasure += _hoardPilfered.currentHoardValue;
            }
            else
            {
                _hoardPilfered.currentHoardValue -= _currentStealAmount;
                stolenTreasure += _currentStealAmount;
            }
        }
    }
}