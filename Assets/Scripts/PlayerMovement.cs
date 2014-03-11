using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour 
{

	[HideInInspector]
	public bool activeMovement = true;
	private CharacterMotor motor;
	private float startJumpHeight;
	private float inAirJumpHeight;
	public float inAirJumpMultiplier = 10;
	public bool canDoubleJump = true;
	
	void Start () 
	{
		activeMovement = true;
		motor = GetComponent<CharacterMotor>();
		startJumpHeight = motor.jumping.baseHeight;
		inAirJumpHeight = startJumpHeight * inAirJumpMultiplier;
	}
	

	void Update () 
	{

		motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal");
		motor.inputJump = Input.GetButton ("Jump")||Input.GetKey (KeyCode.Space);

		if (motor.jumping.baseHeight != startJumpHeight) 
		{
			motor.jumping.baseHeight = startJumpHeight;

		}
		if (!motor.grounded && canDoubleJump) 
		{
				if (Input.GetButtonDown ("Jump"))
			{
				motor.grounded = true;
				motor.inputJump = true;
				motor.jumping.baseHeight = inAirJumpHeight;
				motor.movement.velocity.y = 0f;
				canDoubleJump = false;

			}
		}
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag ("Ground"))
		{
			canDoubleJump = true;
			print ("touchedGround");

		}

	}
	
}
		



