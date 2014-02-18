using UnityEngine;
using System.Collections;

public class MoveØvelse3B : MonoBehaviour 
{
	public float timer;
	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3 (0f, 0f, 0f);
		timer = 1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer = timer - Time.deltaTime;

			if (timer < 0f) 
		{
			transform.position = transform.position + new Vector3 (0f, 1f, 0f);

			timer = 1f;
		}
	}
}
