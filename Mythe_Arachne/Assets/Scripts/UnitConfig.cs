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
    [Range(0, 100)]
    public float wanderPriority;

    // Cohesion variables
    //  public float cohesionRadius;
    [Range(0, 100)]
    public float cohesionPriority;

    // Alignment variables
   // public float alignmentRadius;
   // public float alignmentPriority;

    // Separation variables
    public float separationRadius;
    [Range(0, 100)]
    public float separationPriority;

    // Follow variables
    [Range(0, 100)]
    public float followPriority;

    // Avoidance variables
    // public float avoidanceRadius;
    // public float avoidancePriority;
}
