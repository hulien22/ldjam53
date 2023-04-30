using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float orbitSpeed;
    public Transform orbitCenter;

    private void FixedUpdate()
    {
        if (orbitSpeed != 0)
        {
            transform.RotateAround(new Vector3(orbitCenter.position.x, orbitCenter.position.y, transform.position.z), Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}
