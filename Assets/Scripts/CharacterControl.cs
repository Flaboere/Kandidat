using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{

	public Vector3 force;
	public float direction;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		direction = (Input.GetAxis("Horizontal")) * 10;

		if (Input.GetAxis ("Horizontal")> 0.001)
		{
			rigidbody.AddForce (transform.forward * direction);
		}
		if (Input.GetAxis ("Horizontal")< -0.001)
		{
			rigidbody.AddForce (force * direction);
		}

		print (direction);
	}

}
