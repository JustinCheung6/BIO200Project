using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hostileSpeed = 7f;

    protected Vector3 MoveToPlayer()
    {
        Vector3 pp = PlayerMovement.singleton.transform.position;

        Vector3 finalSpeed = new Vector3(0, 0, 0);

        float aTan = Mathf.Atan((pp.y - transform.position.y) / (pp.x - transform.position.x));

        float hypotenuse = hostileSpeed * Time.fixedDeltaTime;
        Vector2 direction = new Vector2((transform.position.x > pp.x) ? -1 : (transform.position.x < pp.x) ? 1 : 0,
    (transform.position.y > pp.y) ? -1 : (transform.position.y < pp.y) ? 1 : 0);

        return new Vector3(Mathf.Abs(Mathf.Cos(aTan) * hypotenuse) * direction.x, Mathf.Abs(Mathf.Sin(aTan) * hypotenuse) * direction.y, 0f);
    }
    protected void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            DeathManager.singleton.DeathOccurrence();
        }
    }
}
