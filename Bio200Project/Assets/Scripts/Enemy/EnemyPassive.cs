using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassive : Enemy
{
    private SpriteRenderer enemySprite = null;

    [SerializeField] private float idleSpeed = 3f;

    //Follows idle movement if false. Follows player if true
    private bool isHostile = false;

    private Vector3 direction = new Vector3();

    private Vector3 destination = new Vector3();
    private Vector3 start = new Vector3();

    private void OnEnable()
    {
        PlayerHide.PassifyEnemies += TurnOffHostility;
        DeathManager.DeathOccurred += PlayerDeath;
    }
    private void OnDisable()
    {
        if (PlayerHide.PassifyEnemies != null)
            PlayerHide.PassifyEnemies -= TurnOffHostility;
        if(DeathManager.DeathOccurred != null)
            DeathManager.DeathOccurred -= PlayerDeath;
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
        {
            Vector3 offset = (destination.x == start.x && transform.position.x > start.x) ? new Vector3(-1, 0, 0) :
                (destination.x == start.x && transform.position.x < start.x) ? new Vector3(1, 0, 0) :
                (destination.y == start.y && transform.position.y > start.y) ? new Vector3(0, -1, 0) :
                (destination.y == start.y && transform.position.y < start.y) ? new Vector3(0, 1, 0) : Vector3.zero;

            moveSpeed = idleSpeed * (direction + offset) * Time.fixedDeltaTime;
        }
            

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
        if(value)
            AudioManager.singleton.Src.PlayOneShot(enemySpeak, 0.1f);
    }

    private void PlayerDeath()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
