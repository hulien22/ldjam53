using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxStrength;
    private Material shader;

    private void Start()
    {
        shader = GetComponent<Renderer>().material;
    }
    private void FixedUpdate()
    {
        // Debug.Log(shader.GetVector("_Offset"));
        var offset = shader.GetVector("_Offset");
        offset.x = transform.position.x * parallaxStrength;
        offset.y = transform.position.y * parallaxStrength;
        shader.SetVector("_Offset", offset);
    }
}
