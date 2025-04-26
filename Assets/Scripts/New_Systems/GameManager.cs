using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace New_Systems
{
    public class GameManager : MonoBehaviour
    {
        private Animator _animator;
        public int previousLevel;
        private bool hasLoaded;
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            print(SceneManager.GetActiveScene().name);
            
            if (SceneManager.GetActiveScene().name == "Level 6")
            {
                Destroy(gameObject);
            }
            else if (SceneManager.GetActiveScene().name == "LevelClear" && hasLoaded)
            {
                _animator = FindFirstObjectByType<Animator>();
                _animator.SetFloat("Level", previousLevel);
                StartCoroutine(nameof(WaitForLevelChange));
                hasLoaded = false;
            }
            else
            {
               previousLevel = SceneManager.GetActiveScene().buildIndex; 
               hasLoaded = true;
            }
        }

        private IEnumerator WaitForLevelChange()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(previousLevel+1);
        }
    }
}