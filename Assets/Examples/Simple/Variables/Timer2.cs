using UnityEngine;

// A simple cyclic timer.
public class Timer2 : MonoBehaviour 
{
	public float timer;
	public float maxTimer = 5f;
	
	void Start () 
	{
		// Set timer to zero when the game starts.
		timer = 0f;
	}
	
	void Update () 
	{
		// Add time since last Update (Time.deltaTime) to timer variable.
		timer = timer + Time.deltaTime;
		
		// Check if timer value is larger than maxTimer.
		if(timer > maxTimer)
		{
			// Reset timer to zero.
			timer = 0f;
			
			// Write a message to console when timer is reset.
			Debug.Log("Timer was reset.");
		}
	}
}

// Exercises:
// - What happens when you change maxTimer in the Unity editor?
// - Change the script so runs from maxTimer and down to zero (and the resets to maxTimer).