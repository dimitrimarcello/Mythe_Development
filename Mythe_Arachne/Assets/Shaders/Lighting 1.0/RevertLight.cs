using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RevertLight : MonoBehaviour {

	public Material LightingMat;
	public Color baseLight = new Color(1,1,1,1);
	public Color transformLight;
	[Range(0,1)]
	public float srcPower;
	[Range(0,10)]
	public float srcBorder;
	private LightSource lightSource;
	private Camera toPoint;
	private Color current;

	void Start()
	{
		toPoint = GetComponent<Camera>();
		lightSource = GameObject.FindObjectOfType<LightSource>();
	} 

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, LightingMat);
	}

	void FixedUpdate()
	{
		current = Color.Lerp(baseLight, transformLight, srcPower);
        Vector3 convertedPos = toPoint.WorldToViewportPoint(lightSource.getPos);
		LightingMat.SetVector("_LightSource", convertedPos);
		LightingMat.SetFloat("_LightStrenght", srcPower * srcBorder);
		LightingMat.SetColor("_DarkMulti", current);
	}

}
