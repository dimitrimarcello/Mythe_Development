using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightManager : MonoBehaviour {

	public Material lightingMat;
	private Torch[] allSources = new Torch[100];
	private List<float> points = new List<float>(); 

	void Awake()
	{
		allSources = GameObject.FindObjectsOfType<Torch>();
	}

	void FixedUpdate()
	{
        lightingMat.SetFloatArray("_Points", SetPoints());
		lightingMat.SetInt("_Points_Lenght", allSources.Length);
	}

	private float[] SetPoints(){
		for(int i = 0; i < allSources.Length; i++){
            points.Insert(0,allSources[i].pos.y);
			points.Insert(0,allSources[i].pos.x);
		}
		return points.ToArray();
	}

}
