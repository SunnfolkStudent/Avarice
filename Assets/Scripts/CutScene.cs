using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(GoToMainMenu));
    }

    public IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene("MainMenu");
    }
}