using UnityEngine;
using System.Collections;

public class MoveØvelse5B : MonoBehaviour 
{
	public Transform otherObject;
	public float speedFactor = 0.2f;
	public Vector3 speed = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
//	public Vector3 accel = Vector3.zero;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 toOtherObject = otherObject.transform.position - transform.position;

		if (toOtherObject.magnitude > 5f) 
		{
			toOtherObject.Normalize ();

//			speed = speed + acceleration * Time.deltaTime;

			speed = speed + acceleration * Time.deltaTime;

//			acceleration = acceleration + accel * Time.deltaTime;

//			speed = toOtherObject + acceleration;

			transform.position = transform.position + speed * Time.deltaTime;
		}

//		Meget rykkende, underlig bevægelse. Objectet får hele tiden mere og mere fart.
//		Vector3 toOtherObject = otherObject.transform.position - transform.position;
////		
//		if (toOtherObject.magnitude > 1f) 
//		{
//			toOtherObject.Normalize ();
//			
//			//			speed = speed + acceleration * Time.deltaTime;
//			
//			//			speed = toOtherObject * speedFactor;
//			
//			acceleration = acceleration + accel * Time.deltaTime;
//			
//			speed = toOtherObject + acceleration;
//			
//			transform.position = transform.position + speed * Time.deltaTime;
//		}
	}
}
