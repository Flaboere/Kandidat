using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour 
{

	Vector3 playerRotate;
	Vector3 playerRunRotate;
	public float rotateAmountJump = 2;
	public float rotateAmountRun = 2;
//	public bool runAnim = true;
//	public bool jumpAnim = true;
	public CharacterController controler;
	public CharacterMotor motor;
	public PlayerMovement move;
	// Use this for initialization
	void Start () 
	{
		controler = transform.parent.GetComponent<CharacterController> ();
		motor = transform.parent.GetComponent<CharacterMotor> ();
		move = transform.parent.GetComponent<PlayerMovement> ();
		playerRotate.y = 90f;
		playerRunRotate.y = 90f;
	}
	void FixedUpdate () 
	{


		// Rotation af avatar ved velocity i y, dvs. den gør det faktisk hele tiden, men hvis y er 0 ganger den bare med 0 og ingen effekt
		playerRotate.z = controler.velocity.y;
		playerRunRotate.z = controler.velocity.x;

		if (motor.grounded == false)
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
		if (motor.grounded == true)
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

		if (motor.grounded && controler.velocity.x == 0)
		{
			this.transform.rotation = Quaternion.Euler (0,0,0);
		}
//		print (motor.grounded);
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
