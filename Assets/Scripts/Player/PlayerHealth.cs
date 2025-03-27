using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float invincibilityDuration;

        private void TakeDamage(float damage)
        {
            health -= damage;
        }
        
    }
}