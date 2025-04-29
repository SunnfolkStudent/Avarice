using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace New_Systems
{
    public class GameManager : MonoBehaviour
    {
        private static readonly int Level = Animator.StringToHash("Level");
        private Animator _animator;
        public int previousLevel;
        private bool _hasLoaded;

        public int Score { get; private set; }
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            print(SceneManager.GetActiveScene().name);
            
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                
                previousLevel = 1;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                previousLevel = 2;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                previousLevel = 3;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 4")
            {
                previousLevel = 4;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 5")
            {
                previousLevel = 5;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 6")
            {
                previousLevel = 6;
                _hasLoaded = true;
            }
            else if (SceneManager.GetActiveScene().name == "LevelClear" && _hasLoaded)
            {
                _animator = FindFirstObjectByType<Animator>();
                _animator.SetFloat(Level, previousLevel);
                
                var scoreCanvas = FindFirstObjectByType<ScoreCanvas>();
                scoreCanvas.SetScoreText(Score);
                
                StartCoroutine(nameof(WaitForLevelChange));
                _hasLoaded = false;
            }
            else if (SceneManager.GetActiveScene().name == "Victory")
            {
                var scoreCanvas = FindFirstObjectByType<ScoreCanvas>();
                scoreCanvas.SetScoreText(Score);
            }
        }

        private IEnumerator WaitForLevelChange()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(previousLevel+1);
        }

        public void SetScore(int score)
        {
            Score += score;
        }
    }
}