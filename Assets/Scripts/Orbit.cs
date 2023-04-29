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
            transform.RotateAround(new Vector3(orbitCenter.x, orbitCenter.y, transform.position.z), Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}
