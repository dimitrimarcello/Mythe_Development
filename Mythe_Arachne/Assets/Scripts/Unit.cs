using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [SerializeField]
    private AllUnits allUnits;

    private Vector3 ToPosition;

    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float maxDistance;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Unit nearestUnit = GetNearestUnit();

        if (nearestUnit == null) return;

        transform.localPosition = Vector2.MoveTowards(transform.position, nearestUnit.transform.position, maxDistance);

    }

    public Unit GetNearestUnit()
    {
        Unit nearestUnit = null;
        
        foreach (Unit unit in allUnits.unitList)
        {
            if (unit == this) continue;

            if (nearestUnit == null)
            {
                nearestUnit = unit;

                continue;
            }

            if (Vector2.Distance(unit.transform.position, transform.position) < Vector2.Distance(nearestUnit.transform.position, transform.position))
            {
                nearestUnit = unit;
            }
        }

        return nearestUnit;
    }


    private void WrapAround(ref Vector3 vector, float min, float max)
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
