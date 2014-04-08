using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterRespawn : MonoBehaviour 
{
	public Transform spawnpoint;
	public float spawnTimer;

	public int deathScorePen = 1;
	public Score score;

	private bool dead = false;

	public PlayerMovement playerMov;

	public float baseMoveSpeed;
	public float baseGroundAccel;

	public float baseAirAccel;
	public float baseJumpHeight;
	public float baseExtraJumpHeight;

	PlayerIndex player1 = PlayerIndex.One;

		// Use this for initialization
	void Start () 
	{
		transform.position = spawnpoint.position;

		score = GameObject.FindObjectOfType<Score>();
		playerMov = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

	}


	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag ("Kill") && !dead)
		{
			StartCoroutine (Dead());
			score.score -= deathScorePen;
			dead = true;
		}
	}



	IEnumerator Dead()
	{
		playerMov.canMove = false;
		GamePad.SetVibration(player1, 0.2f, 0.4f);
		yield return new WaitForSeconds (0.1f);
		GamePad.SetVibration(player1, 0.0f, 0.0f);
		renderer.enabled = false;
		yield return new WaitForSeconds (spawnTimer);
		transform.position = spawnpoint.position;
		renderer.enabled = true;
		dead = false;
		playerMov.canMove = true;
	}

}
