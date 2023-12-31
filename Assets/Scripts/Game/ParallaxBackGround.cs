using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField] private float parallaxFactor;
    
    private Vector3 initialPosition;
    private SpriteRenderer srRenderer;
    private GameStateChecker gC;

    private void Awake()
    {
        gC = GetComponent<GameStateChecker>();
    }

    private void Start()
    {
        srRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if(!gC.isPaused)
            transform.Translate(Vector2.left * Time.deltaTime * parallaxFactor);

        if (transform.position.x <= initialPosition.x - srRenderer.bounds.size.x)
            transform.position = new Vector2(transform.position.x + srRenderer.bounds.size.x, transform.position.y);
        
    }

}

