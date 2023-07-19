using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnMinInterval = 2f;
    [SerializeField] private float spawnMaxInterval = 4f;
    [SerializeField] private float deSpawnDistance = 10f;

    private List<GameObject> obstacles = new List<GameObject>();
    private GameStateChecker sC;
    private Transform playerTransform;
    private Animator animator;
    private float timer = 0f;

    private void Awake()
    {
        sC = GetComponent<GameStateChecker>();
        animator = GetComponent<Animator>();
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
            SpawnObstacle();
            timer = Random.Range(spawnMinInterval, spawnMaxInterval);
        }

        if(obstacles.Count > 0)
        {
            if (playerTransform.position.x > obstacles[0].transform.position.x + deSpawnDistance)
            {
                DespawnObstacle(obstacles[0]);
            }
        }
        
    }

    private void SpawnObstacle()
    {
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        obstacles.Add(newObstacle);
        spawnMinInterval = newObstacle.GetComponent<Obstacle>().obstacleCD;
        animator.Play("attack1");
    }

    private void DespawnObstacle(GameObject obstacle)
    {
        obstacles.Remove(obstacle);
        Destroy(obstacle);
    }

}

