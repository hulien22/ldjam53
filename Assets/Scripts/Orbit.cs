using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public float orbitSpeed;
    public Vector2 orbitCenter;

    private void Update()
    {
        if (orbitSpeed != 0)
        {
            transform.RotateAround(new Vector3(orbitCenter.x, orbitCenter.y, transform.position.z), new Vector3(0, 0, 1), orbitSpeed * Time.deltaTime);
        }
    }
}
