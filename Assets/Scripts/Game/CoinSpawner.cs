using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnPoint;
    [SerializeField] private float spawnMaxInterval = 2f;
    [SerializeField] private float spawnMinInterval = 4f;
    [SerializeField] private float deSpawnDistance = 10f;

    private List<GameObject> coins = new List<GameObject>();
    private GameStateChecker sC;
    private Transform playerTransform;
    private float timer = 0f;

    private void Awake()
    {
        sC = GetComponent<GameStateChecker>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !sC.isPaused)
        {
            SpawnCoin();
            timer = Random.Range(spawnMinInterval, spawnMaxInterval);
        }

        if (coins.Count > 0)
        {
            if (playerTransform.position.x > coins[0].transform.position.x + deSpawnDistance)
            {
                DespawnCoin(coins[0]);
            }
        }

    }

    private void SpawnCoin()
    {
        GameObject newCoin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity, transform);
        coins.Add(newCoin);
    }
    private void DespawnCoin(GameObject Coin)
    {
        coins.Remove(Coin);
        Destroy(Coin);
    }

}
