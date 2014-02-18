using UnityEngine;
using System.Collections;

public class Counter1Jeppe : MonoBehaviour {

	public int counter;
	
	void Start () {
	
		counter = 0;
		
	}
	
	
	void Update () {
	
		counter = counter +10;
	}
}
