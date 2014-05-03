using UnityEngine;
using System.Collections;

public class HurdleScript : MonoBehaviour 
{

	public bool activated = false;
	public bool dead = false;

	public int scoreAdd = 100;
	public int scoreRemove = 75;

	public Renderer[] renderers;

	public CameraMove camMove;

	public float breakAngle;

	public Score score;
	// Use this for initialization
	void Start () 
	{
		camMove = GameObject.FindObjectOfType<CameraMove> ();
		score = GameObject.FindObjectOfType<Score>();
		renderers = GetComponentsInChildren<Renderer> ();
		StartCoroutine (Activation ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		// "Dør" hvis den vælter
		if (Vector3.Dot(transform.up, Vector3.up) < breakAngle && !dead)
		{
			dead = true;
			score.score -= scoreRemove;
			score.penalty -=1;
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

	IEnumerator Activation ()
	{
		yield return new WaitForSeconds (2.0f);
		activated = true;
	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.gameObject.CompareTag ("Player"))
		{
			if (!dead)
			{
				score.score += scoreAdd;		
				dead = true;
			}
		}

	}




}
