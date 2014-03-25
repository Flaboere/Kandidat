using UnityEngine;
using System.Collections;

public class ControllerTest : MonoBehaviour 
{

	private bool activeMovement = true;

	public float speed = 10;
	public float maxSpeed = 10;

	public float jumpForce = 10;
	private float jumpPower;
//	public bool jumpingUp;
	public float jumpMaxDuration = 2;
	public bool jumping = false;
	private bool jumpReleased = true;

	private bool jumpCor = false;

	public bool jumpKeyDown = false;

	private KeyCode jumpKey = KeyCode.Joystick1Button0;



	private float gravity;
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

			inputDir = new Vector3 (Input.GetAxis("Horizontal"), 0, 0);

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

			if(Input.GetKey(jumpKey) && !jumpKeyDown && grounded && jumpReleased)
			{
				jumpReleased = false;
				jumpKeyDown = true;
			}
			
			if(Input.GetKey(jumpKey) && !jumpKeyDown && !grounded && jumpReleased)
			{
				jumpReleased = false;
				jumpKeyDown = true;
			}

//			if (Input.GetKey(jumpKey))
//			{
//				jumpKeyDown = true;
//			}

			if (!Input.GetKey(jumpKey))
			{
				jumpKeyDown = false;
				jumping = false;
			}

			if(Input.GetKeyUp(jumpKey))
			{
				jumpReleased = true;
			}

			if (grounded && jumpKeyDown && !jumpCor)
			{
//				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, 0);
//				jumpingUp = true;
//				jumping = true;
				StartCoroutine (Jump());
			}

			if (jumping)
			{
				jumpPower = jumpForce;
			}
			else
			{
				if(jumpPower > 0 && !grounded)
				{
					jumpPower -= Time.deltaTime * 40;
				}
				else
				{
					jumpPower = 0;
				}
			}
//			if (jumpingUp && Input.GetButtonUp("Jump"))
//			{
//				jumpingUp = false;
//			}

//			if (!grounded && !jumpingUp)
//			{
//				jumpPower -= Time.deltaTime * 20;
//			}
			if (jumpKeyDown)
			{
				print ("jumpDown");
			}
			if (!jumpKeyDown)
			{
				print ("jumpUp");
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
				
//			}
		
			
		}

	}

	IEnumerator Jump()
	{
		jumpCor = true;
		jumping = true;
		yield return new WaitForSeconds(jumpMaxDuration);
		jumpKeyDown = false;
		jumpCor = false;
		jumping = false;
	}


	void LateUpdate()
	{
		// Begrænser hastigheden i X ved at maxe velocity.x
		rigidbody.velocity = new Vector3 (Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed),rigidbody.velocity.y, 0);
	}
}
