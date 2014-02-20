using UnityEngine;
using System.Collections;

public class MoveØvelse5A : MonoBehaviour 
{
	public Transform otherObject;
	public float speedFactor = 0.2f;
	public Vector3 speed = Vector3.zero;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 toOtherObject = otherObject.transform.position - transform.position;

		if (toOtherObject.magnitude > 1f) 
		{
			toOtherObject.Normalize ();

			speed = toOtherObject * speedFactor;

			transform.position = transform.position + speed * Time.deltaTime;
		}
	}
}
