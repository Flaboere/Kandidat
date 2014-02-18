using UnityEngine;

// A simple timer.
public class Timer1 : MonoBehaviour 
{
	// Variable used to store the timer value (decimal number). 	
	public float timer;
	
	void Start () 
	{
		// Set timer to zero when the game starts.
		timer = 10f;
	}
	
	void Update () 
	{
		// Add time since last Update (Time.deltaTime) to timer variable.
		timer = timer + Time.deltaTime;
	}
}

// Exercises:
// - Change the script so timer starts at 1000f.