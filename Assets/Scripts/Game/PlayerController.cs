using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; 
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private float slideColliderReductiom = 2f;
    [SerializeField] private float damageAnimationTime = 0.5f;

    private KeyCode JumpKey = KeyCode.W;
    private KeyCode slideKey = KeyCode.S;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;
    private bool isSliding = false;
    private float slideTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check if the slide key is pressed and not currently sliding
        if (Input.GetKey(slideKey) && !isSliding)
        {
            StartSliding();
        }

        if (isSliding)
        {
            slideTimer += Time.deltaTime;

            if (slideTimer >= slideDuration)
            {
                StopSliding();
            }
        }
    }

    void FixedUpdate()
    {
        // Check if the character is jumping
        if ( Input.GetKey(JumpKey) && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.Play("jump");
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            PlayDefaultAnimation();
        }
    }

    
    private void StartSliding()
    {
        isSliding = true;
        animator.Play("Slide");
        slideTimer = 0f;
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / slideColliderReductiom);
        //animator.SetBool("IsSliding", true);
    }

    private void StopSliding()
    {
        isSliding = false;
        PlayDefaultAnimation();
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * slideColliderReductiom);
        //animator.SetBool("IsSliding", false);
    }

    private void PlayDefaultAnimation()
    {
        animator.Play("Run");
    }

    private void PlayerHit()
    {
        StartCoroutine(PlayerDamaged());
    }


    private IEnumerator PlayerDamaged()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageAnimationTime/4);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(damageAnimationTime / 4);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageAnimationTime / 4);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(damageAnimationTime / 4);
    }
    


    private void OnEnable()
    {
        Obstacle.playerHitObstacle += PlayerHit;
    }
    private void OnDisable()
    {
        Obstacle.playerHitObstacle -= PlayerHit;
    }

}
