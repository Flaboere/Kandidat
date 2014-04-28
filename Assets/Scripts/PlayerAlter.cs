using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerAlter : MonoBehaviour 
{
	public CharacterMotor charMotor;
	public PlayerMovement playerMovement;


	public bool giveJump = false;
	public bool forcedJump = false;

	PlayerIndex player1 = PlayerIndex.One;

	// Use this for initialization
	void Start () 
	{
		charMotor = GameObject.FindObjectOfType<CharacterMotor> ();
		playerMovement = GameObject.FindObjectOfType<PlayerMovement> ();

	}


	// Update is called once per frame
	void Update () 
	{
		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);
	}




	void OnTriggerEnter(Collider hit)
	{
		if (hit.collider.CompareTag ("Player")) 
		{
			if (giveJump == true)
			{
//				playerMovement.canDoubleJump = true;
				playerMovement.extraJump = true;
				StartCoroutine (Vibrate());
			}
			if (forcedJump)
			{
				charMotor.inputJump = true;
			}

		}
	}

	IEnumerator Vibrate()
	{
		GamePad.SetVibration(player1, 0.1f, 0.3f);
		yield return new WaitForSeconds (0.3f);
		GamePad.SetVibration(player1, 0.0f, 0.0f);
	}
}
