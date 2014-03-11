using UnityEngine;
using System.Collections;

public class PlayerAnim_feet : MonoBehaviour 
{

	Vector3 playerRotate;
	public float rotateAmount = 2;
	public CharacterController player;
	// Use this for initialization
	void Start () 
	{
	
	}
	void FixedUpdate () 
	{

		// Rotation af avatar ved hop
		playerRotate.z = player.velocity.y;
		
		if (player.velocity.x > 0)
		{
			this.transform.rotation = Quaternion.Euler (playerRotate * rotateAmount);
		}
		
		if (player.velocity.x < 0)
		{
			this.transform.rotation = Quaternion.Euler (playerRotate * -rotateAmount);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
