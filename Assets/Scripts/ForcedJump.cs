using UnityEngine;
using System.Collections;

public class ForcedJump : MonoBehaviour 
{

	public float force = 1;

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnTriggerStay (Collider other)
	{
		other.rigidbody.AddForce (Vector3.up * force, ForceMode.Force);
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
