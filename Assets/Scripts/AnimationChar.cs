using UnityEngine;
using System.Collections;

public class AnimationChar : MonoBehaviour 
{

	public ParticleSystem dustWalk;

	// Use this for initialization
	void Start () 
	{
		dustWalk = GameObject.Find ("dustWalk").GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FootStep ()
	{


	}
}
