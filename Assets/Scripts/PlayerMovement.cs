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


	// Variabler for kontakt med Hurlde
	public bool playerTouching = false;
	private CharacterController controller;

	// Do i have an extra jump picked up
	public bool extraJump = false;

	// Is double jump feature activated
	public bool doubleJumpOn = true;

	// Can i double jump right now
	public bool canDoubleJump = true;

	// Can i move
	public bool canMove = false;

	public bool canJump = true;
	public bool jumped = false;
	public float canJumpTimer = 1.5f;
	public float canJumpTimerTemp = 0f;

	// Am i out of breath after sprinting
	private bool outOfBreath = false;
	

//	public float forwardJump = 3;

	// floats der indeholder den speed man har når banen starter
	private float tempSpeed;
	private float tempMoveAccel;
	private float tempAirAccel;

	// floats for hop når man starter banen
	private float tempBaseHeight;
	private float tempExtraHeight;

	//variabler for sprint funktion
	[HideInInspector]
	public float sprintAmount = 10;
	private float maxSprintAmount = 10;
	private float sprintTemp;
	public float sprintRemove = 0.1f;
	public float sprintRecover = 0.05f;
	public bool canSprint = true;
	private bool sprinting = false;
//	public float sprintVibrateTemp = 10;
	private float sprintVibrate;

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


	// Variabler for når man er i vand
	public bool inWater = false;
	public float waterMoveSpeed = 5;
	public float waterMoveAccel = 5;
	private float waterMoveSpeedTemp;
	private float waterMoveAccelTemp;

	// Animation ting
	private bool moveRight;
	private bool moveLeft;
	private bool jumpUp;
	private bool jumpForward;
	private bool idling;
	private Animator animator;
	private Transform animatorGameObject;


	// Partikel effekter
	private GameObject particleSweat;
//	public ParticleSystem[] particles;
	public GameObject waterSplash;


	PlayerIndex player1 = PlayerIndex.One;


	void Start () 
	{
//		Time.timeScale = 0.5f;
		motor = GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController> ();
		startJumpHeight = motor.jumping.baseHeight;
//		inAirJumpHeight = startJumpHeight * inAirJumpMultiplier;
//		particles = GetComponentsInChildren<ParticleSystem> ();
		waterSplash = GameObject.Find ("Particle_water");
		waterSplash.SetActive (false);
		particleSweat = GetComponentInChildren<ParticleSystem>().gameObject;
		particleSweat.SetActive(false);

		// Animations stuff
		animator = GetComponentInChildren<Animator>();
		animatorGameObject = animator.gameObject.transform;

		// temp variabler brugt til movement
		tempSpeed = motor.movement.maxSidewaysSpeed;
		tempMoveAccel = motor.movement.maxGroundAcceleration;
		tempAirAccel = motor.movement.maxAirAcceleration;
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
			motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal") * (outOfBreath ? 0f : 1f);

			print (motor.inputJump);

			if (!outOfBreath)
			{

				if ((Input.GetAxis("Horizontal") > -0.2f ) && (Input.GetAxis("Horizontal") < 0.2f ) && Input.GetButton ("Jump") && motor.grounded)
				{
					canJumpTimerTemp += 1f;

					if (canJumpTimerTemp >= canJumpTimer)
						{
							motor.inputJump = true;	
							jumped = true;
						}

				}
				else if ((Input.GetAxis("Horizontal") < -0.2f ) || (Input.GetAxis("Horizontal") > 0.2f ))
				{
					motor.inputJump = Input.GetButton ("Jump");
				}

				if (jumped && motor.grounded)
				{

					canJumpTimerTemp = 0f;
					jumped = false;
				}



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
						if (Input.GetButtonDown ("Jump"))
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
					if (Input.GetButtonDown ("Jump"))
					{
						StartCoroutine (ExtraJump());
					}
				}


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

		}

		if (canSprintOn && canMove)
		{
			// Afgør sprint input
			if (outOfBreath)
			{
				//motor.movement.velocity.x = 0f;
			}

			if (Input.GetAxis ("RT") < -0.2 && !outOfBreath)
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
			if (sprintButtonDown && sprintAmount > 0f && motor.grounded)
			{
				sprintAmount -= sprintRemove;
				sprinting = true;
			}
			if (sprintButtonDown && sprintAmount > 0f && !motor.grounded && sprinting)
			{
				sprintAmount -= sprintRemove;
			}

			if (sprintButtonDown && sprintAmount <= 0f && motor.grounded)
			{
				sprinting = false;
				outOfBreath = true;
			}
			if (sprintButtonUp && sprintAmount < maxSprintAmount)
			{
				sprintAmount += sprintRecover;
				
			}
			if (sprintButtonUp && sprintAmount < maxSprintAmount && motor.grounded)
			{
				sprinting = false;
				
			}
			if (sprintButtonUp && sprintAmount >= maxSprintAmount && motor.grounded)
			{
				sprinting = false;
			}
			if (!sprinting && sprintAmount >= maxSprintAmount && outOfBreath)
			{
				outOfBreath = false;
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
			sprintVibrate = (maxSprintAmount - sprintAmount)/10;


			if (sprintAmount < (maxSprintAmount/2f) && sprintButtonDown)
			{
				GamePad.SetVibration(player1, sprintVibrate/3f, sprintVibrate/1.5f);
			}
			if (sprintAmount >= (maxSprintAmount/3f) && sprintButtonUp)
			{
				GamePad.SetVibration(player1, 0f, 0f);
			}
		}

		// Animation styring
		if (canMove)
		{
			animator.SetBool ("moveRight", moveRight);
			animator.SetBool ("moveLeft", moveLeft);
			animator.SetBool ("idling", idling);
			animator.SetBool ("jumpUp", jumpUp);
			animator.SetBool ("jumpForward", jumpForward);
			animator.SetBool ("sprinting", sprinting);
			animator.SetBool ("outOfBreath", outOfBreath);

			if (!outOfBreath)
			{
				if (Input.GetAxis ("Horizontal") > 0.1f)
				{
					animatorGameObject.eulerAngles = new Vector3 (0f,90f,0f);
				}

				if (Input.GetAxis ("Horizontal") < -0.1f)
				{
					animatorGameObject.eulerAngles = new Vector3 (0f,-90f,0f);
				}



				if (motor.grounded)
				{
					if (Input.GetAxis ("Horizontal") > 0.1f && !moveRight)
					{
						moveRight = true;
						moveLeft = false;
						idling = false;
						jumpUp = false;
						jumpForward = false;

					}

					if (Input.GetAxis ("Horizontal") < -0.1f && !moveLeft)
					{
						moveRight = false;
						moveLeft = true;
						idling = false;
						jumpUp = false;
						jumpForward = false;

					}

					if (Input.GetAxis ("Horizontal") > -0.1f && Input.GetAxis ("Horizontal") < 0.1f && !idling)
					{
						moveRight = false;
						moveLeft = false;
						idling = true;
						jumpUp = false;
						jumpForward = false;

					}

				}
				if (Input.GetButton ("Jump") && ((Input.GetAxis("Horizontal") > -0.2f ) || (Input.GetAxis("Horizontal") < 0.2f )))
				{
					idling = false;
					jumpUp = true;
					jumpForward = false;
					moveRight = false;
					moveLeft = false;

				}
				if (Input.GetButton ("Jump") && ((Input.GetAxis("Horizontal") < -0.2f ) || (Input.GetAxis("Horizontal") > 0.2f )))
				{
					idling = false;
					jumpUp = false;
					jumpForward = true;
					moveRight = false;
					moveLeft = false;

				}
				// Effekter
				if (Input.GetButton ("Jump") && !motor.grounded)
				{
					particleSweat.SetActive(true);
				}
				if (motor.grounded)
				{
					particleSweat.SetActive(false);
				}
			}
				
		}
		if (controller.collisionFlags == CollisionFlags.None)
		{
			playerTouching = false;
		}
		if (controller.collisionFlags == CollisionFlags.Below)
		{
			playerTouching = true;
		}
	}

	IEnumerator CanJump()
	{
		yield return new WaitForSeconds(canJumpTimer);
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

	IEnumerator ExtraJumpVibrate()
	{
		GamePad.SetVibration(player1, 0.1f, 0.3f);
		yield return new WaitForSeconds (0.3f);
		GamePad.SetVibration(player1, 0f, 0f);
	}

	// Kollision for hvis spilleren er i/ude af Water
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = true;
			canSprintOn = false;
			sprinting = false;
			waterSplash.SetActive(true);

//			waterSplash.SetActive(true);
//			waterSplash.SetActive(false);
//			waterSplash.SetActive = false;
			// Her slår jeg "water" patikel til
//			foreach (ParticleSystem particle in particles)
//			{
//				if (gameObject.transform.name ="Particle_Water")
//				{
//					gameObject.SetActive = true;
//				}
//			}

		}

		if (hit.gameObject.CompareTag ("ExtraJump")) 
		{
			extraJump = true;
			StartCoroutine (ExtraJumpVibrate());
		}
	}
	
	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = false;
			canSprintOn = true;
			waterSplash.SetActive(false);
		}
	}

//	void OnControllerColliderHit(ControllerColliderHit hit)
//	{
//		if (hit.gameObject.CompareTag ("Hurdle"))
//		{
////			if (controller.collisionFlags == CollisionFlags.Below)
////			{
////				playerTouching = true;
////			}
//		}
//	}
//	// Hvis spilleren hopper ind i ExtraJump
//	void OnTriggerEnter(Collider extrajump)
//	{
//
//	}




}
		



