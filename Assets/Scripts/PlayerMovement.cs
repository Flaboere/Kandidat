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

	PlayerIndex player1 = PlayerIndex.One;


	void Start () 
	{
		activeMovement = true;
		motor = GetComponent<CharacterMotor>();
		startJumpHeight = motor.jumping.baseHeight;
		inAirJumpHeight = startJumpHeight * inAirJumpMultiplier;
	}
	

	void Update () 
	{
		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

		motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal");
		motor.inputJump = Input.GetButton ("Jump")||Input.GetKey (KeyCode.Space);

		if (Input.GetButtonDown ("Jump") && motor.grounded) 
		{
			StartCoroutine (Vibrate());
			//skal bruge en ordentlig "grounded" for ikke at gøre det hver gang knappen trykkes
		}


		if (motor.grounded)
		{
			canDoubleJump = true;	
		}


		if (motor.jumping.baseHeight != startJumpHeight) 
		{
			motor.jumping.baseHeight = startJumpHeight;

		}

	}

	void FixedUpdate ()
	{
		if (!motor.grounded && canDoubleJump) 
		{
			if (Input.GetButtonDown ("Jump"))
			{
				motor.grounded = true;
				motor.inputJump = true;
				motor.movement.velocity.y = 0f;
				motor.jumping.baseHeight = inAirJumpHeight;
				canDoubleJump = false;
			}
		}
	}

	IEnumerator Vibrate ()
	{
		GamePad.SetVibration(player1, 0.1f, 0.3f);
		yield return new WaitForSeconds (0.3f);
		GamePad.SetVibration (player1, 0f, 0f);
	}


	
	
	
}
		



