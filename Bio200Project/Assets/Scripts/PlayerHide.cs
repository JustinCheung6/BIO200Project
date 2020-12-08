using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    //Player's collider for checking enemy collisions
    private Collider2D enemyCollider = null;

    //Enemies
    public delegate void Passify();
    public static Passify PassifyEnemies;

    private void Start()
    {
        enemyCollider = PlayerMovement.singleton.gameObject.GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Grass"))
        {
            PlayerMovement.singleton.Hidden = true;
            enemyCollider.enabled = false;
            if(PassifyEnemies != null)
                PassifyEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Grass"))
        {
            PlayerMovement.singleton.Hidden = false;
            enemyCollider.enabled = true;
        }
    }
}
