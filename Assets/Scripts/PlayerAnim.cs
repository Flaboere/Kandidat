using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour 
{

	Vector3 playerRotate;
	Vector3 playerRunRotate;
	public float rotateAmountJump = 2;
	public float rotateAmountRun = 2;
	// Use this for initialization
	void Start () 
	{
	
	}
	void FixedUpdate () 
	{
		CharacterController controler = GetComponent<CharacterController> ();
		CharacterMotor motor = GetComponent<CharacterMotor> ();
		PlayerMovement move = GetComponent<PlayerMovement> ();

		// Rotation af avatar ved velocity i y, dvs. den gør det faktisk hele tiden, men hvis y er 0 ganger den bare med 0 og ingen effekt
		playerRotate.z = controler.velocity.y;
		playerRunRotate.z = controler.velocity.x;

		if (move.myGrounded = false)
		{
			if (controler.velocity.x > 0)
			{
				this.transform.rotation = Quaternion.Euler (playerRotate * rotateAmountJump);
			}
				
			if (controler.velocity.x < 0)
			{
				this.transform.rotation = Quaternion.Euler (playerRotate * -rotateAmountJump);
			}
		}

		// Dette skal få player til at rotere når den løber, men reagerer også i luften; hjælp!
		if (move.myGrounded = true)
		{
			if (controler.velocity.x > 0)
			{
				this.transform.rotation = Quaternion.Euler (playerRunRotate * rotateAmountRun);
			}


			if (controler.velocity.x < 0)
			{
				this.transform.rotation = Quaternion.Euler (playerRunRotate * rotateAmountRun);
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
