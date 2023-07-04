using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{

    public static event UnityAction playerCollectedCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollectedCoin?.Invoke();
            StartCoroutine(coinCollected());
        }
    }

    private IEnumerator coinCollected()
    {
        float animationTIme = 2f;
        float timer = 0;

        while (timer < animationTIme)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 5f);
            timer += Time.deltaTime;
            yield return null;
        }
    }

}
