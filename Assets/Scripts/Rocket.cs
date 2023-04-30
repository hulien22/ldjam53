using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    public Rigidbody2D rocketBody;

    public CircleCollider2D planetDetector;
    public ParticleSystem particles;

    public int health;

    public float thrustModifier;
    public float turnModifier;

    public float rayCastLength = 0.5f;
    public float landingTime;

    public Transform defaultParent;

    [SerializeField]
    private InputActionReference thrust, turn;

    private float lastLandTime = 0;
    private bool landed;

    private int rayCastLayerMask;

    public float minimumDamageThreshold;
    public float minimumDamageThresholdLandingGear;
    public float collisionDamageScale;

    public float gravModifier = 1;
    // MAX SPEED?

    // Start is called before the first frame update
    void Start()
    {
        rayCastLayerMask = LayerMask.GetMask("Planets");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float thrustInput = thrust.action.ReadValue<float>();

        // Check landing
        Vector2 rayCastDir = -Vector2.up;
        rayCastDir = Quaternion.AngleAxis(rocketBody.rotation, Vector3.forward) * rayCastDir;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayCastDir, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(transform.position, rayCastDir * rayCastLength, Color.blue);
        if (hit)
        {
            if (!landed && thrustInput <= 0)
            {

                if (lastLandTime == 0)
                {
                    // haven't landed yet, start checking if we are landing
                    lastLandTime = Time.time;
                }
                else if (Time.time - lastLandTime > landingTime)
                {
                    // TODO check rotational speed instead of velocity?
                    landed = true;
                    lastLandTime = 0;
                    Debug.Log("landed on : " + hit.collider.gameObject);
                    // stick to parent.
                    // TODO still need this?
                    // transform.SetParent(hit.collider.gameObject.transform);
                }
            }
        }
        else
        {
            lastLandTime = 0;
            if (landed)
            {
                landed = false;
                // set back to root node
                // transform.SetParent(null);
            }
        }

        if (Mathf.Abs(thrustInput) > 0.1)
        {
            Vector2 velocity = Vector2.up;
            velocity *= thrustInput;
            velocity = Quaternion.AngleAxis(rocketBody.rotation, Vector3.forward) * velocity;

            // When landed, only allow thrust
            if (!landed || thrustInput > 0)
            {
                rocketBody.AddForce(velocity * thrustModifier);
            }
            if (thrustInput > 0)
            {
                particles.Emit(1);
            }
        }
        // If landed kill all speed.
        if (landed && thrustInput <= 0)
        {
            rocketBody.velocity = Vector2.zero;
            rocketBody.angularVelocity = 0;
        }
        // Space to stop/stall?


        if (!landed)
        {
            ApplyTorque();
            ApplyPlanetGravity();
        }
        // Debug.DrawLine(rocketBody.transform.position, Vector3.zero, Color.red, 2.5f);
    }

    private void ApplyTorque()
    {
        float turnInput = turn.action.ReadValue<float>();
        if (Mathf.Abs(turnInput) > 0.1)
        {
            rocketBody.AddTorque(turnInput * turnModifier);
        }
        else
        {
            // angular damping?
        }
    }

    private void ApplyPlanetGravity()
    {
        // Planet gravity
        List<Collider2D> colliders = new List<Collider2D>();
        if (planetDetector.GetContacts(colliders) > 0)
        {
            float minDistance = -1;
            Collider2D closestPlanet = null;
            foreach (var collider in colliders)
            {
                // collider.gameObject
                // Debug.Log(collider.gameObject);
                Planet planet = collider.gameObject.GetComponent<Planet>();
                Vector2 direction = collider.transform.position - rocketBody.transform.position;
                Vector2 velocity = direction.normalized;
                // g = GM / r^2
                velocity *= planet.gravityStrength / Mathf.Pow(direction.magnitude, 2);
                rocketBody.AddForce(velocity * gravModifier);
                // Debug.Log(collider.gameObject.name + " : " + (planet.gravityStrength / Mathf.Pow(direction.magnitude, 2)));
                Vector2 rb = rocketBody.transform.position;
                Debug.DrawLine(rb, 100 * velocity + rb, Color.red, 2.5f, false);

                if (direction.magnitude < planet.atmosphereDistance)
                {
                    if (minDistance < 0 || direction.magnitude < minDistance)
                    {
                        closestPlanet = collider;
                        minDistance = direction.magnitude;
                    }
                }
            }
            if (minDistance > 0)
            {
                // Debug.Log("setting parent to " + closestPlanet.gameObject);
                transform.SetParent(closestPlanet.transform);
            }
            else
            {
                transform.SetParent(null);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        Vector2 impactVelocity = other.relativeVelocity;
        float magnitude = 0f;

        Planet planet = other.gameObject.GetComponent<Planet>();
        if (planet && lastLandTime > 0)
        {
            magnitude = Mathf.Max(0f, impactVelocity.sqrMagnitude - minimumDamageThresholdLandingGear);
        }
        else
        {
            magnitude = Mathf.Max(0f, impactVelocity.sqrMagnitude - minimumDamageThreshold);
        }

        float damage = magnitude * collisionDamageScale;

        // var v = Vector2.Dot(other.contacts[0].normal, other.relativeVelocity);
        Debug.Log("Collision Detected. Damage taken: " + damage);
    }

}
