using UnityEngine;
using System.Collections;

public class Vector4Jeppe : MonoBehaviour {
// ved at gøre "v" public, kan jeg bare ændre tallene i Inspectoren
public Vector3 v;

	
	void Start () 
	{
//		Vector3 v = new Vector3(1f,1f,0f);
		
		float length = v.magnitude;
		
		Debug.Log(length);
	}
	
	// Update is called once per frame
	void Update () {
				
	}
}
