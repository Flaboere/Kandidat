using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour 
{
	public Transform spawnpoint;
	public Camera camera;
	private CameraShake camShake;
	public float spawnTimer;
//	private Quaternion cameraRotation;
		// Use this for initialization
	void Start () 
	{
//		cameraRotation = camera.transform.rotation;
		camShake = camera.GetComponent <CameraShake>();
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
			camShake.Shake ();
//			camera.transform.rotation = cameraRotation;

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
