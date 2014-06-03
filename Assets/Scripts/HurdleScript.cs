using UnityEngine;
using System.Collections;

public class HurdleScript : MonoBehaviour 
{

//	public bool activated = false;
	public bool dead = false;

	public int scoreAdd = 100;
	public int scoreRemove = 75;

	public Renderer[] renderers;
	public ParticleSystem add;
	public ParticleSystem remove;

	public CameraMove camMove;

	public float breakAngle;

	public PlayerMovement move;

	public CharacterMotor motor;
	public CharacterController controller;
	private float pushedForce;
	private bool playerTouching = false;
	public bool pushed = false;
	public float pushAmp = 0f;
	public float vertPushAmp = 10f;


	public Score score;
	// Use this for initialization
	void Start () 
	{
		controller = GameObject.FindObjectOfType<CharacterController> ();
		move = GameObject.FindObjectOfType<PlayerMovement>();
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
		camMove = GameObject.FindObjectOfType<CameraMove> ();
		score = GameObject.FindObjectOfType<Score>();
		renderers = GetComponentsInChildren<Renderer> ();
//		StartCoroutine (Activation ());
	}
	
	// Update is called once per frame
	void Update () 
	{


//		pushedForce = 1000f;
//		pushedForce = -pushedForce;

		// "Dør" hvis den vælter
		if (Vector3.Dot(transform.up, Vector3.up) < breakAngle && !dead)
		{
			dead = true;
			score.score -= scoreRemove;
			score.penalty -=1;
			score.StartCoroutine("Penaltypoints");
		}


		// Slukker renderer hvis den dør
		if (dead)
		{
			foreach(Renderer r in renderers)
			{
				if (r != this.renderer)
				{
				r.enabled = false;
				}
			}
		}

//		//Hvis spilleren står ovenpå hurdle
//		if (playerTouching == true)
//		{
//			StartCoroutine (PlayerTouching());
//		}
	}


//	IEnumerator Activation ()
//	{
//		yield return new WaitForSeconds (2.0f);
//		activated = true;
//	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.gameObject.CompareTag ("Player"))
		{
			if (!dead)
			{
				score.StartCoroutine("Scorepoints");
				score.score += scoreAdd;
				dead = true;
			}

		}

	}
	void OnTriggerStay (Collider hit)
	{
		if (hit.gameObject.CompareTag ("Player"))
		{

			if (move.playerTouching == true && !pushed)
			{
				Playertouching();
			}
		}
		
	}

	void Playertouching ()
	{
		if (motor.movement.velocity.x < 1 && motor.movement.velocity.x > -1)
		{
			pushedForce = (motor.movement.velocity.x + vertPushAmp) * pushAmp;
		}
		else
		{
			pushedForce = motor.movement.velocity.x * pushAmp;
		}

		rigidbody.AddForce (pushedForce, 0f, 0f);
		pushed = true;

//		playerTouching = false;
	}


//	IEnumerator PlayerTouching()
//	{
//		rigidbody.AddForce (pushedForce, 0f, 0f);
//		yield return new WaitForSeconds (0.2f);
////		playerTouching = false;
//
//	}



}
