using UnityEngine;
using System.Collections;

public class ColorChangeTest : MonoBehaviour 
{

	public float yMax = 200;
	public float yMin = 0;
	
	public Color maxColor = Color.green;
	public Color minColor = Color.red;
	
	private Camera mainCam;
	private float camYPos;
	public float camYPosScaled;
	// Use this for initialization
	void Start () 
	{
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		camYPos = mainCam.transform.position.y;
		
		//vi skal  have camyposscaled  til at fluktuerer mellem 0 og 1 fordi det er hvad lerp tager
		camYPosScaled = (camYPos - yMin) / (yMax - yMin);
		
		this.renderer.material.color = Color.Lerp(minColor, maxColor, camYPosScaled);
	}
}