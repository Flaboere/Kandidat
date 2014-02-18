using UnityEngine;
using System.Collections;

public class Timer2Jeppe : MonoBehaviour {

	public float timer;
	public float MaxTimer = 5f;
	
	void Start () {
		
		timer = MaxTimer;
	
	}
	
	
	void Update () {
		timer = timer - Time.deltaTime;
//		når der står (int) betyder det at den tæller hele tal og runder op og ned - derfor siger den bare,
//		at når timer er = 0.450 er timer = 0
		if (timer < 0f)
		{
			timer = 5f;
			Debug.Log ("Timer was reset, dit fjols!");
		}
	}
}
