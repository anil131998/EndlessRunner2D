using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float cameraShakeDuration = 0.5f;
    [SerializeField] private float cameraShakeMagnitude = 0.1f;

    private void PlayerDamaged()
    {
        StartCoroutine(CameraShake());
    }

    public IEnumerator CameraShake()
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * cameraShakeMagnitude;
            float y = Random.Range(-1f, 1f) * cameraShakeMagnitude;

            transform.position = new Vector3(orignalPosition.x + x, orignalPosition.y - y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }

    private void OnEnable()
    {
        Obstacle.playerHitObstacle += PlayerDamaged;
    }
    private void OnDisable()
    {
        Obstacle.playerHitObstacle -= PlayerDamaged;
    }
}
