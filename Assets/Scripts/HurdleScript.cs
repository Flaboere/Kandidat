using UnityEngine;
using System.Collections;

public class HurdleScript : MonoBehaviour 
{

	public bool dead = false;

	public Renderer[] renderers;

	public float breakAngle;

	public Score score;
	// Use this for initialization
	void Start () 
	{
		score = GameObject.FindObjectOfType<Score>();
		renderers = GetComponentsInChildren<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// "Dør" hvis den vælter
		if (!gameObject.CompareTag ("Ceiling"))
		{
			if (transform.rotation.z > breakAngle && !dead || transform.rotation.z < -breakAngle && !dead)
			{
				dead = true;
				score.score -= 1;
			}
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
	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.gameObject.CompareTag ("Player"))
		{
			if (!dead)
			{
				score.score += 1;		
				dead = true;
			}
		}

	}

//	void OnControllerColliderHit (Controller hit)
//	{
//		if (hit.collider.CompareTag ("Ceiling"))
//	}


}
