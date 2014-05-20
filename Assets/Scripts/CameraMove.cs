using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	// Hastigheder for camera
	private Vector3 speed;
	public float acceleration;
	public float accelerationTemp;
	public GameObject player;
	private float smoothY;
	private float speedStop;
	public float speedStopTime;
	private float curVel4;
	private float curVel3;
	private float curVel2;
	private float curVel;
	public float smoothTime = 1f;
	public float offSetY;
	public float offSetX;
	private float tempY;
	public float posX;
	public float playerOffsetX;
	public float playerInFront;
	public float playerBehind;
	public float speedMulti;
	public float speedChange = 1.5f;


	public CharacterRespawn respawn;

	public float camStartWait = 1;
//	public float camSpawnWait = 1;

	public bool camMoving = false;
	public bool dynamicCam = true;
	public bool staticCam = false;
	private bool staticCamTemp = false;
	private bool camStarting = false;
	private bool camStarted = false;

//	public float camPause = 1;

	public GameObject spawn;
	public CharacterMotor motor;
	public PlayerMovement move;
	
	public float maxSpeed = 5f;
	public float maxSpeedTemp;
	public float fastSpeed = 9f;

	public bool ahead = false;
	public bool behind = false;
	// Use this for initialization
	void Start () 
	{
		respawn = GameObject.FindObjectOfType<CharacterRespawn> ();
		spawn = GameObject.Find ("Spawn");
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
		move = GameObject.FindObjectOfType<PlayerMovement> ();

		if (staticCam)
		{
			dynamicCam = false;
		}

		transform.position = new Vector3 (spawn.transform.position.x + offSetX, this.transform.position.y, this.transform.position.z);
		accelerationTemp = acceleration;
		tempY = spawn.transform.position.y;
		maxSpeedTemp = maxSpeed;


	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetButtonDown("Start"))
		{
			StartCoroutine (Camstart());
		}

		print (speed.x);

		if (camMoving)
		{


	//		acceleration = acceleration + accelRate * Time.deltaTime;


			if (motor.grounded && !move.inWater)
			{
				smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTime * Time.deltaTime);
			}
			else
			{
				smoothY = this.transform.position.y;
			}
		
			transform.position = new Vector3 (transform.position.x, smoothY, transform.position.z) + speed * Time.deltaTime;
//			transform.position = transform.position + speed * Time.deltaTime;

			// Ændrer max hastighed alt efter hvor langt fra kameraet spilleren er
			if (player.transform.position.x - playerInFront > transform.position.x)
			{
				maxSpeedTemp = speedMulti;
				ahead = true;
				behind = false;
			}
//			if (player.transform.position.x - playerInFront < transform.position.x)
//			{
//				maxSpeedTemp = maxSpeed;
//				ahead = false;
//				behind = true;
//			}

			if (player.transform.position.x - playerInFront < transform.position.x)
			{
				maxSpeedTemp = maxSpeed;
				ahead = false;
				behind = true;
			}

			if (staticCamTemp)
			{
				speed.x = (speed.x + accelerationTemp * Time.deltaTime);
				if (speed.x > maxSpeed)
				{
					accelerationTemp = 0f;
				}
			}
			
			if (camStarting)
			{
				speed.x = (speed.x + accelerationTemp * Time.deltaTime);

				if (speed.x > maxSpeed)
				{
					StartCoroutine(Camstartet());
				}
//				accelerationTemp = acceleration;
			}
			
			
			

//			else
//			{
//				maxSpeedTemp = maxSpeed;
//				ahead = false;
//				behind =  false;
//			}

			// Slår acceleration fra og til alt efter om maxhastighed er nået
			if (camStarted)
			{
				if (speed.x > maxSpeedTemp) 
				{
	//				speed.x = maxSpeedTemp;
					speed.x = Mathf.SmoothDamp(speed.x, maxSpeedTemp, ref curVel3, speedChange * Time.deltaTime);
				}
				if (speed.x < maxSpeedTemp) 
				{
					speed.x = Mathf.SmoothDamp(speed.x, fastSpeed, ref curVel4, speedChange * Time.deltaTime);

				}
			}
//			if (speed.x < maxSpeedTemp && maxSpeedTemp > maxSpeed) 
//			{
//				accelerationTemp = -2f;
//			}
//			if (speed.x < maxSpeedTemp && maxSpeedTemp < maxSpeed) 
//			{
//				accelerationTemp = 0f;
//			}

			if (respawn.dead == true)
			{
				StartCoroutine (Camdead());
			}

		}
	}

	IEnumerator Camstart()
	{
		yield return new WaitForSeconds (camStartWait);
		camMoving = true;
		if (dynamicCam)
		{
			camStarting = true;
		}
		if (staticCam)
		{
			staticCamTemp = true;
		}


	}

	IEnumerator Camstartet ()
	{
		camStarting = false;
		camStarted = true;
		yield return new WaitForSeconds (0f);

	}

	// Stopper kamera når spiller dør
	IEnumerator Camdead()
		{
			accelerationTemp = 0f;
			speedStop = Mathf.SmoothDamp (speed.x, 0f, ref curVel2, speedStopTime * Time.deltaTime);
			speed.x = speedStop;
			yield return new WaitForSeconds (respawn.spawnTimer);
		}

	// Respawner spiller (virker med korrekt Dead() i CharacterRespawn
//	IEnumerator Camdead()
//	{
//
//		speedStop = Mathf.SmoothDamp (speed.x, 0f, ref curVel2, speedStopTime * Time.deltaTime);
//		speed.x = speedStop;
//		yield return new WaitForSeconds (respawn.spawnTimer);
//		transform.position = new Vector3 (spawn.transform.position.x, player.transform.position.y + offSetY, this.transform.position.z);
//		accelerationTemp = acceleration;
//	}
}
