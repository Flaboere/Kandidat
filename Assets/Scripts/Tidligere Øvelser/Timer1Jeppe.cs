using UnityEngine;
using System.Collections;

public class Timer1Jeppe : MonoBehaviour {
	
	public float timer;
	
	void Start () {
	
		timer = 1000f;
		
	}
	
	
	void Update () {
	
		timer = timer + Time.deltaTime;
	}
}
