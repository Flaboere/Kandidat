using UnityEngine;
using System.Collections;

public class CounterØvelseB : MonoBehaviour {

	public int counter;
	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter = counter * 2;
		if (counter > 2000) 
		{
			counter = 2;
		}
	}
}
// tæller hver update