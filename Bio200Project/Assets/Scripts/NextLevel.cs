using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    /*
    private void Update()
    {
        if (Input.GetKeyDown("="))
        {
            if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Input.GetKeyDown("-"))
        {
            if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex - 1) != null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Player")
        {
            if(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
               SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
