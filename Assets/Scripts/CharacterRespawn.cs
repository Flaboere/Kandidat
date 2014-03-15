using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterRespawn : MonoBehaviour 
{
	public Transform spawnpoint;
	public float spawnTimer;

	PlayerIndex player1 = PlayerIndex.One;

		// Use this for initialization
	void Start () 
	{
		transform.position = spawnpoint.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

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
		GamePad.SetVibration(player1, 0.2f, 0.4f);
		yield return new WaitForSeconds (0.1f);
		GamePad.SetVibration(player1, 0.0f, 0.0f);
		renderer.enabled = false;
		yield return new WaitForSeconds (spawnTimer);
		transform.position = spawnpoint.position;
		renderer.enabled = true;
	}

}
