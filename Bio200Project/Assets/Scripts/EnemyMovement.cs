using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private SpriteRenderer enemySprite = null;

    [SerializeField] private float idleSpeed = 3f;
    [SerializeField] private float hostileSpeed = 7f;

    //Follows idle movement if false. Follows player if true
    private bool isHostile = false;

    private Vector3 direction = new Vector3();

    private Vector3 destination = new Vector3();
    private Vector3 start = new Vector3();

    private void OnEnable()
    {
        PlayerHide.PassifyEnemies += TurnOffHostility;
    }
    private void OnDisable()
    {
        if (PlayerHide.PassifyEnemies != null)
            PlayerHide.PassifyEnemies -= TurnOffHostility;
    }

    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();

        Transform pathEnd = GetComponentsInChildren<Transform>()[1];

        start = transform.position;
        destination = pathEnd.position;
        Destroy(pathEnd.gameObject);

        direction = (destination.x != start.x) ? ((start.x < destination.x) ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0)) :
            ((start.y < destination.y) ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0));

    }
    private void FixedUpdate()
    {
        Vector3 moveSpeed = new Vector3();

        if (isHostile)
            moveSpeed = MoveToPlayer();
        else
            moveSpeed = idleSpeed * direction * Time.fixedDeltaTime;

        enemySprite.flipX = (moveSpeed.x > 0) ? false : (moveSpeed.x < 0) ? true : enemySprite.flipX;

        transform.position += moveSpeed;

        if (!isHostile)
            ReorientEnemy();

    }
    private void ReorientEnemy()
    {
        if ( (direction.x > 0 && transform.position.x >= destination.x) || (direction.x < 0 && transform.position.x <= destination.x) || 
            (direction.y > 0 && transform.position.y >= destination.y) || (direction.y < 0 && transform.position.y <= destination.y) )
        {
            direction = -direction;
            transform.position = destination;
            destination = start;
            start = transform.position;
        }
    }

    private Vector3 MoveToPlayer()
    {
        Vector3 pp = PlayerMovement.singleton.transform.position;

        Vector3 finalSpeed = new Vector3(0, 0, 0);

        float aTan = Mathf.Atan((pp.y - transform.position.y) / (pp.x - transform.position.x));

        /*
        if(direction.x == 1 && direction.y == -1)
            aTan -= (Mathf.PI * 1.5f);
        else if(direction.x == -1 && direction.y == -1)
            aTan -= (Mathf.PI);
        else if (direction.x == -1 && direction.y == 1)
            aTan -= (Mathf.PI *0.5f);
        */
        float hypotenuse = hostileSpeed * Time.fixedDeltaTime;
        Vector2 direction = new Vector2((transform.position.x > pp.x) ? -1 : (transform.position.x < pp.x) ? 1 : 0,
    (transform.position.y > pp.y) ? -1 : (transform.position.y < pp.y) ? 1 : 0);

        return new Vector3(Mathf.Abs(Mathf.Cos(aTan) * hypotenuse) * direction.x, Mathf.Abs(Mathf.Sin(aTan) * hypotenuse) * direction.y, 0f);
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        if(c.gameObject.tag == "PlayerRadius" && !PlayerMovement.singleton.Hidden && !isHostile)
        {
            RaycastHit2D[] results = new RaycastHit2D[3];
            Physics2D.RaycastNonAlloc((Vector2)transform.position, (Vector2)c.transform.position - (Vector2)transform.position, results, 100f);
            if (results[1].collider.gameObject.tag == "Player" || results[0].collider.gameObject.tag == "Player")
            {
                SetHostility(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerRadius")
            SetHostility(false);
    }
    private void TurnOffHostility()
    {
        SetHostility(false);
    }
    private void SetHostility(bool value)
    {
        isHostile = value;
    }
}
