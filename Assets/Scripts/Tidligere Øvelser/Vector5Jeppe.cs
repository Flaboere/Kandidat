using UnityEngine;
using System.Collections;

public class Vector5Jeppe : MonoBehaviour {
// ved at gøre "v" public, kan jeg bare ændre tallene i Inspectoren
public Vector3 v;

	
	void Start () 
	{
//		Vector3 v = new Vector3(1f,1f,0f);
		
//		Sådan her regner jeg (10,10,10) - (-10,-10,10) ud nemt. Ændrer bare v's værdi i inspector		
		float length = v.magnitude + -v.magnitude;
		
		
		Debug.Log(length);
	}
	
	// Update is called once per frame
	void Update () {
				
	}
}
