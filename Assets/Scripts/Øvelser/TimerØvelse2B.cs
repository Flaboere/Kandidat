using UnityEngine;
using System.Collections;

public class TimerØvelse2B : MonoBehaviour 
{
	public float timer;
	public float MaxTimer = 5f;
	// Use this for initialization
	void Start () 
	{
		timer = MaxTimer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer = timer - Time.deltaTime;
		if (timer < 0f) 
		{
			timer = MaxTimer;
			Debug.Log("Timer er MaxTimer.");
		}
	}
}
