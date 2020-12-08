using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleFunctions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
