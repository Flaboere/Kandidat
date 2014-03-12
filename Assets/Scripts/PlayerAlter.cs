using UnityEngine;
using System.Collections;

public class PlayerAlter : MonoBehaviour 
{
	public CharacterMotor charMotor;
	public PlayerMovement playerMovement;
	public float extraSpeedSideways = 50f;
	public bool extraSpeed = true;
	public bool giveJump = false;
	// Use this for initialization
	void Start () 
	{
		charMotor = GameObject.FindObjectOfType<CharacterMotor> ();
		playerMovement = GameObject.FindObjectOfType<PlayerMovement> ();
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.collider.CompareTag ("Player")) 
		{
			if (extraSpeed)
			{
			charMotor.movement.maxSidewaysSpeed += extraSpeedSideways;
			}
			if (giveJump = true)
			{
				playerMovement.canDoubleJump = true;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
