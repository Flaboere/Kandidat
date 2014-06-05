using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	[HideInInspector]
	public GameObject player;
	public Transform playerTarget;

	[HideInInspector]
	public CharacterRespawn respawn;

	[HideInInspector]
	public GameObject spawn;

	[HideInInspector]
	public CharacterMotor motor;

	[HideInInspector]
	public PlayerMovement move;
	
	// Hastigheder for camera
	private Vector3 speed;
	public float acceleration;
	private float accelerationTemp;
	public float maxSpeed = 5f;
	private float maxSpeedTemp;
	public float fastSpeed = 9f;

	// Position i Y og Z
	private float smoothY;
	private float smoothZ;

	// Stoppe hastighed
	private float speedStop;
	public float stopTime;

	private float curVel6;
	private float curVel5;
	private float curVel4;
	private float curVel3;
	private float curVel2;
	private float curVel;

	// Smooth hastigheder
	public float smoothTimeY = 1f;
	public float smoothTimeZ = 0.3f;
	public float smoothTimeSpeed = 1.5f;

	// Posititioner og offsets
	public float offSetY;
	public float offSetX;
	public float playerInFront;

	private float originZ;
	public float newPosZ;
	private float newPosZTemp;

	// Rotation
	public float rotationSmoothTime = 20f;
	private Vector3 velocity;
	private Vector3 targetRotation;

	private bool canRotate = true;
	private Vector3 rotation;
	public Quaternion lookAtRotation;
	private Vector3 originRotation;
	public Vector3 aheadRotation;
	public float damping = 5;
	
	public float camStartWait = 1;

	private bool camMoving = false;
	private bool windUp = false;
	private bool camActive = false;
	
	private bool ahead = false;
	private bool behind = false;

	// audio
	private AudioSource audioStart;
	public AudioClip startLow;
	public AudioClip startHigh;

	// Use this for initialization
	void Start () 
	{
		respawn = GameObject.FindObjectOfType<CharacterRespawn> ();
		spawn = GameObject.Find ("Spawn");
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
		move = GameObject.FindObjectOfType<PlayerMovement> ();

		// audio
		audioStart = GameObject.Find ("audio_start").GetComponent<AudioSource> ();
		
//		originRotation = this.transform.eulerAngles;

//		transform.position = new Vector3 (spawn.transform.position.x + offSetX, this.transform.position.y, this.transform.position.z);
		accelerationTemp = acceleration;

		maxSpeedTemp = maxSpeed;

		smoothZ = transform.position.z;
		originZ = transform.position.z;

		StartCoroutine (StartSequence ());

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
//		if (Input.GetButtonDown("X"))
//		{
//			StartCoroutine (Startcam());
//		}

		// Print ting her:


		// Holder øje med hvor spilleren er forhold til kameraet
		if (player.transform.position.x + playerInFront > transform.position.x)
		{
			ahead = true;
			behind = false;
		}
		
		if (player.transform.position.x + playerInFront < transform.position.x)
		{
			ahead = false;
			behind = true;
		}

		if (camMoving)
		{
			// Kameraets Rotation
			if (canRotate)
			{
//				transform.eulerAngles = new Vector3((Mathf.Clamp(transform.eulerAngles.x, -30f, 90f)),(Mathf.Clamp(transform.eulerAngles.y, -30f, 90f)), (Mathf.Clamp(transform.eulerAngles.z, -30f, 90f)));

				lookAtRotation = Quaternion.LookRotation(playerTarget.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * damping);

//				transform.rotation = Quaternion.Euler(Vector3.Slerp(transform.eulerAngles, lookAtRotation.eulerAngles, Time.deltaTime * damping));

//				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, lookAtRotation.eulerAngles, Time.deltaTime * damping);

//				if (ahead)
//				{
//
//				}

//				if (behind)
//				{

//				}

//				transform.eulerAngles = rotation;
//				targetRotation = aheadRotation;
//
//				if (ahead)
//				{
//					rotation = Vector3.SmoothDamp (rotation, targetRotation, ref velocity, rotationSmoothTime * Time.deltaTime);
//				}
//				if (behind)
//				{
//					rotation = Vector3.SmoothDamp (rotation, originRotation, ref velocity, rotationSmoothTime * Time.deltaTime);
//				}
			}

//			rotation = Quaternion.LookRotation(playerTarget.position - transform.position);
//			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

			// Kameraets Y position
			if (motor.grounded && !move.inWater)
			{
				smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTimeY * Time.deltaTime);
			}
			else
			{
				smoothY = this.transform.position.y;
			}

			// Kameraets Z position
			newPosZTemp = originZ - newPosZ;

			if (ahead && !behind)
			{
				smoothZ = Mathf.SmoothDamp (transform.position.z, newPosZTemp, ref curVel5, smoothTimeZ * Time.deltaTime);
			}

			if (!ahead && behind)
			{
				smoothZ = Mathf.SmoothDamp (transform.position.z, originZ, ref curVel6, smoothTimeZ * Time.deltaTime);
			}
		
			// Bevæger kameraet
			transform.position = new Vector3 (transform.position.x, smoothY, smoothZ) + speed * Time.deltaTime;

			// Kameraet starter, og når tophastighed
			if (windUp)
			{
				speed.x = (speed.x + accelerationTemp * Time.deltaTime);

				if (speed.x > maxSpeed)
				{
					StartCoroutine(Camstartet());
				}
			}

			// Ændrer kamera speed alt efter hvor karakter er i forhold til kamera
			if (camActive)
			{
				if (behind) 
				{
					speed.x = Mathf.SmoothDamp(speed.x, maxSpeedTemp, ref curVel3, smoothTimeSpeed * Time.deltaTime);
				}
				if (ahead) 
				{
					speed.x = Mathf.SmoothDamp(speed.x, fastSpeed, ref curVel4, smoothTimeSpeed * Time.deltaTime);

				}
			}

			if (respawn.dead == true)
			{
				StartCoroutine (Camdead());
			}

		}
	}

	IEnumerator StartSequence()
	{
		yield return new WaitForSeconds (1.5f);
		audioStart.Play ();
		yield return new WaitForSeconds (1.5f);
		audioStart.Play ();
		yield return new WaitForSeconds (1.5f);
		audioStart.Play ();
		yield return new WaitForSeconds (1.5f);
		audioStart.clip = startHigh;
		audioStart.Play ();
		yield return new WaitForSeconds (1f);
		camMoving = true;
		move.canMove = true;
		windUp = true;
	}

	// Kameraet starter
	IEnumerator Startcam()
	{
		yield return new WaitForSeconds (camStartWait);
		camMoving = true;
		windUp = true;
	}

	// Kameraet har nået tophastighed
	IEnumerator Camstartet ()
	{
		windUp = false;
		camActive = true;
		yield return new WaitForSeconds (0f);

	}

	// Stopper kamera når spiller dør
	IEnumerator Camdead()
	{
		canRotate = false;
		lookAtRotation = Quaternion.LookRotation(playerTarget.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * damping);
		camActive = false;
		speedStop = Mathf.SmoothDamp (speed.x, 0f, ref curVel2, stopTime * Time.deltaTime);
		speed.x = speedStop;
		yield return new WaitForSeconds (respawn.spawnTimer);
	}
}
