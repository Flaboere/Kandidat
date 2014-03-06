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
	bool doubleJump = false;
	// Use this for initialization
	void Start () 
	{
	
	}

	void Update ()
	{
		if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
		{
			rigidbody.AddForce(new Vector2(0,jumpForce));
			if (!doubleJump && !grounded)
			{
				doubleJump = true;
			}
		}
	}
	// Update is called once per frameo
	void FixedUpdate () 
	{
		grounded = Physics.OverlapSphere(groundCheck.position, groundRadius, whatIsGround).Length > 0;

		float move = Input.GetAxis ("Horizontal");

		rigidbody.velocity = new Vector2 (move * maxSpeed, rigidbody.velocity.y);

		if (grounded) 
		{
			doubleJump = false;
		}


		if (grounded) 
		{
			Debug.Log ("grounded");
		} 
		else 
		{
			Debug.Log ("unground");
		}
	}

//	- Denne function tegner en sphere med det samme information som groundCheck bruger, og gør den rød
//	public void OnDrawGizmos()
//	{
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireSphere (groundCheck.position, groundRadius);
//	}
}
