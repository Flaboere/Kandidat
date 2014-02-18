using UnityEngine;
using System.Collections;

public class TimerØvelse2A : MonoBehaviour 
{
	public float timer;
	public float MaxTimer = 5f;
	// Use this for initialization
	void Start () 
	{
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer = timer + Time.deltaTime;
		if (timer > MaxTimer) 
		{
			timer = 0f;
			Debug.Log("Timer was reset.");
		}
	}
}
