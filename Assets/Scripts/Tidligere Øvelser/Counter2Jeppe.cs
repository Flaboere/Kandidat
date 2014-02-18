using UnityEngine;
using System.Collections;

public class Counter2Jeppe : MonoBehaviour {
	
	public float counter;
	
	void Start () {
	
		counter = 2;
	
	}
	
	
	void Update () {
	
		counter = counter * 2;
		
		if (counter > 2000)
		{
			counter = 2;
		}
	}
}
