using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Systems
{
    public class UISystem : MonoBehaviour
    {
       // public TMP_Text healthText;
        //public TMP_Text treasureText;

        private float _health;
        private int _treasure;
        
        public float goldPaddingValue;
        [FormerlySerializedAs("GoldSlider")] public RectMask2D goldSlider;
        public float healthPaddingValue;
        [FormerlySerializedAs("HealthSlider")] public RectMask2D healthSlider;
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void FireFireball()
        {
            _animator.Play("igniting Charge");
        }

        public void ChargeFireball()
        {
            _animator.Play("building charge");
        }
        

        private void Update()
        {
           // healthText.text = "Health: " + _health;
            // treasureText.text = "Treasure: " + _treasure +"%";
            UpdateSliderValue();

            if (_treasure <= 50)
            {
                SceneManager.LoadScene("GoldDeath");
            }

            if (_health <= 0)
            {
                SceneManager.LoadScene("BloodDeath");
            }
        }

        public void ReturnHealthValue(float value)
        {
            _health = value;
        }

        public void ReturnTreasureValue(int value)
        {
            _treasure = value;
        }

        private void UpdateSliderValue()
        {
            goldPaddingValue = (100 - _treasure) * 9f;
            goldSlider.padding = new Vector4(0,0, goldPaddingValue,0);
            // 4.5 per value of 
            healthPaddingValue = (100 - _health) * 4.5f;
            healthSlider.padding = new Vector4(0,0, healthPaddingValue,0);
            
        }
    }
}