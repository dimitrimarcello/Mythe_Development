using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawObjects : MonoBehaviour {

	public GameObject rope;
	public float maxDrawLenght;
	public float zAxis;
	public int amountOfDraws = 3;
	private List<GameObject> drawnObjects = new List<GameObject>();
	private GameObject currentDraw;
	private Vector2 mouseStart;

	void Update()
	{
		if(Input.GetMouseButtonDown(0)){
			mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			StartDrawing();
		}
        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
		if(currentDraw != null){
			//rotation of object that is drawn
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 dist = mousePosition - currentDraw.transform.position;
			float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
			currentDraw.transform.eulerAngles = new Vector3(0,0,angle);
			//scale of drawn object
			float scale = Mathf.Clamp(Vector2.Distance(mousePosition, mouseStart), 1, maxDrawLenght);
			currentDraw.GetComponent<SpriteRenderer>().size = new Vector2(-scale, 2.531775f);
            currentDraw.GetComponent<BoxCollider2D>().size = new Vector2(scale, 0.08949333f);
            currentDraw.GetComponent<BoxCollider2D>().offset = new Vector2(scale/2, 0.005293125f);
		}
		if(drawnObjects.Count > amountOfDraws){
			Destroy(drawnObjects[0]);
			drawnObjects.RemoveAt(0);
		}
	}

	private void StartDrawing(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = zAxis;
		currentDraw = Instantiate(rope, mousePosition, transform.rotation);
		currentDraw.transform.eulerAngles = new Vector3(0,0,90);
	}

	private void StopDrawing(){
		drawnObjects.Add(currentDraw);
		currentDraw = null;
	}

}
