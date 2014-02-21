using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	public float Speed = 0;
	public float MaxSpeed = 10f;
	public float Acceleration = 10f;
	public float Deceleration = 10f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	if (Input.GetKey(KeyCode.RightArrow)&&(Speed < MaxSpeed)) 
		{
			Speed = Speed + Acceleration * Time.deltaTime;
		}
	else if (Input.GetKey(KeyCode.LeftArrow)&&(Speed>-MaxSpeed))
		{
			Speed = Speed - Acceleration * Time.deltaTime;
		}
	else
		{
		if(Speed > Deceleration * Time.deltaTime)
		{
			Speed = Speed - Deceleration * Time.deltaTime;
		}
		else if(Speed < -Deceleration * Time.deltaTime) 
		{
			Speed = Speed + Deceleration * Time.deltaTime;
		}
		else
		{
			Speed = 0;
		}
		}
	transform.position = new Vector3 ( transform.position.x + Speed * Time.deltaTime, 0, 0);
	}
}