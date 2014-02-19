using UnityEngine;
using System.Collections;

public class InputØvelse1A : MonoBehaviour 
{
	public int counter;
	public int adder;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			counter = counter + adder;
			Debug.Log ("Up is down");
			Debug.Log (counter);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) 
		{
			counter-= 1;
			Debug.Log ("Down is down");
			Debug.Log (counter);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) 
		{
			
			Debug.Log ("Right is down");
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) 
		{
			Debug.Log ("Left is down");
		}
	}
}
