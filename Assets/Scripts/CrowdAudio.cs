using UnityEngine;
using System.Collections;

public class CrowdAudio : MonoBehaviour 
{
	public bool hurdleMiss = false;
//	private AudioSource cheer;
	private AudioSource reaction;
	// Use this for initialization
	void Start () 
	{
		reaction = GameObject.Find ("Reaction").GetComponent<AudioSource> ();
		hurdleMiss = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hurdleMiss)
		{
			HurdleMiss ();
		}
	}

	public void HurdleMiss()
	{
		reaction.Play ();
		hurdleMiss = false;
	}
}
