using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
	// Score variabler
	public int score;

	public Font font;

	GUIText highScore;

	// Sprintbar variabler
	public float fullHeight = 128f;

	public PlayerMovement playerMovement;

	public UISprite sprintBar;

	void Start ()
	{

	}

	void Update () 
	{
		sprintBar.fillAmount = playerMovement.sprintAmount/10;
	}

	void OnGUI()
	{
		GUI.skin.font = font;
		GUI.Label(new Rect(10,10,100,20),("Score is = " + score));
	}   
}
