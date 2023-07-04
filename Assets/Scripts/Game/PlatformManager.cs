using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab; // Reference to the platform prefab
    public float platformSpawnOffset = 10f; // Distance between each spawned platform
    public int initialPlatforms = 5; // Number of platforms to generate at the start

    private Transform playerTransform; // Reference to the player's transform
    private List<GameObject> platforms = new List<GameObject>(); // List to track the spawned platforms

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Tag your player object as "Player"

        // Spawn initial platforms
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        // Check if the player has moved beyond a certain distance
        if (playerTransform.position.x > platforms[0].transform.position.x + platformSpawnOffset)
        {
            RecyclePlatform(platforms[0]); // Remove the first platform from the list
            SpawnPlatform(); // Spawn a new platform at the end
        }
    }

    private void SpawnPlatform()
    {
        // Calculate the position for the new platform
        Vector3 spawnPosition = Vector3.zero;
        if (platforms.Count > 0)
        {
            spawnPosition = platforms[platforms.Count - 1].transform.position + new Vector3(platformSpawnOffset, 0f, 0f);
        }

        // Instantiate the new platform
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, transform);
        platforms.Add(newPlatform); // Add the new platform to the list
    }

    private void RecyclePlatform(GameObject platform)
    {
        platforms.Remove(platform); // Remove the platform from the list
        Destroy(platform); // Destroy the platform game object
    }
}

