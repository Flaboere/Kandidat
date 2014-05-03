using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	// Hastigheder for camera
	private Vector3 speed;
	public float acceleration;
	private float accelerationTemp;
	public GameObject player;
	private float smoothY;
	private float speedStop;
	public float speedStopTime;
	private float curVel2;
	private float curVel;
	public float smoothTime = 1f;
	public float offSetY;

	public CharacterRespawn respawn;

	public float camStartWait = 1;
	public float camSpawnWait = 1;
	public bool camMoving = false;

	public float camPause = 1;

	public GameObject spawn;


	public float maxSpeed = 5f;
	// Use this for initialization
	void Start () 
	{
		respawn = GameObject.FindObjectOfType<CharacterRespawn> ();
		spawn = GameObject.Find ("Spawn");
		transform.position = new Vector3 (spawn.transform.position.x, player.transform.position.y + offSetY, this.transform.position.z);
		accelerationTemp = acceleration;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Input.GetButtonDown("Start"))
		{
			StartCoroutine (Camstart());
		}



		if (camMoving)
		{


	//		acceleration = acceleration + accelRate * Time.deltaTime;
			speed.x = speed.x + accelerationTemp * Time.deltaTime;
			smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTime * Time.deltaTime);
			transform.position = new Vector3 (transform.position.x, smoothY, transform.position.z) + speed * Time.deltaTime;
//			transform.position = transform.position + speed * Time.deltaTime;


			if (speed.magnitude > maxSpeed) 
			{
				accelerationTemp = 0f;
			}

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
	}

	IEnumerator Camdead()
	{

		speedStop = Mathf.SmoothDamp (speed.x, 0f, ref curVel2, speedStopTime * Time.deltaTime);
		speed.x = speedStop;
//		camMoving = false;
		yield return new WaitForSeconds (respawn.spawnTimer);
		transform.position = new Vector3 (spawn.transform.position.x, player.transform.position.y + offSetY, this.transform.position.z);
//		camMoving = true;
		accelerationTemp = acceleration;
	}
}
