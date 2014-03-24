using UnityEngine;
using System.Collections;

public class ControllerTest : MonoBehaviour 
{

	public bool activeMovement = true;

	public float speed = 10;
	public float maxSpeed = 10;

	public float jumpForce = 10;
	private float jumpPower;
	public bool jumpKeyDown;
	public bool jumping;
	public bool jumpingUp;

	public float gravity;
	public float gravityGrounded = 0.05f;
	public float gravityAir = 9.18f;


	private bool grounded = false;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	private float groundRadius = 0.2f;

	private Vector3 inputDir;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activeMovement)
		{
			grounded = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;

			inputDir = new Vector3 (Input.GetAxis("Horizontal"), 0, 0).normalized;

			// Sætter gravity; den skal være meget lidt når grounded, ellers kan man ikke bevæge sig
			if (!grounded)
			{
				gravity = gravityAir;
			}

			if (grounded)
			{
				gravity = gravityGrounded;
//				jumpPower = 0f;
			}

			if (grounded && Input.GetButtonDown ("Jump"))
			{
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, 0);
				jumpingUp = true;
				jumping = true;
				jumpPower = jumpForce;
			}

			if (jumpingUp && Input.GetButtonUp("Jump"))
			{
				jumpingUp = false;
			}

			if (!grounded && !jumpingUp)
			{
				jumpPower -= Time.deltaTime * 20;
			}

		}
	}



	void FixedUpdate()
	{
		if (activeMovement) // gør så jeg kan slå styring fra hvis jeg har brug for det
		{
			// Movement
			rigidbody.AddForce(new Vector3(inputDir.x * speed, -gravity, 0), ForceMode.VelocityChange);

//			if (jumpingUp)
//			{
				rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
				jumping = false;
//			}
		
			
		}

	}


	void LateUpdate()
	{
		// Begrænser hastigheden i X ved at maxe velocity.x
		rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed),rigidbody.velocity.y, 0);

		print (rigidbody.velocity.y);
	}
}
