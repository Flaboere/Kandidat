using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour 
{

	public float maxSpeed = 10f;
//	bool facingRight = true; - har med animation at gøre, ikke brugt
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
	public bool doubleJump = false;
	Vector3 playerRotate;
	public float rotateAmount = 2;
	bool jumpPickup = false;
	// Use this for initialization
	void Start () 
	{
	
	}

	void Update ()
	{

	}

	// Opdager collision, og "tilføjer" et hop
	void OnTriggerEnter(Collider pickupHit)
	{
		if (pickupHit.collider.CompareTag ("ExtraJump"))
		    {
				doubleJump = false;
			Debug.Log ("ExtraJump");
			}
	}


	void FixedUpdate () 
	{
		// Rotation af avatar ved hop
		playerRotate.z = rigidbody.velocity.y;

		if (rigidbody.velocity.x > 0)
		{
		this.transform.rotation = Quaternion.Euler (playerRotate * -rotateAmount);
		}

		if (rigidbody.velocity.x < 0)
		{
			this.transform.rotation = Quaternion.Euler (playerRotate * rotateAmount);
		}

		// Bestemmer hvornår man kan hoppe
		if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
		{
			rigidbody.AddForce(new Vector2(0,jumpForce));
			Debug.Log ("Jumped");
			if (!doubleJump && !grounded)
			{
				doubleJump = true;
			}
		}
		if (grounded) 
		{
			doubleJump = false;
		}


		// Sætter rotation i 0 hvis grounded
		if (grounded)
		{
			this.transform.rotation = Quaternion.Euler (0,0,0);
		}

		// Bestemmer grounded
		grounded = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;

		float move = Input.GetAxis ("Horizontal");

		rigidbody.velocity = new Vector2 (move * maxSpeed, rigidbody.velocity.y);



	}

//	- Denne function tegner en sphere med det samme information som groundCheck bruger, og gør den rød
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (groundCheck.position, groundRadius);
	}
}
