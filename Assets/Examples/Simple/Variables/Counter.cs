using UnityEngine;

public class Counter : MonoBehaviour 
{
	// Variable used to store a counter (integer). 	
	public int counter;
	
	void Start () 
	{
		// Set counter to zero when the game starts.
		counter = 0;
	}
	
	void Update () 
	{
		// Adds one to the counter in each update.
		counter = counter + 1;
	}
}

// Exercises:
// - Change the script so it adds 10 (instead of 1) in each update.
// - Change the script so it doubles (multiply by 2) the counter in each update.