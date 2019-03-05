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
    public float wanderPriority;

    // Cohesion variables
    public float cohesionRadius;
    public float cohesionPriority;

    // Alignment variables
    public float alignmentRadius;
    public float alignmentPriority;

    // Separation variables
    public float separationRadius;
    public float separationPriority;

    // Avoidance variables
    public float avoidanceRadius;
    public float avoidancePriority;
}
