using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaveSlice : MonoBehaviour {

	public List<KeyCode> possibleKeys = new List<KeyCode>();
	public TextMesh keyName;
	public float maximumSize = 5f;
	public float minimumSize = 1f;
	private SpriteRenderer setSize;
	private KeyCode choosenKey;
	private WeaveSpawner getData;
	private float activation = 0;
	private float finishTime = 5f;
	public bool disabled = false;

	void Start()
	{
		setSize = GetComponent<SpriteRenderer>();
		getData = FindObjectOfType<WeaveSpawner>();
		setSize.size = new Vector2(setSize.size.x, Random.Range(minimumSize, maximumSize));
		choosenKey = possibleKeys[Random.Range(0, possibleKeys.Count)];
		keyName.text = choosenKey.ToString();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.GetComponent<WeaveMachine>() != null && Input.GetKey(choosenKey)){
			activation += Time.deltaTime;
        Debug.Log(activation);
		}
	}

	void Update()
	{
		MoveDown();
		if(activation > 0.5f){
			disabled = true;
			Debug.Log("good!");
		}
	}

	private void MoveDown(){
		transform.Translate(-transform.up * Time.deltaTime * getData.dropSpeed);
	}

}
