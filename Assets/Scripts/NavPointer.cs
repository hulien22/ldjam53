using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NavPointer : MonoBehaviour
{
    public GameObject pointer;
    public SpriteRenderer sprite;
    public TextMeshPro distanceText;
    public Rocket rocket;
    public Transform target;
    // Don't show text under this distance.
    public float minDistance;

    public float zIndex;

    public float distanceFromRocket;

    public float minScale;
    public float maxScale;

    private void FixedUpdate()
    {
        if (!target)
        {
            HideSprite();
            return;
        }

        // Get direction.
        Vector3 rPos3 = rocket.GetRocketPosition();
        Vector2 vec = target.position - rPos3;
        var distance = vec.magnitude;
        var direction = vec.normalized;

        // pointer.transform.LookAt(target.position, Vector3.forward);
        sprite.transform.up = direction;
        Vector2 rPos2 = rPos3;
        Vector3 newPos = rPos2 + distanceFromRocket * direction;
        newPos.z = zIndex;
        pointer.transform.position = newPos;
        if (distance > minDistance)
        {
            distanceText.text = distance.ToString("0.00") + " m";
            ShowSprite();
        }
        else
        {
            HideSprite();
        }

    }

    void HideSprite()
    {
        if (sprite.enabled)
        {
            sprite.enabled = false;
            distanceText.enabled = false;
        }
    }

    void ShowSprite()
    {
        if (!sprite.enabled)
        {
            sprite.enabled = true;
            distanceText.enabled = true;
        }

    }

}
