using UnityEngine;
using System.Collections;

public class AnimationChar : MonoBehaviour 
{
	public CharacterMotor motor;
	public PlayerMovement playermov;
	public ParticleSystem dustWalk;
	public ParticleSystem dustLand;
	public ParticleSystem sweat;
	public ParticleSystem splash;
	private bool landed;
	private bool jumped;
	public float landWait = 0.5f;
	public float jumpWait = 0.2f;

	// Use this for initialization
	void Start () 
	{
		motor = GameObject.FindObjectOfType<CharacterMotor> ();
		playermov = GameObject.FindObjectOfType<PlayerMovement>();
		dustWalk = GameObject.Find ("dustWalk").GetComponent<ParticleSystem> ();
		dustLand = GameObject.Find ("dustLand").GetComponent<ParticleSystem> ();
		sweat = GameObject.Find ("Particle_sweat").GetComponent<ParticleSystem> ();
		splash = GameObject.Find ("Particle_water").GetComponent<ParticleSystem> ();

		dustWalk.enableEmission = false;
		dustLand.enableEmission = false;
		sweat.enableEmission = false;
		splash.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!motor.grounded)
		{
			dustWalk.enableEmission = false;
			landed = true;
		}
		if (motor.grounded)
		{
			jumped = false;
		}
		if (motor.grounded && landed)
		{
			StartCoroutine(Landed());
		}
		if (!motor.grounded && !jumped)
		{
			StartCoroutine(Jumped());
		}

		if (playermov.inWater)
		{
			landed = false;
			jumped = true;
		}
	}

	IEnumerator Landed()
	{
		dustLand.enableEmission = true;
		yield return new WaitForSeconds (landWait);
		dustLand.enableEmission = false;
		landed = false;
	}
	IEnumerator Jumped()
	{
		dustLand.enableEmission = true;
		sweat.enableEmission = true;
		yield return new WaitForSeconds (jumpWait);
		dustLand.enableEmission = false;
		sweat.enableEmission = false;
		jumped = true;

	}
	public IEnumerator Watersplash()
	{
		splash.enableEmission = true;
		yield return new WaitForSeconds (jumpWait);
		splash.enableEmission = false;

	}

	void FootStep ()
	{
		if (motor.grounded && !playermov.inWater)
		{
			dustWalk.enableEmission = true;
		}
//		if (!playermov.inWater)
//		{
			playermov.FootStep ();
//		}
	}
	void FootStepOff()
	{
		dustWalk.enableEmission = false;

//		if (!playermov.inWater)
//		{
//			playermov.FootStep ();
//		}
	}
}
