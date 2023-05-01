using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    public Rigidbody2D rocketBody;

    public CircleCollider2D planetDetector;
    public ParticleSystem particles;
    public ParticleSystem downParticles;
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;

    public float maxHealth;
    public float health;
    public bool immuneToSun;
    public float maxFuel;
    public float fuel;
    public bool sensor;

    public float thrustModifier;
    public float downThrustModifier;
    public float turnModifier;

    public float turnDamping;

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

    public float atmosMod = 1;

    private bool reset = false;


    public Vector3 previousPosition;
    private Vector2 worldVelocity;

    private Vector3 previousRelativePosition = Vector3.zero;
    // MAX SPEED?

    // Start is called before the first frame update
    void Start()
    {
        immuneToSun = false;
        rayCastLayerMask = LayerMask.GetMask("Planets");
        // GlobalState.AddKnownLocation(GameObject.Find("Terrus"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reset)
        {
            rocketBody.velocity = Vector2.zero;
            rocketBody.angularVelocity = 0;
            previousPosition = GetRocketPosition();
            previousRelativePosition = Vector3.zero;
            landed = false;
            reset = false;
            return;
        }
        worldVelocity = (GetRocketPosition() - previousPosition) / Time.deltaTime;
        // Debug.Log(worldVelocity + " | " + rocketBody.velocity + " | " + transform.parent);
        previousPosition = GetRocketPosition();

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
                    GlobalState.AddKnownLocation(hit.collider.gameObject);
                    GlobalState.instance.lastPlanetVisited = hit.collider.gameObject.GetComponent<Planet>();
                    DialogManager.Instance.StartDialog(GlobalState.instance.lastPlanetVisited.location);

                    fuel = maxFuel;
                    health = maxHealth;
                    GlobalState.instance.fuelBar.SetVal(0);
                    GlobalState.instance.healthBar.SetVal(0);
                    // TODO disable controls!
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
                if (fuel > 0)
                {
                    if (thrustInput > 0)
                    {
                        rocketBody.AddForce(velocity * thrustModifier);
                        particles.Emit(1);
                    }
                    else
                    {
                        rocketBody.AddForce(velocity * downThrustModifier);
                        downParticles.Emit(1);
                    }

                    fuel -= 0.1f;
                    GlobalState.instance.fuelBar.SetVal(maxFuel - fuel);
                }
                Debug.Log("Fuel: " + fuel);
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
        // Debug.DrawLine(GetRocketPosition(), Vector3.zero, Color.red, 2.5f);
    }

    private void ApplyTorque()
    {
        float turnInput = turn.action.ReadValue<float>();
        if (Mathf.Abs(turnInput) > 0.1)
        {
            rocketBody.angularDrag = 0;
            rocketBody.AddTorque(turnInput * turnModifier);
            if (turnInput < 0)
            {
                leftParticles.Emit(1);
            }
            else
            {
                rightParticles.Emit(1);
            }
        }
        else
        {
            // up angular damping when not turning
            rocketBody.angularDrag = turnDamping;
        }
    }

    private void ApplyPlanetGravity()
    {
        // Planet gravity
        List<Collider2D> colliders = new List<Collider2D>();
        if (planetDetector.GetContacts(colliders) > 0)
        {
            Planet closestPlanet = null;
            float minAtmosphereDistance = -1;
            foreach (var collider in colliders)
            {
                // collider.gameObject
                // Debug.Log(collider.gameObject);
                Planet planet = collider.gameObject.GetComponent<Planet>();
                Vector2 direction = collider.transform.position - GetRocketPosition();
                Vector2 velocity = direction.normalized;
                // g = GM / r^2
                velocity *= planet.gravityStrength / Mathf.Pow(direction.magnitude, 2);
                rocketBody.AddForce(velocity * gravModifier);
                // Debug.Log(collider.gameObject.name + " : " + (planet.gravityStrength / Mathf.Pow(direction.magnitude, 2)));
                Vector2 rb = GetRocketPosition();
                Debug.DrawLine(rb, 100 * velocity + rb, Color.red, 2.5f, false);

                if (!closestPlanet || direction.magnitude - planet.atmosphereDistance < minAtmosphereDistance)
                {
                    closestPlanet = planet;
                    minAtmosphereDistance = direction.magnitude - planet.atmosphereDistance;
                }
            }
            if (closestPlanet && minAtmosphereDistance <= 0)
            {
                // Entered atmosphere.
                if (transform.parent != closestPlanet.transform)
                {
                    // Debug.Log("setting parent to " + closestPlanet.gameObject);
                    transform.SetParent(closestPlanet.transform);

                    if (previousRelativePosition.magnitude > 0)
                    {
                        Vector3 relativePosn = GetRocketPosition() - closestPlanet.transform.position;
                        Vector2 relativeVelocity = (relativePosn - previousRelativePosition) / Time.deltaTime;
                        rocketBody.velocity = relativeVelocity * atmosMod;
                    }
                }
                // lastWorldVelocity = closestPlanet.worldVelocity;
            }
            else
            {
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                    rocketBody.velocity = worldVelocity * atmosMod;
                    // lastWorldVelocity = Vector2.zero;
                }
                // bug with one frame escape?
                if (closestPlanet)
                {
                    // Close to a planet, calculate relative posn.
                    previousRelativePosition = GetRocketPosition() - closestPlanet.transform.position;
                }
            }
            // Debug.Log(rocketBody.velocity);
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
        health -= damage;
        GlobalState.instance.healthBar.SetVal(maxHealth - health);

        // var v = Vector2.Dot(other.contacts[0].normal, other.relativeVelocity);
        Debug.Log("Collision Detected. Damage taken: " + damage);
    }

    public Vector3 GetRocketPosition()
    {
        // Vector3 com = rocketBody.centerOfMass;
        // return rocketBody.transform.position + com;
        return rocketBody.worldCenterOfMass;
    }

    public void ResetToLastPlanet()
    {
        Debug.Log("RESET SHIP");
        var lastPlanet = GlobalState.instance.lastPlanetVisited;

        // move to the planet and set relative position/rotation
        transform.SetParent(lastPlanet.transform);
        transform.localPosition = Vector3.up * (lastPlanet.planetRadius + 2);
        transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

        // nullify all physics
        rocketBody.velocity = Vector2.zero;
        rocketBody.angularVelocity = 0;
        previousPosition = GetRocketPosition();
        previousRelativePosition = Vector3.zero;
        landed = false;

        // Refuel + reheal
        fuel = maxFuel;
        health = maxHealth;
        GlobalState.instance.fuelBar.SetVal(0);
        GlobalState.instance.healthBar.SetVal(0);

        // set flag for first physics tick
        reset = true;
    }

}
