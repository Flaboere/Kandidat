using UnityEngine;

// Keyboard input.
public class Input1 : MonoBehaviour 
{
	void Update()
	{
		if(Input.GetKey(KeyCode.X))
		{
			Debug.Log("X key is down.");
		}
		
		if(Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("X key was just pressed.");
		}
	}
}

// Exercises:
// - Change script to detect when left, right, up or down arrow keys are pressed down. Print
//   using Debug.Log.
// - Add a counter variable (int) to the script. When the user presses arrow key up 1 should 
//   be added to the counter and on arrow key down 1 shoud be subtracted from the counter.