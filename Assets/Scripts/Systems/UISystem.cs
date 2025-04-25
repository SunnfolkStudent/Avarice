using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class UISystem : MonoBehaviour
    {
        public TMP_Text healthText;
        public TMP_Text treasureText;

        private float _health;
        private int _treasure;
        public float paddingValue;

        public RectMask2D paddingObject;

        private void Update()
        {
            healthText.text = "Health: " + _health;
            treasureText.text = "Treasure: " + _treasure +"%";
            UpdateSliderValue();
        }

        public void ReturnHealthValue(float value)
        {
            _health = value;
        }

        public void ReturnTreasureValue(int value)
        {
            _treasure = value;
        }
        
        public void UpdateSliderValue()
        {
            paddingValue = (100 - _treasure) * 4.5f;
            paddingObject.padding = new Vector4(0,0, paddingValue,0);
            // 4.5 per value of 
        }
    }
}