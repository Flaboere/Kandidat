using UnityEngine;
using System.Collections;

public class VectorØvelse2B : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Vector3 v1 = new Vector3 (10f, 10f, 10f);

		Vector3 v2 = new Vector3 (-10f, -10f, -10f);

		Vector3 v3 = v1 + v2;
	
		float length = v3.magnitude;

		Debug.Log (length);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
