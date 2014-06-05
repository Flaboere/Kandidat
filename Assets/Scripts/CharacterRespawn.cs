using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterRespawn : MonoBehaviour 
{
	public Transform spawnpoint;
	public float spawnTimer;

	public string loadLevel;

	public int deathScorePen = 1;
	public Score score;
	public CameraMove camMove;

	public bool dead = false;

	public bool canRespawn = false;

	public float camDeadDistance = 15;

	public PlayerMovement playerMov;
	public CharacterMotor motor;

	public float baseMoveSpeed;
	public float baseGroundAccel;

	public float baseAirAccel;
	public float baseJumpHeight;
	public float baseExtraJumpHeight;

	public SkinnedMeshRenderer childMesh;

	PlayerIndex player1 = PlayerIndex.One;

		// Use this for initialization
	void Start () 
	{
		transform.position = spawnpoint.position;
		childMesh = GetComponentInChildren<SkinnedMeshRenderer> ();
		score = GameObject.FindObjectOfType<Score>();
		playerMov = GetComponent<PlayerMovement> ();
		motor = GetComponent<CharacterMotor> ();
		motor.enabled = true;
		camMove = GameObject.FindObjectOfType<CameraMove> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		PlayerIndex controllerNumber = PlayerIndex.One;
		GamePadState state = GamePad.GetState(player1);

		if (dead && canRespawn)
		{
			if (Input.GetButton("X"))
			{
				Application.LoadLevel (loadLevel);
			}
		}
		
		if (transform.position.x < (Camera.main.transform.position.x - camDeadDistance))
		{
			StartCoroutine (Dead());
		}
		if (score.penalty <= 0)
		{
			StartCoroutine (Dead());
		}
	}


	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.CompareTag ("Kill") && !dead)
		{
			StartCoroutine (Dead());
			StartCoroutine (VibrateDead());
		}
	}

	void OnTriggerEnter (Collider hit)
	{
		if (hit.collider.CompareTag ("Kill") & !dead)
		{
			StartCoroutine (VibrateDead());
			StartCoroutine (Dead());
		}
	}

	// Genstarter scenen når man "dør"
	IEnumerator Dead()
	{
		playerMov.canMove = false;
		motor.enabled = false;
		dead = true;
		childMesh.renderer.enabled = false;
		yield return new WaitForSeconds (spawnTimer);
		canRespawn = true;
//		Application.LoadLevel ("Run_01");
	}

	// Respawner spilleren ved start hvis man dør, men resætter ikke banen
//	IEnumerator Dead()
//	{
//		score.score -= deathScorePen;
//		dead = true;
//		playerMov.canMove = false;
//		childMesh.renderer.enabled = false;
//		yield return new WaitForSeconds (spawnTimer);
//		transform.position = spawnpoint.position;
//		childMesh.renderer.enabled = true;
//		dead = false;
//		playerMov.canMove = true;
//	}

	IEnumerator VibrateDead()
	{
		GamePad.SetVibration(player1, 0.2f, 0.4f);
		yield return new WaitForSeconds (0.6f);
		GamePad.SetVibration(player1, 0.0f, 0.0f);
	}

}
