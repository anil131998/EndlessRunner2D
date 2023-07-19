using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public static event UnityAction playerHitObstacle;

    [SerializeField] private float obstacleCooldown;
    public float obstacleCD { get; private set; }

    private void Awake()
    {
        obstacleCD = obstacleCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHitObstacle?.Invoke();
            StartCoroutine(obstacleHit());
        }
    }

    private IEnumerator obstacleHit()
    {
        float animationTIme = 2f;
        float timer = 0;

        while (timer < animationTIme)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 4f);
            transform.Rotate(new Vector3(0, 0, 30f * Time.deltaTime ));
            timer += Time.deltaTime;
            yield return null;
        }
    }

}
