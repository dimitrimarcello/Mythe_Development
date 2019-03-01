using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour {

    [SerializeField]
    private Transform unitObject;

    [Range(0, 200), SerializeField]
    private int unitAmount;

    [Range(0, 50), SerializeField]
    private float spawnRadius;

    [SerializeField]
    private Transform playerTransform;

    public List<Unit> unitList = new List<Unit>();


    // Use this for initialization
    void Start () {

        Spawn(unitObject, unitAmount);

        unitList.AddRange(FindObjectsOfType<Unit>());

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn(Transform prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0);

            position += playerTransform.position;

            Unit unit = Instantiate(unitObject, position, Quaternion.identity).GetComponent<Unit>();

            unit.transform.SetParent(transform);
        }
    }

    public List<Unit> GetNeighbors(Unit unit, float radius)
    {
        List<Unit> neighborsFound = new List<Unit>();

        foreach (var otherUnit in unitList)
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
}
