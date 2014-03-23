using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterControl : MonoBehaviour 
{

	public float speed = 10f;
	float force;
	public float gravity = 9.81f;
	public float maxSpeed = 10f;
	public float maxSpeedBoost = 10f;
	public float boost = 50;

	bool grounded = false;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	float groundRadius = 0.2f;

	bool inAir = true;


	Vector3 inputDir;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		grounded = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;

		// Bevægelse i X, tilføjer kraft, men tilføjer også kraft i Y (gravity)
		inputDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		rigidbody.AddForce(new Vector3(inputDir.x * force, -gravity, 0), ForceMode.Force);

		// Styrer max fart, samt "gravity" hvis man er grounded eller ikke grounded
//		rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed), 0, 0);

		if (grounded)
		{
			rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed), 0, 0);
			inAir = false;
		}

		if (!grounded)
		{
			rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed), -gravity, 0);
			inAir = true;
		}

		// Sprint
		if (Input.GetAxis ("RT") < -0.2)
		{
			force = boost;
			maxSpeed = 20f;

		}
		else
		{
			force = speed;
			maxSpeed = 10f;
		}

		// Debug right trigger
//		if (Input.GetAxis ("RT")>-0.5)
//		{
//			print ("Half");
//		}
//		if (Input.GetAxis ("RT")<-0.5)
//		{
//			print ("Whole");
//		}
//		float axis = Input.GetAxis ("RT");

//		print (axis);
//		print (rigidbody.velocity.x);
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (groundCheck.position, groundRadius);
	}

}
