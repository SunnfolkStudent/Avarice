using System.Linq;
using Systems;
using UnityEngine;

namespace New_Systems
{
    public class HoardManager : MonoBehaviour
    {
        public Hoard[] hoards;
        private int[] _currentHoardValues;
        private int _totalHoardValue;
        private int _previousHoardTotal;
        public float hoardPercentage;

        public UISystem ui;

        
        private void Start()
        {
            hoards = FindObjectsByType<Hoard>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            ui = GameObject.FindFirstObjectByType<UISystem>();
            _currentHoardValues = new int[hoards.Length];
            
            for (int i = 0; i < hoards.Length; i++)
            {
                UpdateHoardValue(i);
            }
        }

        private void Update()
        {
            for (int i = 0; i < hoards.Length; i++)
            {
                if (hoards[i].currentHoardValue != _currentHoardValues[i])
                {
                    UpdateHoardValue(i);
                }
            }
            
            hoardPercentage = ((float)_totalHoardValue/1000)*100f;
            
            ui.ReturnTreasureValue(HoardValue());
            //print("Hoard Value: "+ hoardPercentage +"%");
        }

        private int HoardValue()
        {
            return (int)hoardPercentage;
        }

        private void UpdateHoardValue(int i)
        {
            _currentHoardValues[i] = hoards[i].currentHoardValue;
            _totalHoardValue = _currentHoardValues.Sum();
        }
        
    }
}