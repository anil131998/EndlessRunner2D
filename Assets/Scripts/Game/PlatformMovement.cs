using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float movespeed = 10f;

    void Update()
    {
        transform.Translate(Vector2.left * movespeed * Time.deltaTime);    
    }
}
