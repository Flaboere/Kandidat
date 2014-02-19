using UnityEngine;
using System.Collections;

public class VectorØvelse2A : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Vector3 v = new Vector3 (3f, 4f, 0f);
	
		float length = v.magnitude;

		Debug.Log (length);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
