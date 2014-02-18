using UnityEngine;
using System.Collections;

public class Move2Jeppe : MonoBehaviour {
	
//	public Vector3 startPosition;
//	public float startPositionX = 5f;
	public Vector3 startPosition = new Vector3 (0f, 10f, 10f);
	
	void Start () {
	 
//		startPosition = new Vector3 (startPositionX,0f,0f);
		transform.position = startPosition;
	}
	
	
	void Update () {
	
	}
}
