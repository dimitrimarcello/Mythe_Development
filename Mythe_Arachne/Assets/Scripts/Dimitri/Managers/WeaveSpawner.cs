using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaveSpawner : MonoBehaviour {

	public GameObject weaveTile;
	public float dropSpeed = 3f;
	public List<float> xSpawnPositions = new List<float>();
	public float yOffset = 0f;
	public int maxWeaveTiles = 3;
	private List<GameObject> currentSpawns = new List<GameObject>();

	void Update()
	{
		if(currentSpawns.Count < maxWeaveTiles){
			Spawn();
		}
  }

	private void Spawn(){
			Vector2 spawnPosition = new Vector2(xSpawnPositions[Random.Range(0, xSpawnPositions.Count)], yOffset);
			currentSpawns.Insert(0,Instantiate(weaveTile, spawnPosition, transform.rotation));
	}

	public void CleanList(){
		foreach (GameObject weavePiece in currentSpawns)
		{
				if(weavePiece == null){
					currentSpawns.Remove(weavePiece);
				}
		}
	}

}
