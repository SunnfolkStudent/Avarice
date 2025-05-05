using Systems;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        [SerializeField] private float invincibilityDuration = .1f;
        private float _invincibilityTimer;
        [SerializeField] private float damage = 2f;
        public UISystem ui;

        private void Awake()
        {
            ui = GameObject.FindFirstObjectByType<UISystem>();
        }

        private void Update()
        {
            ui.ReturnHealthValue(health);
        }

        public void TakeDamage()
        {
            if (_invincibilityTimer < Time.time)
            {
                health -= damage;
                _invincibilityTimer = Time.time + invincibilityDuration;
            }
        }
        
    }
}