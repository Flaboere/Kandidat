using UnityEngine;
using System.Collections;

public class VectorØvelse1C : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Vector3 a = new Vector3 (1f, 0f, 0f);

		Vector3 b = new Vector3 (1f, 2f, 0f);

		Vector3 c = a + b;

		Debug.Log ("Resultat Øv1C = " + c);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
