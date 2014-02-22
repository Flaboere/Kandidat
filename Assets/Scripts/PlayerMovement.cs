using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool activeMovement = true;
	private CharacterMotor motor;
	void Start () 
	{
		activeMovement = true;
		motor = GetComponent<CharacterMotor>();
	}
	

	void Update () 
	{

			motor.inputMoveDirection = Vector3.right * Input.GetAxis("Horizontal");
			motor.inputJump = Input.GetKey(KeyCode.Joystick1Button0);
//			motor.inputJump = Input.GetKey(KeyCode.Space);
	
	}
	
}
		



