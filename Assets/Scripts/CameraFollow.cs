using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
//	private float lockedZ;
	public Transform player;

	private float curVel;
	private float curVel2;
	public float offSetY;
	public float offSetX;

	public float speed;
	private Vector3 refPos;
	// Use this for initialization
	void Start () 
	{
		refPos = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		refPos.x = Mathf.SmoothDamp (this.transform.position.x, player.position.x + offSetX, ref curVel, Time.deltaTime * speed);
		refPos.y = Mathf.SmoothDamp (this.transform.position.y, player.position.y + offSetY, ref curVel2, Time.deltaTime * speed);

		transform.position = refPos;
		//		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, lockedZ);
//		transform.position = new Vector3 (Mathf.SmoothDamp (this.transform.position.x, player.transform.position.x, ref curVel, Time.deltaTime * speed), Mathf.SmoothDamp (this.transform.position.y, player.transform.position.y, ref curVel2, Time.deltaTime * speed), lockedZ);
	}
}
