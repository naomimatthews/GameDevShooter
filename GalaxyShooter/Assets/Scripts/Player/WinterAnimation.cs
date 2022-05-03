using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterAnimation : MonoBehaviour
{
    private Vector3 finalPosition;
    private Vector3 startPosition;

    private void Awake()
    {
        finalPosition = transform.position;
        startPosition = finalPosition - transform.right * 5.0f;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
    }

    private void OnEnable()
    {
        transform.position = startPosition;
    }
}
