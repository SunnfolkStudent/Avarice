using System;
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
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}