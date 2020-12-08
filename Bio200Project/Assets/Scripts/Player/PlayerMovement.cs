using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement _singleton = null;
    public static PlayerMovement singleton { get => _singleton; }

    //Movement
    [SerializeField] private float moveSpeed = 5f;
    private float hor = 0f, ver = 0f;

    //Animation
    private Animator anim = null;
    private SpriteRenderer spriteRenderer = null;

    //Collision
    [SerializeField] private bool hidden = false;
    public bool Hidden { get => hidden; set => hidden = value; }

    //Other
    public bool playerDied = false;

    private void OnEnable()
    {
        DeathManager.DeathOccurred += KillPlayer;
    }
    private void OnDisable()
    {
        DeathManager.DeathOccurred -= KillPlayer;
    }
    private void Awake()
    {
        if (_singleton == null)
            _singleton = this;
        else if (_singleton != this)
            Debug.Log("Multiple PlayerMovement Scripts found");
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerDied)
            return;
        //Get input from player
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (playerDied)
            return;

        Vector3 inputMovement = new Vector3(hor, ver, 0) * moveSpeed;
        UpdateAnimation(hor, ver);

        transform.position += inputMovement * Time.fixedDeltaTime;
    }

    private void UpdateAnimation(float xMove, float yMove)
    {
        if (xMove != 0)
            spriteRenderer.flipX = (xMove < 0);

        anim.SetBool("Moving", (xMove != 0 || yMove != 0) );
    }
    
    private void KillPlayer()
    {
        playerDied = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
        anim.SetTrigger("DeD");
    }
}
