using UnityEngine;
using System.Collections;

public class Move5Jeppe : MonoBehaviour {

	public float timer;
	public Vector3 step;
	
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
			transform.position = transform.position + step;
			timer = 1f;
		}
//		Debug.Log (Time.deltaTime);
	}
}
