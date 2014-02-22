using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour 
{
	public Transform spawnpoint;
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
			transform.position = spawnpoint.position;
			Debug.Log ("Yo");
		}
		
	}

}
