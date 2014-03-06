using UnityEngine;
using System.Collections;

public class CharacterRespawn : MonoBehaviour 
{
	public Transform spawnpoint;
	public float spawnTimer;
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
			StartCoroutine (Dead());
		}

		if (hit.collider.CompareTag ("NextLevel"))
		{
			Application.LoadLevel ("Victory");
		}
		
	}
		

	IEnumerator Dead()
	{
		Debug.Log ("About to wait");
		renderer.enabled = false;
		yield return new WaitForSeconds (spawnTimer);
		transform.position = spawnpoint.position;
		renderer.enabled = true;
		Debug.Log ("Yo");
	}

}
