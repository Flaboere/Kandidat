using UnityEngine;
using System.Collections;

public class RotateStill : MonoBehaviour 
{
	// Use this for initialization
	Quaternion rotation;
	
	void Start()
	{
		rotation = transform.rotation;
	}
	
	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
