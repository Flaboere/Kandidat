using UnityEngine;
using System.Collections;

public class MoveØvelser4A : MonoBehaviour 
{
	public Vector3 speed = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
//		speed = speed + acceleration * Time.deltaTime;

		transform.position = transform.position + speed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			acceleration = acceleration + new Vector3(1,1,1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			acceleration = acceleration + new Vector3(-1,-1,-1);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			speed = speed + new Vector3(1,1,1);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			speed = speed + new Vector3(-1,-1,-1);
		}

	}

	
}
