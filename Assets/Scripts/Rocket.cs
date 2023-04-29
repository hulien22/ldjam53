using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    public Rigidbody2D rocketBody;

    public CircleCollider2D planetDetector;
    public ParticleSystem particles;

    public float thrustModifier;
    public float turnModifier;


    [SerializeField]
    private InputActionReference thrust, turn;
    // MAX SPEED?

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float thrustInput = thrust.action.ReadValue<float>();
        if (Mathf.Abs(thrustInput) > 0.1)
        {
            Vector2 velocity = Vector2.up;
            velocity *= thrustInput;
            velocity = Quaternion.AngleAxis(rocketBody.rotation, Vector3.forward) * velocity;
            rocketBody.AddForce(velocity * thrustModifier);
            if (thrustInput > 0)
            {
                particles.Emit(1);
            }
        }

        float turnInput = turn.action.ReadValue<float>();
        if (Mathf.Abs(turnInput) > 0.1)
        {
            rocketBody.AddTorque(turnInput * turnModifier);
        }
        // Space to stop/stall?

        // Planet gravity
        List<Collider2D> colliders = new List<Collider2D>();
        if (planetDetector.GetContacts(colliders) > 0)
        {
            foreach (var collider in colliders)
            {
                // collider.gameObject
                // Debug.Log(collider.gameObject);
                Planet planet = collider.gameObject.GetComponent<Planet>();
                Vector2 direction = collider.transform.position - rocketBody.transform.position;
                Vector2 velocity = direction.normalized;
                // g = GM / r^2
                velocity *= planet.gravityStrength / Mathf.Pow(direction.magnitude, 2);
                rocketBody.AddForce(velocity);
                Debug.Log(collider.gameObject.name + " : " + (planet.gravityStrength / Mathf.Pow(direction.magnitude, 2)));
                Vector2 rb = rocketBody.transform.position;
                Debug.DrawLine(rb, 100 * velocity + rb, Color.red, 2.5f, false);
            }
        }
        // Debug.DrawLine(rocketBody.transform.position, Vector3.zero, Color.red, 2.5f);
    }

}
