using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Systems
{
    public class HoardManager : MonoBehaviour
    {
        public Hoard[] _hoards;
        public int[] _currentHoardValues;
        public int _totalHoardValue;
        private int _previousHoardTotal;
        public float hoardPercentage;

        
        private void Start()
        {
            _hoards = Object.FindObjectsByType<Hoard>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _currentHoardValues = new int[_hoards.Length];
            
            for (int i = 0; i < _hoards.Length; i++)
            {
                UpdateHoardValue(i);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _hoards.Length; i++)
            {
                if (_hoards[i].currentHoardValue != _currentHoardValues[i])
                {
                    UpdateHoardValue(i);
                }
            }
            
            hoardPercentage = ((float)_totalHoardValue/1000)*100f;
            //print("Hoard Value: "+ hoardPercentage +"%");
        }

        public int HoardValue()
        {
            return (int)hoardPercentage;
        }

        private void UpdateHoardValue(int i)
        {
            _currentHoardValues[i] = _hoards[i].currentHoardValue;
            _totalHoardValue = (int)_currentHoardValues.Sum();
        }
        
    }
}