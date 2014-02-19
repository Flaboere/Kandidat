using UnityEngine;
using System.Collections;

public class VectorØvelse4B : MonoBehaviour 
{
//	public float scale;

	// Use this for initialization
	void Start () 
	{
		Vector3 v = new Vector3 (1, 2, 4);

		v = v.normalized;

		Debug.Log (v.magnitude);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
