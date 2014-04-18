using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	public Vector3 speed;
	public Vector3 acceleration;
	public Vector3 accelRate;
	public GameObject player;
	private float smoothY;
	private float curVel;
	public float smoothTime = 1f;
	public float offSetY;

	public float maxSpeed = 5f;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		smoothY = Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y + offSetY, ref curVel, smoothTime * Time.deltaTime);

		acceleration = acceleration + accelRate * Time.deltaTime;
		speed = speed + acceleration * Time.deltaTime;
		transform.position = transform.position + speed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x, smoothY, transform.position.z);

		if (speed.magnitude > maxSpeed) 
		{
			acceleration = Vector3.zero;
			accelRate = Vector3.zero;
		}
	}
}
