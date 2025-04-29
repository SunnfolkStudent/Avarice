using TMPro;
using UnityEngine;

namespace New_Systems
{
    public class ScoreCanvas : MonoBehaviour
    {
        private TMP_Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TMP_Text>();
        }

        public void SetScoreText(int scoreValue)
        {
            _scoreText.text = "Current Score: " + scoreValue;
        }
        
    }
}