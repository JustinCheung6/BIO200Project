using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvasive : Enemy
{
    private void Start()
    {
        AudioManager.singleton.Src.PlayOneShot(enemySpeak, 0.1f);
    }
    private void FixedUpdate()
    {
        transform.position += MoveToPlayer();
    }
}
