using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag ("Kill")) 
		{
			Debug.Log ("Yo");		
		}

	}

}
