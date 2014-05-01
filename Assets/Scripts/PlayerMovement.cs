using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour 
{

	[HideInInspector]
	public bool activeMovement = true;
	private CharacterMotor motor;
	private float startJumpHeight;
//	private float inAirJumpHeight;
//	public float inAirJumpMultiplier = 10;
	[HideInInspector]

	// Do i have an extra jump picked up
	public bool extraJump = false;

	// Is double jump feature activated
	public bool doubleJumpOn = true;

	// Can i double jump right now
	public bool canDoubleJump = true;

	public bool canMove = false;

	// floats der indeholder den speed man har når banen starter
	private float tempSpeed;
	private float tempMoveAccel;
	private float tempAirAccel;

	// floats for hop når man starter banen
	private float tempBaseHeight;
	private float tempExtraHeight;

	//tillader sprint
	public float sprintAmount = 100;
	public float maxSprintAmount;
	private float sprintTemp;
	public float sprintRemove = 0.1f;
	public float sprintRecover = 0.05f;
	public bool canSprint = true;
	private bool sprinting = false;

	// Sprint input
	private bool sprintButtonDown = false;
	private bool sprintButtonUp = true;


	// floats for sprint hastigheder
	public bool canSprintOn = true;
	public float sprintMoveSpeed = 15;
	public float sprintMoveAccel = 15;
	public float sprintAirAccel = 15;
//	public float sprintBaseHeight = 15;
//	public float sprintExtraHeight = 15;
//	public bool sprinting = false;
	private float sprintMoveSpeedTemp;
	private float sprintMoveAccelTemp;
	private float sprintAirAccelTemp;
	public float SprintWarning1;
	public float SprintWarning2;

	// Variabler for når man er i vand
	public bool inWater = false;
	public float waterMoveSpeed = 5;
	public float waterMoveAccel = 5;
	private float waterMoveSpeedTemp;
	private float waterMoveAccelTemp;

	PlayerIndex player1 = PlayerIndex.One;


	void Start () 
	{
//		activeMovement = true;
		motor = GetComponent<CharacterMotor>();
		startJumpHeight = motor.jumping.baseHeight;
//		inAirJumpHeight = startJumpHeight * inAirJumpMultiplier;

		// temp variabler brugt til movement
		tempSpeed = motor.movement.maxSidewaysSpeed;
		tempMoveAccel = motor.movement.maxGroundAcceleration;
		tempAirAccel = motor.movement.maxAirAcceleration;
		sprintTemp = sprintAmount;
		sprintAmount = maxSprintAmount;

		// Sprint hastigheder
		sprintMoveSpeedTemp = tempSpeed + sprintMoveSpeed;
		sprintMoveAccelTemp = tempMoveAccel + sprintMoveAccel;
		sprintAirAccelTemp = tempAirAccel + sprintAirAccel;

		// hop under sprint
//		tempBaseHeight = motor.jumping.baseHeight;
//		tempExtraHeight = motor.jumping.extraHeight;

		// Movement speeds i vand
		waterMoveSpeedTemp = tempSpeed - waterMoveSpeed;
		waterMoveAccelTemp = tempMoveAccel - waterMoveAccel;
	}
	

	void Update () 
	{
		if (Input.GetButtonDown ("Start"))
		{
			canMove = true;
		}

		// Eksempel på at tvinge spiller til at hoppe
//		if(Input.GetKeyDown(KeyCode.F))
//		{
//			motor.movement.velocity = new Vector3(1f, 1f, 0f) * 100f;
//			motor.grounded = false;
//		}

		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

		// Skifter kamera baggrund når hop knappen trykkes
//		Camera.main.backgroundColor = Input.GetButton ("Jump") ? Color.blue : Color.red;

		// Input til styring
		if (canMove)
		{
			motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal");
			motor.inputJump = Input.GetButton ("Jump")||Input.GetKey (KeyCode.Space);

			if (doubleJumpOn)
			{
				if (motor.grounded)
				{
					canDoubleJump = true;	
				}
				
				
				if (motor.jumping.baseHeight != startJumpHeight) 
				{
					motor.jumping.baseHeight = startJumpHeight;
					
				}

				// Doublejump
				if (!motor.grounded && canDoubleJump) 
				{
					if (Input.GetButtonDown ("Jump")||Input.GetKeyDown (KeyCode.Space))
					{
						StartCoroutine (DoubleJump());
						
					}
				}
			}

			// Hop pickup
			if (motor.grounded)
			{
				extraJump = false;
			}
			
			if (!motor.grounded && extraJump)
			{
				if (Input.GetButtonDown ("Jump")||Input.GetKeyDown (KeyCode.Space))
				{
					StartCoroutine (ExtraJump());
				}
			}
			
			// Sprint


			// Water movement
			if (inWater)
			{
				motor.movement.maxSidewaysSpeed = waterMoveSpeedTemp;
				motor.movement.maxGroundAcceleration = waterMoveAccelTemp;
			}
			if (!inWater && !sprinting)
			{
				motor.movement.maxSidewaysSpeed = tempSpeed;
				motor.movement.maxGroundAcceleration = tempMoveAccel;
			}

		}

		if (canSprintOn && canMove)
		{
			// Afgør sprint input
			if (Input.GetAxis ("RT") < -0.2)
			{
				sprintButtonDown = true;
				sprintButtonUp = false;
			}
			else
			{
				sprintButtonDown = false;
				sprintButtonUp = true;
			}
			
			// Sprint mekanik
			if (sprintButtonDown && sprintAmount > 0f)
			{
				sprintAmount -= sprintRemove;
				sprinting = true;
			}
			if (sprintButtonDown && sprintAmount <= 0f)
			{
				sprinting = false;
			}
			if (sprintButtonUp && sprintAmount < maxSprintAmount)
			{
				sprintAmount += sprintRecover;
				sprinting = false;
				
			}
			
			// Sprint hastigheder
			if (sprinting)
			{
				motor.movement.maxSidewaysSpeed = sprintMoveSpeedTemp;
				motor.movement.maxGroundAcceleration = sprintMoveAccelTemp;
				motor.movement.maxAirAcceleration = sprintAirAccelTemp;
			}
			else
			{
				motor.movement.maxSidewaysSpeed = tempSpeed;
				motor.movement.maxGroundAcceleration = tempMoveAccel;
				motor.movement.maxAirAcceleration = tempAirAccel;
			}

			//Sprint vibration
			if (!sprinting || (sprintAmount >= maxSprintAmount))
			{
				GamePad.SetVibration(player1, 0f, 0f);
			}

			if (sprinting && (sprintAmount < maxSprintAmount/SprintWarning1))
			{
				GamePad.SetVibration(player1, 0.1f, 0.3f);
			}
			
		}

	}


	IEnumerator DoubleJump()
	{
		motor.movement.maxAirAcceleration = 0;
	
		motor.Jump();

		canDoubleJump = false;
		yield return new WaitForSeconds (0.1f);
		motor.movement.maxAirAcceleration = tempSpeed;
	}

	IEnumerator ExtraJump()
	{
		motor.movement.maxAirAcceleration = 0;
		
		motor.Jump();
		extraJump = false;
		yield return new WaitForSeconds (0.1f);
		motor.movement.maxAirAcceleration = tempSpeed;
	}

//	IEnumerator SprintRecover()
//	{
//		yield return new WaitForSeconds (sprintRecover);
//		sprintAmount = sprintTemp;
////		sprintAmount += 0.1f;
//	}


	IEnumerator SprintVibrate ()
	{
		GamePad.SetVibration(player1, 0.1f, 0.3f);
		yield return new WaitForSeconds (0.3f);
		GamePad.SetVibration (player1, 0f, 0f);
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = true;
			canSprintOn = false;
		}
	}
	
	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = false;
			canSprintOn = true;
		}
	}
	
}
		



