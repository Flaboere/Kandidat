using UnityEngine;
using System.Collections;

public class Move1Jeppe : MonoBehaviour {

	void Start () {
	
		transform.position = new Vector3 (0f, 10f, 10f);
	}
	
	
	void Update () {
	
		if (transform.position == new Vector3 (0f, 10f, 10f))
		{
			transform.position = new Vector3 (10f, 10f, 10f);
		}
			
	}
}
