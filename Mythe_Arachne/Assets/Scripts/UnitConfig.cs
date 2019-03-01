using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitConfig : MonoBehaviour{

    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    // Wander variables
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderpriority;

    // Cohesion variables
    public float cohesionRadius;
    public float alignmentPriority;

    // Avoidance variables
    public float avoidanceRadius;
    public float avoidancePriority;
}
