using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private static DeathManager _singleton = null;
    public static DeathManager singleton { get => _singleton; }

    public delegate void OnDeath();
    public static OnDeath DeathOccurred;

    private Canvas deathMenu;

    private void Awake()
    {
        if (_singleton == null)
            _singleton = this;
        else if (_singleton != this)
            Debug.Log("Multiple instances of DeathManager Found");
    }

    private void Start()
    {
        deathMenu = GetComponentInChildren<Canvas>();
        deathMenu.gameObject.SetActive(false);
    }

    public void DeathOccurrence()
    {
        if (DeathOccurred != null)
            DeathOccurred();

        deathMenu.gameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
