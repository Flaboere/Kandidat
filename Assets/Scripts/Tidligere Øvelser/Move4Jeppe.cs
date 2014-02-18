using UnityEngine;
using System.Collections;

public class Move4Jeppe : MonoBehaviour {

	public float timer;
	
	void Start () 
	{
	
		transform.position = new Vector3 (0f,0f,0f);
		timer = 1f;
	}
	
	void Update () 
	{
	
		timer = timer - Time.deltaTime;
		if (timer > 0)
		{
			transform.position = transform.position + new Vector3 (0f, 1f, 0f);
			timer = 1f;
		}
	}
}
