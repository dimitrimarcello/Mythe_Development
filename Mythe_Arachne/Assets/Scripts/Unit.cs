using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;

    public AllUnits allUnits;
    public UnitConfig unitConfig;

    private Vector2 wanderTarget;

	// Use this for initialization
	void Start () {
        allUnits = FindObjectOfType<AllUnits>();
        unitConfig = FindObjectOfType<UnitConfig>();

        position = transform.position;
        velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
	}

    void Update()
    {
        acceleration = Wander();

        acceleration = Vector2.ClampMagnitude(acceleration, unitConfig.maxAcceleration);

        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, unitConfig.maxVelocity);

        position = position + velocity * Time.deltaTime;

        WrapAround(ref position, -allUnits.bounds, allUnits.bounds);

        transform.position = position;
    }

    protected Vector2 Wander()
    {
        float jitter = unitConfig.wanderJitter * Time.deltaTime;

        wanderTarget += new Vector2(RandomBinomial() * jitter, RandomBinomial() * jitter);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= unitConfig.wanderRadius;

        Vector2 targetInLocalSpace = wanderTarget + new Vector2(0, unitConfig.wanderDistance);
        Vector2 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= position;
        return targetInWorldSpace.normalized;
    }

    private void WrapAround(ref Vector2 vector, float min, float max) // Set Unit to other side of screen when he goed off screen
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
    }

    private float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
            value = min;
        else if (value < min)
            value = max;
        return value;
    }

    private float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }
}
