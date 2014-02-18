using UnityEngine;
using System.Collections;

public class Vector3Jeppe : MonoBehaviour {

	void Start () {
		
		Vector3 a = new Vector3 (1f, 0f, 0f);
		
		Vector3 b = new Vector3 (1f, 2f, 0f);
		
		Vector3 c = a + b;
		
		Debug.Log ("last vector er c = " + c);
	
	}
}