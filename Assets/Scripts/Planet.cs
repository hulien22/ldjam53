using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // TODO use a parent class for all gravity bodies?
    public float gravityStrength;
    public float atmosphereDistance;

    public Vector3 previousPosition;
    public Vector2 worldVelocity;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        worldVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
        // Debug.Log(worldVelocity);
    }
}
