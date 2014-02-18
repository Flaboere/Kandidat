using UnityEngine;
using System.Collections;

public class Vector2Jeppe : MonoBehaviour {

	void Start () {
		
		Vector3 a = new Vector3 (1f, 0f, 0f);
		
		Vector3 b = new Vector3 (0f, 2f, 0f);
		
		Vector3 c = new Vector3 (0f, 0f, 3f);
		
		Vector3 d = a + b + c;
		
		Debug.Log ("d = " + d);
	
	}
}