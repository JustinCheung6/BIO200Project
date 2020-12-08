using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvasive : Enemy
{

    private void FixedUpdate()
    {
        transform.position += MoveToPlayer();
    }
}
