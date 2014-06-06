using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour 
{
	PlayerIndex player1 = PlayerIndex.One;

	// Import af komponenter
	private CharacterMotor motor;
	private CharacterController controller;
	private AnimationChar animChar;

	// Spiller scenen i slowmotion
	public bool slowMo = false;
	public float slowMoSpeed = 0.5f;

	// Information omkring hvorvidt figuren bevæger sig
	public bool canMove = false;
	private bool isMoving = false;
	private bool isStopped = true;
	private bool inAir;
	public bool characterFlipping = true;
	public bool characterTurning = false;
	public bool facingRight = true;
	public bool facingLeft = false;
	public bool analogRight = false;
	public bool analogLeft = false;
	public float turnSpeed = 2f;
	public bool turning = false;

	// floats der indeholder den speed man har når banen starter
	private float tempSpeed;
	private float tempMoveAccel;
	private float tempAirAccel;

	// Typer af hop til/fra
	public bool standardHop = true;
	[HideInInspector]
	public bool tilløbUp = false;
	[HideInInspector]
	public bool tilløbAltid = false;
	[HideInInspector]
	public bool hopPause = false;

	// Do i have an extra jump picked up
	[HideInInspector]
	public bool extraJump = false;
	
	// Is double jump feature activated
	[HideInInspector]
	public bool doubleJumpOn = true;
	
	// Can i double jump right now
	[HideInInspector]
	public bool canDoubleJump = true;
	private float startJumpHeight;
	
	// Bruges til pause mellem hop
	public float canJumpTimer = 1.5f;
	public float canJumpTimerTemp = 0f;

	//variabler for sprint funktion
	public bool canSprint = true;
	[HideInInspector]
	public bool sprinting = false;

	// Sprint input
	private bool sprintButtonDown = false;
	private bool sprintButtonUp = true;


	[HideInInspector]
	public float sprintAmount = 10;
	private float maxSprintAmount = 10;
	private float sprintTemp;
	public float sprintRemove = 0.1f;
	public float sprintJumpRemove = 0.1f;
	public float sprintRecover = 0.05f;
	private bool outOfBreath = false;
	private float sprintVibrate;
	
	// floats for sprint hastigheder
	public bool canSprintOn = true;
	public float sprintMoveSpeed = 15;
	public float sprintMoveAccel = 15;
	public float sprintAirAccel = 15;
//	public float sprintBaseHeight = 15;
	public float sprintExtraHeight = 15;
	private float sprintMoveSpeedTemp;
	private float sprintMoveAccelTemp;
	private float sprintAirAccelTemp;
	private float tempBaseHeight;
	private float tempExtraHeight;
//	private float sprintBaseHeightTemp;
	private float sprintExtraHeightTemp;

	
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
	private bool jumpingUp;
	private bool idling;
	private Animator animator;
	private Transform animatorGameObject;

	// Pause mellem hop - variabler
	[HideInInspector]
	public float jumpCounter = 20;
	[HideInInspector]
	public bool canJumpNow = true;
	[HideInInspector]
	public float jumpCounterTemp;

	// Lyde
	private AudioSource water;
	private AudioSource steps;
	public AudioSource stepsWater;
	private AudioSource audioJump;
	private AudioSource audioJumpVoice;
	public AudioClip jumpTakeOff;
	public AudioClip jumpLand;
	public AudioClip[] jumpVoice;
	public AudioClip[] landVoice;
	public AudioClip[] sprintVoice;
	public AudioClip[] outOfBreathVoice;
	private bool sprintBreathe = true;




	void Start () 
	{
		if (slowMo)
		{
			Time.timeScale = slowMoSpeed;
		}

		// Henter basis komponenter
		motor = GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController> ();

		// Lyde
		water = GameObject.Find ("audio_water").GetComponent<AudioSource> ();
		steps = GameObject.Find ("audio_steps").GetComponent<AudioSource> ();
		stepsWater = GameObject.Find ("audio_steps_water").GetComponent<AudioSource> ();
		audioJump = GameObject.Find ("audio_jump").GetComponent<AudioSource> ();
		audioJumpVoice = GameObject.Find ("audio_jump_voice").GetComponent<AudioSource> ();

		// Animations stuff
		animator = GetComponentInChildren<Animator>();
		animatorGameObject = animator.gameObject.transform;
		animChar = GetComponentInChildren<AnimationChar> ();

		// Husker standard hoppehøjden
		startJumpHeight = motor.jumping.baseHeight;

		// temp variabler brugt til movement
		tempSpeed = motor.movement.maxSidewaysSpeed;
		tempMoveAccel = motor.movement.maxGroundAcceleration;
		tempAirAccel = motor.movement.maxAirAcceleration;
		tempBaseHeight = motor.jumping.baseHeight;
		tempExtraHeight = motor.jumping.extraHeight;

		// Sætter mængden af sprint tilgængelig
		sprintAmount = maxSprintAmount;

		// Sprint hastigheder
		sprintMoveSpeedTemp = tempSpeed + sprintMoveSpeed;
		sprintMoveAccelTemp = tempMoveAccel + sprintMoveAccel;
		sprintAirAccelTemp = tempAirAccel + sprintAirAccel;

		// hop under sprint
//		sprintBaseHeightTemp = tempBaseHeight + sprintBaseHeight;
		sprintExtraHeightTemp = tempExtraHeight + sprintExtraHeight;


		// Movement speeds i vand
		waterMoveSpeedTemp = tempSpeed - waterMoveSpeed;
		waterMoveAccelTemp = tempMoveAccel - waterMoveAccel;
	}
	

	void Update () 
	{

		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

		// Ting Peter har lavet ved fejltagelse der var AWESOME
		// Skifter kamera baggrund når hop knappen trykkes
		//		Camera.main.backgroundColor = Input.GetButton ("Jump") ? Color.blue : Color.red;

		// Eksempel på at tvinge spiller til at hoppe
		//		if(Input.GetKeyDown(KeyCode.F))
		//		{
		//			motor.movement.velocity = new Vector3(1f, 1f, 0f) * 100f;
		//			motor.grounded = false;
		//		}

		// Print ting her:


		// Bools for at lande på jorden
		if (!motor.grounded)
		{
			inAir = true;
		}

		// Bools for bevægelse
		if ((motor.movement.velocity.x < 1f && motor.movement.velocity.x > -1f) || (Input.GetAxis ("Horizontal") > -0.2f && Input.GetAxis ("Horizontal") < 0.2f))
		{
			isStopped = true;
			isMoving = false;
		}

		if ((motor.movement.velocity.x > 1f || motor.movement.velocity.x < -1f) || (Input.GetAxis ("Horizontal") > 0.2f || Input.GetAxis ("Horizontal") < -0.2f))
		{
			isStopped = false;
			isMoving = true;
		}

		if (Input.GetButtonDown ("X"))
		{
			canMove = true;
		}

		// Standard styring
//		motor.inputMoveDirection = Vector3.right * Input.GetAxis ("Horizontal");
//		motor.inputJump = Input.GetButton ("Jump");
	
		// Input til styring
		if (canMove)
		{
			// Sætter input, og hvis "outOfBreath" gangner den med 0 = intet input
			motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal") * (outOfBreath ? 0f : 1f);


			if (!outOfBreath)
			{

				// Normalt input, uden tilløb eller pause ved hop
				if (standardHop)
				{
					motor.inputJump = Input.GetButton ("Jump");
				}

				if (hopPause)
				{
					if (canJumpNow)
					{
						motor.inputJump = Input.GetButton ("Jump");
					}
					if (canJumpNow && Input.GetButtonDown ("Jump"))
					{
						audioJumpVoice.pitch = Random.Range(0.9f, 1.1f);
						audioJumpVoice.clip = jumpVoice[Random.Range(0,jumpVoice.Length)];
						audioJumpVoice.Play();
					}
					
					if (motor.grounded && (jumpCounterTemp <= 0f))
					{
						canJumpNow = true;
					}
					if (jumpCounterTemp > 0f)
					{
						canJumpNow = false;
					}
					if (motor.grounded && !canJumpNow)
					{
						jumpCounterTemp -= 1f;
					}
					if (!motor.grounded)
					{
						jumpCounterTemp = jumpCounter;
					}
				}

				// Hoppe input - tilløb på jumpUp
				if (tilløbUp)
				{
					if (isStopped)
					{
						if (Input.GetButton ("Jump") && motor.grounded)
						{
							canJumpTimerTemp += 1f;
							jumpingUp = true;

							if (canJumpTimerTemp >= canJumpTimer)
							{
								motor.inputJump = true;
							}
						}
						if (Input.GetButton ("Jump") && canJumpTimerTemp > canJumpTimer && motor.grounded)
						{
							jumpingUp = false;
						}
					}
					else if (isMoving)
					{
						motor.inputJump = Input.GetButton ("Jump");
						jumpingUp = false;
					}
					if (!Input.GetButton ("Jump") && motor.grounded)
					{
						canJumpTimerTemp = 0f;
						motor.inputJump = false;
						jumpingUp = false;
					}
				}

				// Input hvor der er tilløb til alle slags hop
				if (tilløbAltid)
				{
					if (Input.GetButton ("Jump") && motor.grounded)
						{
							
							canJumpTimerTemp += 1f;
							
							if (canJumpTimerTemp >= canJumpTimer)
							{
								motor.inputJump = true;	
							}
						}

					if (!Input.GetButton ("Jump") && motor.grounded)
					{
						canJumpTimerTemp = 0f;
						motor.inputJump = false;
					}
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


			}
		}

		if (canSprintOn && canMove)
		{
			// Afgør sprint input
			if (Input.GetButtonDown ("Jump") && !outOfBreath)
			{
				sprintAmount -= sprintJumpRemove;
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
			if (sprintButtonDown && sprintAmount > 0f && motor.grounded && isMoving && !isStopped)
			{
				sprintAmount -= sprintRemove;
				sprinting = true;
			}

			if (!isMoving && isStopped)
			{
				sprinting = false;
			}

			if (sprintButtonDown && sprintAmount > 0f && !motor.grounded && sprinting)
			{
				sprintAmount -= sprintRemove;
			}
		}

		if (sprintAmount <= 0f && motor.grounded)
		{
			sprinting = false;
			outOfBreath = true;
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
		if (sprintButtonUp && sprintAmount < maxSprintAmount)
		{
			sprintAmount += sprintRecover;
		}
			
		// Sprint hastigheder
		if (sprinting)
		{
			motor.movement.maxSidewaysSpeed = sprintMoveSpeedTemp;
			motor.movement.maxGroundAcceleration = sprintMoveAccelTemp;
			motor.movement.maxAirAcceleration = sprintAirAccelTemp;
//			motor.jumping.baseHeight = sprintBaseHeightTemp;
			motor.jumping.extraHeight = sprintExtraHeightTemp;

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
			motor.movement.maxSidewaysSpeed = tempSpeed;
			motor.jumping.baseHeight = tempBaseHeight;
			motor.jumping.extraHeight = tempExtraHeight;
		}

		//Sprint vibration
		sprintVibrate = (maxSprintAmount - sprintAmount)/10;


		if (canMove)
		{
			if (sprintAmount < (maxSprintAmount/2f) && sprintButtonDown)
			{
				GamePad.SetVibration(player1, sprintVibrate/3f, sprintVibrate/1.5f);
			}
			if (sprintAmount >= (maxSprintAmount/3f) && sprintButtonUp)
			{
				GamePad.SetVibration(player1, 0f, 0f);
			}
		}

		// Bools for hvilken retning karakteren og styringen peger
//		if (animatorGameObject.eulerAngles.y < 269f)
//		{
//			facingRight = true;
//			facingLeft = false;
//		}
//		if (animatorGameObject.eulerAngles.y == 270f)
//		{
//			facingRight = false;
//			facingLeft = true;
//		}
		if (Input.GetAxis("Horizontal") < -0.1f)
		{
			analogRight = false;
			analogLeft = true;
		}
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			analogRight = true;
			analogLeft = false;
		}
		if (Input.GetAxis("Horizontal") == 0f)
		{
			analogRight = false;
			analogLeft = false;
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
				// Flipper figuren den rigtige retning
				if (characterFlipping)
				{
					if (Input.GetAxis ("Horizontal") > 0.1f && motor.grounded)
					{
						animatorGameObject.eulerAngles = new Vector3 (0f,90f,0f);
					}

					if (Input.GetAxis ("Horizontal") < -0.1f && motor.grounded)
					{
						animatorGameObject.eulerAngles = new Vector3 (0f,-90f,0f);
					}
				}

				// Drejer karakteren rundt når man skifter retning
				if (characterTurning)
				{
					if (facingRight && motor.grounded && !turning)
					{
						if (analogLeft)
						{
							StartCoroutine("ChangeDirection", true);
						}
					}
					if (facingLeft && motor.grounded && !turning)
					{
						if (analogRight)
						{
							StartCoroutine("ChangeDirection", false);
						}
					}
				}


				// Kører rigtige animation

				if (Input.GetAxis ("Horizontal") > 0.1f && motor.grounded)
				{
					moveRight = true;
					moveLeft = false;
					idling = false;
					jumpUp = false;
					jumpForward = false;

				}

				if (turning)
				{
					moveRight = true;
					moveLeft = false;
					idling = false;
					jumpUp = false;
					jumpForward = false;
				}

				if (Input.GetAxis ("Horizontal") < -0.1f && motor.grounded)
				{
					moveRight = false;
					moveLeft = true;
					idling = false;
					jumpUp = false;
					jumpForward = false;

				}

				if (isStopped && motor.grounded)
				{
					moveRight = false;
					moveLeft = false;
					idling = true;
					jumpUp = false;
					jumpForward = false;
					
				}
				
				if (!motor.grounded && isStopped)
				{
					idling = false;
					jumpUp = true;
					jumpForward = false;
					moveRight = false;
					moveLeft = false;

				}
//				if (Input.GetButton ("Jump") && isStopped)
//				{
//					idling = false;
//					jumpUp = true;
//					jumpForward = false;
//					moveRight = false;
//					moveLeft = false;
//					
//				}
				if (!motor.grounded && isMoving)
				{
					idling = false;
					jumpUp = false;
					jumpForward = true;
					moveRight = false;
					moveLeft = false;
				}
//				if (Input.GetButton ("Jump") && isMoving && !motor.grounded)
//				{
//					idling = false;
//					jumpUp = false;
//					jumpForward = true;
//					moveRight = false;
//					moveLeft = false;
//				}
			}		 
		}

		// Lyde (nogle lyde kaldes under andre funktioner)
		if (Input.GetButtonDown ("Jump") && motor.grounded && canMove)
		{
			audioJumpVoice.pitch = Random.Range(0.9f, 1.1f);
			audioJumpVoice.clip = jumpVoice[Random.Range(0,jumpVoice.Length)];
			audioJumpVoice.Play();
		}

		if (inAir && motor.grounded)
		{
			if (!inWater)
			{
				audioJump.pitch = Random.Range(0.8f, 1.2f);
				audioJump.volume = (0.35f);
				audioJump.clip = jumpLand;
				audioJump.Play();
				inAir = false;
			}
			if (!inWater)
			{
				audioJumpVoice.pitch = Random.Range(0.9f, 1.1f);
				audioJumpVoice.clip = landVoice[Random.Range(0,landVoice.Length)];
				audioJumpVoice.Play();
			}
		}

		if (sprinting && motor.grounded && !inWater)
		{
			if (sprintBreathe)
			{
				StartCoroutine(SprintBreathe());
			}
		}

		if (inWater)
		{
			if (inAir && motor.grounded)
			{
				audioJumpVoice.clip = landVoice[Random.Range(0,landVoice.Length)];
				audioJumpVoice.Play();
				inAir = false;
			}
		}


		if (outOfBreath)
		{
			if (sprintBreathe)
			{
				StartCoroutine(OutOfBreathing());
			}
		}
	}

	public void FootStep()
	{
		if (!inWater)
		{
			steps.pitch = Random.Range (0.5f, 1f);
			steps.Play ();
		}
//		else
//		{
//			print ("ding");
//			stepsWater.pitch = Random.Range (0.8f, 1.2f);
//			stepsWater.Play();
//
//		}
	}

	IEnumerator ChangeDirection(bool facing)
	{
		Vector3 currentRotation = animatorGameObject.eulerAngles;
		turning = true;
//		canMove = false;
		float mTime = 0f;
		while(turning)
		{
			if (facing)
			{
				facingRight = false;
				facingLeft = true;

				if (mTime < 1f)
				{
					moveLeft = true;
					mTime += Time.deltaTime * turnSpeed;
					animatorGameObject.eulerAngles = Vector3.Slerp(currentRotation, new Vector3(0,270,0), mTime);
				}
				else
				{
					turning = false;
//					canMove = true;
				}
//				print ("turned left");
//				yield return new WaitForSeconds (0.5f);
//				animatorGameObject.eulerAngles = new Vector3 (0f,270f,0f);
//				facingRight = false;
//				facingLeft = true;
			}
			else
			{
				facingRight = true;
				facingLeft = false;
				if (mTime < 1f)
				{
					moveRight = true;
					mTime += Time.deltaTime * turnSpeed;
					animatorGameObject.eulerAngles = Vector3.Slerp(currentRotation, new Vector3(0,90,0), mTime);
				}
				else
				{

					turning = false;
//					canMove = true;
				}
//				print ("turned right");
//				yield return new WaitForSeconds (0.5f);
//				animatorGameObject.eulerAngles = new Vector3 (0f,90f,0f);
//				facingRight = true;
//				facingLeft = false;
			}
		yield return null;
		}
	}

	IEnumerator OutOfBreathing()
	{
		sprintBreathe = false;
		audioJumpVoice.pitch = Random.Range(0.9f, 1.2f);
		audioJumpVoice.clip = outOfBreathVoice[Random.Range(0,outOfBreathVoice.Length)];
		audioJumpVoice.Play();
		yield return new WaitForSeconds (2f);
		sprintBreathe = true;
	}

	IEnumerator SprintBreathe()
	{
		sprintBreathe = false;
		audioJumpVoice.pitch = Random.Range(0.9f, 1.2f);
		audioJumpVoice.clip = sprintVoice[Random.Range(0,sprintVoice.Length)];
		audioJumpVoice.Play();
		yield return new WaitForSeconds (0.6f);
		sprintBreathe = true;
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

//	IEnumerator ExtraJumpVibrate()
//	{
//		GamePad.SetVibration(player1, 0.1f, 0.3f);
//		yield return new WaitForSeconds (0.3f);
//		GamePad.SetVibration(player1, 0f, 0f);
//	}

	// Kollision for hvis spilleren er i/ude af Water
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = true;
			water.Play();
			water.pitch = Random.Range(0.5f, 1f);
			canSprintOn = false;
			sprintButtonUp = true;
			sprinting = false;
			animChar.StartCoroutine("Watersplash");
		}

		if (hit.gameObject.CompareTag ("ExtraJump")) 
		{
			extraJump = true;
//			StartCoroutine (ExtraJumpVibrate());
		}
	}
	
	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.CompareTag ("Water"))
		{
			inWater = false;
			animChar.StartCoroutine("Watersplash");
			canSprintOn = true;
//			waterSplash.SetActive(false);
		}
	}
}
		



