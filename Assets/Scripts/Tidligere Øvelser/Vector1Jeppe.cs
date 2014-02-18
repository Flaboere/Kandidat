using UnityEngine;
using System.Collections;

public class Vector1Jeppe : MonoBehaviour {

	void Start () {
		
		Vector3 a = new Vector3 (0f, 5f, 0f);
		
		Vector3 b = new Vector3 (5f, 0f, 0f);
		
		Vector3 c = a - b;
		
		Debug.Log ("c = " + c);
	
	}
}