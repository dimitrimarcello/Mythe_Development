using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour {

    public Transform unitPrefab;
    public int numberOfUnits;
    public List<Unit> units;
    public float bounds;
    public float spawnRadius;

    public Transform playerTransform;


    // Use this for initialization
    void Start () {

        units = new List<Unit>();

        Spawn(unitPrefab, numberOfUnits);

        units.AddRange(FindObjectsOfType<Unit>());

        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn(Transform prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(0, spawnRadius), 0);

            position += playerTransform.position;

            Unit unit = Instantiate(unitPrefab, position, Quaternion.identity).GetComponent<Unit>();

            unit.transform.SetParent(transform);
        }
    }

    public List<Unit> GetNeighbors(Unit unit, float radius)
    {
        List<Unit> neighborsFound = new List<Unit>();

        foreach (Unit otherUnit in units)
        {
            if (otherUnit == unit)
                continue;

            if (Vector2.Distance(unit.transform.position, otherUnit.transform.position) <= radius)
            {
                neighborsFound.Add(otherUnit);
            }
        }

        return neighborsFound;
    }

    public List<Unit> GetAllNeighbors() { return units; }
    
}
