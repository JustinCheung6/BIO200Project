using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    //Movement
    private float hor = 0f, ver = 0f;

    //Animation
    private Animator anim = null;
    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        //Get input from player
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
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
}
