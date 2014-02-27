using UnityEngine;
using System.Collections;

public class MoveØvelse5C : MonoBehaviour 
{
	public Transform otherObject;
	public bool moveFar;
	public bool moveClose;
//	public float speedFactor = 0.2f;
	public Vector3 speed = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
//	public Vector3 accel = Vector3.zero;
	// Use this for initialization
	void Start () 
	{
		moveFar = false;
		moveClose = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 toOtherObject = otherObject.transform.position - transform.position;

		acceleration = toOtherObject.normalized ;
		Debug.DrawLine(transform.position, transform.position + acceleration, Color.red);
		Debug.DrawLine(transform.position, transform.position + speed, Color.green);


		speed = speed + acceleration * Time.deltaTime;
		transform.position = transform.position + speed * Time.deltaTime;


		/*
		if (toOtherObject.magnitude > 6f) 
		{
			moveFar = true;
//			moveClose = false;
		}
		if (moveFar)
		{

			speed = speed + acceleration * Time.deltaTime;

			transform.position = transform.position + speed * Time.deltaTime;

		}
		if (toOtherObject.magnitude < 3f)
		{
			speed = speed * -Time.deltaTime;
		}
*/
//		if (toOtherObject.magnitude < 1f) 
//		{
//			moveClose = true;
//			moveFar = false;
//		
//		}
//		if (startMove = false)
//		{
//			speed = speed + acceleration * Time.deltaTime;
//			
//			transform.position = transform.position - speed * Time.deltaTime;
//			
//		}
//		if (toOtherObject.magnitude > 3f) 
//		{
//			toOtherObject.Normalize ();
//			
//			acceleration = otherObject.transform.position - transform.position;
//			
//			transform.position = transform.position + acceleration * Time.deltaTime;
//			
//		}

//		if (toOtherObject.magnitude < 3f)
//		{
//			toOtherObject.Normalize ();
//			
//			acceleration = otherObject.transform.position - transform.position;
//						
//			transform.position = transform.position - acceleration * Time.deltaTime;
//		}

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
