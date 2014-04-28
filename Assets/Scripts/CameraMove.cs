using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	// Hastigheder for camera
	public Vector3 speed;
	public Vector3 acceleration;
	public Vector3 accelRate;
	public GameObject player;
	private float smoothY;
	private float curVel;
	public float smoothTime = 1f;
	public float offSetY;

	// Temp hastigheder for camera
	public Vector3 tempAccel;
	public Vector3 tempAccelRate;

	public bool camMoving = true;
	public bool camStopped = false;

	public float camPause = 1;

	public GameObject spawn;
//	public Vector3 startPos;

	public float maxSpeed = 5f;
	// Use this for initialization
	void Start () 
	{
		spawn = GameObject.Find ("Spawn");
		transform.position = new Vector3 (spawn.transform.position.x, this.transform.position.y, this.transform.position.z);
		tempAccel = acceleration;
		tempAccelRate = accelRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (camMoving)
		{
			smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTime * Time.deltaTime);

	//		acceleration = acceleration + accelRate * Time.deltaTime;
			speed = speed + acceleration * Time.deltaTime;
			transform.position = transform.position + speed * Time.deltaTime;
			transform.position = new Vector3 (transform.position.x, smoothY, transform.position.z);

			if (speed.magnitude > maxSpeed) 
			{
				acceleration = Vector3.zero;
				accelRate = Vector3.zero;
			}

			if (camStopped)
			{
//				camMoving = false;
				StartCoroutine(Camerapause());
			}
		}
	}

	IEnumerator Camerapause()
	{
//		camStopped = false;
//		acceleration = -acceleration;
//		accelRate = -accelRate;
//		speed.x = speed.x/2;
		yield return new WaitForSeconds (camPause);
//		camMoving = true;
//		acceleration = tempAccel;
//		accelRate = tempAccelRate;
	}
}
