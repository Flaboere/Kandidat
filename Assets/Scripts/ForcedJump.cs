using UnityEngine;
using System.Collections;

public class ForcedJump : MonoBehaviour 
{
	
	public CharacterMotor motor;

	public Vector3 direction = new Vector3(1,1,0);
	public float force = 100;

	// Use this for initialization
	void Start () 
	{
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
	}



	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider hit)
	{
		motor.movement.velocity = direction * force;
		motor.grounded = false;
	}
}
