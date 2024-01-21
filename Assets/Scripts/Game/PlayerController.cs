using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; 
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private float slideColliderReductiom = 2f;
    [SerializeField] private float damageAnimationTime = 0.5f;
    [SerializeField] private float airDashDuration = 0.5f;

    [SerializeField] private ParticleSystem particleSys;

    private KeyCode JumpKey = KeyCode.W;
    private KeyCode slideKey = KeyCode.S;
    private KeyCode airDashKey = KeyCode.D;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;
    private bool isSliding = false;
    private bool isDashing = false;
    private float slideTimer = 0f;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isPaused = false;
    }

    private void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKey(slideKey) && !isSliding && !isJumping)
            {
                StartSliding();
            }
            //airdash
            if (Input.GetKey(airDashKey) && isJumping && !isPaused && !isDashing)
            {
                StartAirDash();
            }
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
        if (!isPaused)
        {
            if (Input.GetKey(JumpKey) && !isJumping)
            {
                StartJumping();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            StopJumping();
        }
    }

    private void StartJumping()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animator.Play("jump");
        AudioManager.Instance.StartJumping();
        isJumping = true;
    }

    private void StopJumping()
    {
        isJumping = false;
        isDashing = false;
        PlayDefaultAnimation();
    }

    private void StartSliding()
    {
        isSliding = true;
        animator.Play("Slide");
        AudioManager.Instance.StartJumping();
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

    private void StartAirDash()
    {
        isDashing = true;
        rb.simulated = false;
        animator.Play("Slide");
        AudioManager.Instance.StartJumping();
        StartCoroutine(doAirDash());
    }

    private IEnumerator doAirDash()
    {
        float timer = 0;
        while (timer < airDashDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StopAirDash();
    }

    private void StopAirDash()
    {
        rb.velocity = Vector3.zero;
        rb.simulated = true;
    }

    private void PlayDefaultAnimation()
    {
        if (!isPaused)
        {
            animator.Play("Run");
            AudioManager.Instance.PlayRunningAudio();
        }
    }

    private void PlayerHit()
    {
        particleSys.Play();
        AudioManager.Instance.PlayerHit();
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


    private void GamePaused()
    {
        isPaused = true;
        animator.Play("Idle");
    }

    private void GameResumed()
    {
        isPaused = false;
        animator.Play("Run");
    }

    private void OnEnable()
    {
        Obstacle.playerHitObstacle += PlayerHit;
        GamePlayManager.gamePaused += GamePaused;
        GamePlayManager.gameResumed += GameResumed;
    }


    private void OnDisable()
    {
        Obstacle.playerHitObstacle -= PlayerHit;
        GamePlayManager.gamePaused -= GamePaused;
        GamePlayManager.gameResumed -= GameResumed;
    }

}
