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

    // so we can point to that instead.
    public float planetRadius;

    public float zIndex;

    public float distanceFromRocket;
    public float spriteRadius;
    public float textMod;


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
        var distance = vec.magnitude - planetRadius - spriteRadius;
        var direction = vec.normalized;

        // pointer.transform.LookAt(target.position, Vector3.forward);
        sprite.transform.up = direction;
        Vector2 rPos2 = rPos3;

        Vector3 newPos = rPos2 + Mathf.Min(distance, distanceFromRocket) * direction;
        newPos.z = zIndex;
        pointer.transform.position = newPos;
        if (distance > 0)
        {
            distanceText.text = (distance + textMod).ToString("0.00") + " m";
            ShowSprite();
        }
        else
        {
            // distanceText.text = distance.ToString("0.00") + " m";
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
