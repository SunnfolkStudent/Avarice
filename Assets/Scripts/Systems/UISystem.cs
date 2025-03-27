using TMPro;
using UnityEngine;

namespace Scripts.Systems
{
    public class UISystem : MonoBehaviour
    {
        public TMP_Text healthText;
        public TMP_Text treasureText;

        private float _health;
        private int _treasure;

        private void Update()
        {
            healthText.text = "Health: " + _health;
            treasureText.text = "Treasure: " + _treasure +"%";
        }

        public void ReturnHealthValue(float value)
        {
            _health = value;
        }

        public void ReturnTreasureValue(int value)
        {
            _treasure = value;
        }
    }
}