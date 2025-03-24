using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Systems
{
    public class HoardManager : MonoBehaviour
    {
        [FormerlySerializedAs("_hoards")] public Hoard[] hoards;
        [FormerlySerializedAs("_currentHoardValues")] public int[] currentHoardValues;
        [FormerlySerializedAs("_totalHoardValue")] public int totalHoardValue;
        private int _previousHoardTotal;
        public float hoardPercentage;

        
        private void Start()
        {
            hoards = FindObjectsByType<Hoard>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            currentHoardValues = new int[hoards.Length];
            
            for (int i = 0; i < hoards.Length; i++)
            {
                UpdateHoardValue(i);
            }
        }

        private void Update()
        {
            for (int i = 0; i < hoards.Length; i++)
            {
                if (hoards[i].currentHoardValue != currentHoardValues[i])
                {
                    UpdateHoardValue(i);
                }
            }
            
            hoardPercentage = ((float)totalHoardValue/1000)*100f;
            //print("Hoard Value: "+ hoardPercentage +"%");
        }

        public int HoardValue()
        {
            return (int)hoardPercentage;
        }

        private void UpdateHoardValue(int i)
        {
            currentHoardValues[i] = hoards[i].currentHoardValue;
            totalHoardValue = currentHoardValues.Sum();
        }
        
    }
}