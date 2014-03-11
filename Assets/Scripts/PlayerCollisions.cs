using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour 
{
	private CharacterMotor charMotor;
	private PlayerMovement playerMovement;
	public float extraSpeedSideways = 50f;
	// Use this for initialization
	void Start () 
	{
		charMotor = GetComponent<CharacterMotor> ();
		playerMovement = GetComponent<PlayerMovement> ();
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.collider.CompareTag ("ModCube")) 
		{
			charMotor.movement.maxSidewaysSpeed = extraSpeedSideways;
			print ("speeeeed");
		}


		if (hit.collider.CompareTag ("ExtraJump")) 
		{
			playerMovement.canDoubleJump = true;
			print ("JumpJump");
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
