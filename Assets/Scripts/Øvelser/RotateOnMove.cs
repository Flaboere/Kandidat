using UnityEngine;
using System.Collections;

public class RotateOnMove : MonoBehaviour 
{
	float inputDir;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		inputDir = Input.GetAxis ("Horizontal");
	}
}
