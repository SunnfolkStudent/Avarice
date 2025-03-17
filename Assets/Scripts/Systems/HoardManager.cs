using System.Linq;
using UnityEngine;

namespace Scripts.Systems
{
    public class HoardManager : MonoBehaviour
    {
        // Manage all Hoards as one
        // Connect the h
        private Hoard[] _hoards;
        private int[] _previousHoardValues;
        private int[] _currentHoardValues;
        private int _totalHoardValue;

        private void Start()
        {
            for (int i = 0; i < _hoards.Length; i++)
            {
                UpdateHoardValue(i);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _previousHoardValues.Length; i++)
            {
                if (_previousHoardValues[i] != _currentHoardValues[i])
                {
                    UpdateHoardValue(i);
                    UpdateHoardTotalValue();
                }
            }
        }

        private void UpdateHoardValue(int i)
        {
            _currentHoardValues[i] = _hoards[i].currentHoardValue;
            _previousHoardValues[i] = _hoards[i].currentHoardValue;
        }

        private void UpdateHoardTotalValue()
        {
            _totalHoardValue = (int)_currentHoardValues.Sum();
        }
    }
}