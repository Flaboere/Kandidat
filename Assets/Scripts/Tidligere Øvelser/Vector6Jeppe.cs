using UnityEngine;
using System.Collections;

public class Vector6Jeppe : MonoBehaviour {
public float size;
	
	void Start () {
	
	}
	
	
	void Update () 
	{
//		hint: tegn det her lort i et koordinatsystem! Meget nemmere at forstå	
		Debug.DrawLine(new Vector3 (0,0,0)*size, new Vector3 (0,2,0)*size, Color.red);
		Debug.DrawLine(new Vector3 (0,2,0)*size, new Vector3 (2,2,0)*size, Color.red);
		Debug.DrawLine(new Vector3 (2,2,0)*size, new Vector3 (2,0,0)*size, Color.red);
		Debug.DrawLine(new Vector3 (2,0,0)*size, new Vector3 (0,0,0)*size, Color.red);
	}
}
