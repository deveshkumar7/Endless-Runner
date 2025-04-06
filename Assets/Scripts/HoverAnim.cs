using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHover : MonoBehaviour
{
    public float hoverHeight = 0.15f; // Adjust as needed
    public float hoverSpeed = 2f; // Adjust as needed
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate target position for hovering
        Vector3 targetPosition = originalPosition + new Vector3(0f, Mathf.Sin(Time.time * hoverSpeed) * hoverHeight, 0f);

        // Smoothly move towards the target position using Translate
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * hoverSpeed);
    }
}

