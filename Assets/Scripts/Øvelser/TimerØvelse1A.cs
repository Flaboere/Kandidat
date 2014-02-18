using UnityEngine;
using System.Collections;

public class TimerØvelse1A : MonoBehaviour 
{
	public float timer;
	// Use this for initialization
	void Start () {
	
		timer = 1000f;
	}
	
	// Update is called once per frame
	void Update () {
	
		timer = timer + Time.deltaTime;
	}
}
