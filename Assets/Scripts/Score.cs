using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
	// Score variabler
	public int score;
	public float finalScoreOffset;

	public int penalty = 3;

	public float deathWait;
	public float deathWait2;

//	public Font font;

//	public GUIStyle color;
	public UILabel finalscore;
	public UILabel scoreLabel;
	public UILabel penaltyLabel;
	public UILabel insult;

	// Points score variabler
	public float waitScore;

	private int originScoreSize;
	public int newScoreSize;
	private int newScoreSizeTemp;

	private int originPenaltySize;
	public int newPenaltySize;
	private int newPenaltySizeTemp;
//	private int thisFontSize;
//	public float sizeSmooth;

//	private float curVel1;
//	private float curVel2;




//	GUIText highScore;

	// Sprintbar variabler
	public float fullHeight = 128f;

	public PlayerMovement playerMovement;
	public CharacterRespawn respawn;

	public UISprite sprintBar;

	void Start ()
	{
		playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
		respawn = GameObject.FindObjectOfType<CharacterRespawn>();
		finalscore = GameObject.Find ("Finalscore").GetComponent<UILabel> ();
		scoreLabel = GameObject.Find ("ScoreLabel").GetComponent<UILabel> ();
		penaltyLabel = GameObject.Find ("PenaltyLabel").GetComponent<UILabel> ();
		insult = GameObject.Find ("FinalscoreInsult").GetComponent<UILabel> ();

		finalscore.text = "";
		insult.text = "";
		
		originScoreSize = scoreLabel.fontSize;
		newScoreSizeTemp = originScoreSize + newScoreSize;

		originPenaltySize = penaltyLabel.fontSize;
		newPenaltySizeTemp = originPenaltySize + newPenaltySize;
	}

	void Update () 
	{
		sprintBar.fillAmount = playerMovement.sprintAmount/10;
		if (!respawn.dead)
		{
			scoreLabel.text = "Score is = " + score;
			penaltyLabel.text = "Penalty left = " + penalty;

		}

		if (respawn.dead)
		{
			StartCoroutine(Dead ());
		}

	}

	IEnumerator Dead()
	{
		yield return new WaitForSeconds (deathWait);
		finalscore.text = "Your final score is " + score;
		yield return new WaitForSeconds (deathWait2);
		insult.text = "Run faster, fat man";
//		yield return new WaitForSeconds (deathWait2);
//		insult.text = "Run faster, fat man";
	}

	// Ændrer font når der scores points
	IEnumerator Scorepoints()
	{
//		scoreLabel.fontSize = Mathf.SmoothDamp (scoreLabel.fontSize, newSize, ref curVel1, sizeSmooth * Time.deltaTime);
		scoreLabel.fontSize = newScoreSizeTemp;
		yield return new WaitForSeconds(waitScore);
		scoreLabel.fontSize = originScoreSize;
//		scoreLabel.fontSize = Mathf.SmoothDamp (scoreLabel.fontSize, originSize, ref curVel1, sizeSmooth * Time.deltaTime);
	}

	// Ændrer font når der væltes hæk
	IEnumerator Penaltypoints()
	{
		penaltyLabel.fontSize = newPenaltySizeTemp;
		yield return new WaitForSeconds(waitScore);
		penaltyLabel.fontSize = originPenaltySize;
	}
}
