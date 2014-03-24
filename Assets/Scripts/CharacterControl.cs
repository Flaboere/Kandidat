using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterControl : MonoBehaviour 
{

	public float speed = 10f;

	public float gravity = 9.81f;
	public float maxSpeed = 10f;
	public float maxSpeedBoost = 10f;
	public float boost = 50;
	float force;

	public float drag= 2;
	public float airDrag = 4;
	public float jumpForce = 50;

	public bool grounded = false;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	float groundRadius = 0.2f;

	bool inAir = true;


	Vector3 inputDir;

	// Use this for initialization
	void Start () 
	{

	}

	void Update ()
	{
		grounded = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{




		// Bevægelse i X, tilføjer kraft, men tilføjer også kraft i Y (gravity)
		inputDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		rigidbody.AddForce(new Vector3(inputDir.x * speed, 0, 0), ForceMode.Force);

		print (inputDir);
		// Styrer max fart, samt "gravity" hvis man er grounded eller ikke grounded

//		if (grounded)
//		{
//			rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed), rigidbody.velocity.y, 0);
//			rigidbody.drag = drag;
//			inAir = false;
//		}

//		if (!grounded)
//		{
//			rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed), rigidbody.velocity.y, 0);
//			rigidbody.AddForce (new Vector3 (0, -gravity, 0), ForceMode.Force);
//			rigidbody.drag = airDrag;
//			inAir = true;
//		}

		// Sprint
//		if (Input.GetAxis ("RT") < -0.2)
//		{
//			force = boost;
//			maxSpeed = 20f;
//
//		}
//		else
//		{
//			force = speed;
//			maxSpeed = 10f;
//		}


//		print (rigidbody.velocity.x);
//		print (grounded);

	}

//	public void OnDrawGizmos()
//	{
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireSphere (groundCheck.position, groundRadius);
//	}

}
