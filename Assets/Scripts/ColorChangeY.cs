using UnityEngine;
using System.Collections;

public class ColorChangeY : MonoBehaviour 
{

	public Color color;
	public Camera camera;
//	float cameraY;
	float cameraX;
//	float rColor = 10;
//	float gColor;
//	float bColor;
//	float aColor = 0;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
//		cameraY = camera.gameObject.transform.position.y;
		cameraX = camera.gameObject.transform.position.x * -1;
		color = new Color (1,0,0,1);
		renderer.material.color = color;

		Debug.Log (cameraX);
	}
}
