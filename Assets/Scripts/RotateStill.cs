using UnityEngine;
using System.Collections;

public class RotateStill : MonoBehaviour 
{
	// Use this for initialization
	Quaternion rotation;
	
	void Awake()
	{
		rotation = transform.rotation;
	}
	
	void FixedUpdate()
	{
		transform.rotation = rotation;
	}
}
