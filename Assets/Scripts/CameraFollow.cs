using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	// Hastigheder for camera
//	private Vector3 speed;
//	public float acceleration;
//	private float accelerationTemp;
	public GameObject player;
	private float smoothY;
	private float smoothX;
//	private float speedStop;
//	public float speedStopTime;
	private float curVel2;
	private float curVel;
	public float smoothTime = 1f;
	public float offSetY;
	public float offSetX;
	private float tempY;
	
	public CharacterRespawn respawn;
	
	public float camStartWait = 1;
	public float camSpawnWait = 1;
	public bool camMoving = false;
	
	public float camPause = 1;
	
	public GameObject spawn;
	public CharacterMotor motor;
	public PlayerMovement move;
	
	
	
	
	public float maxSpeed = 5f;
	// Use this for initialization
	void Start () 
	{
		respawn = GameObject.FindObjectOfType<CharacterRespawn> ();
		spawn = GameObject.Find ("Spawn");
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
		move = GameObject.FindObjectOfType<PlayerMovement> ();
		
		transform.position = new Vector3 (spawn.transform.position.x + offSetX, this.transform.position.y, this.transform.position.z);
//		accelerationTemp = acceleration;
//		tempY = spawn.transform.position.y;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

			//		acceleration = acceleration + accelRate * Time.deltaTime;
//			speed.x = speed.x + accelerationTemp * Time.deltaTime;
			
			if (motor.grounded && !move.inWater)
			{
				smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTime * Time.deltaTime);
			}
			else
			{
				smoothY = this.transform.position.y;
			}
			smoothX = Mathf.SmoothDamp (this.transform.position.x, player.transform.position.x, ref curVel2, smoothTime * Time.deltaTime);
			transform.position = new Vector3 (smoothX, smoothY, transform.position.z);
			//			transform.position = transform.position + speed * Time.deltaTime;
			
			
//			if (speed.magnitude > maxSpeed) 
//			{
//				accelerationTemp = 0f;
//			}
			
			
			
		
	}
	




	// Gamle kamera
////	private float lockedZ;
//	public Transform player;
//
//	private float curVel;
//	private float curVel2;
//	public float offSetY;
//	public float offSetX;
//
//	public float speed;
//	private Vector3 refPos;
//	// Use this for initialization
//	void Start () 
//	{
//		refPos = this.transform.position;
//	}
//	
//	// Update is called once per frame
//	void FixedUpdate () 
//	{
//		refPos.x = Mathf.SmoothDamp (this.transform.position.x, player.position.x + offSetX, ref curVel, Time.deltaTime * speed);
//		refPos.y = Mathf.SmoothDamp (this.transform.position.y, player.position.y + offSetY, ref curVel2, Time.deltaTime * speed);
//
//		transform.position = refPos;
//		//		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, lockedZ);
////		transform.position = new Vector3 (Mathf.SmoothDamp (this.transform.position.x, player.transform.position.x, ref curVel, Time.deltaTime * speed), Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y, ref curVel2, Time.deltaTime * speed), lockedZ);
//	}
}
