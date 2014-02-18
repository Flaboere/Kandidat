using UnityEngine;

// Step by step movement
public class Move3 : MonoBehaviour 
{
	private float timer;
	void Start() 
	{
		// Places the game object at (0,0,0).
		transform.position = new Vector3(0f, 0f, 0f);
		
		// Resets timer to 1f.
		timer = 1f;
	}
	
	void Update()
	{
		// Subtract time since last Update so timer keeps getting smaller and smaller.
		timer = timer - Time.deltaTime;
		
		// If timer is less than zero: do something.
		if(timer < 0)
		{
			// Change the game objects position to the same as before but add "right vector".
			transform.position = transform.position + new Vector3(1f, 0f, 0f);
			
			timer = 1f;
		}
	}
}

// Exercises:
// - Change the script so moves the game object in the opposite direction.
// - Change the script so moves the game object upward.
// - Add a public variable called "step" of type Vector3 and use it in line 24.
//   See what happens if you change it in the Unity editor.
// - Make it random by using Random.onUnitSphere
// - Right now it moves every 1 second. What would you change to make it take faster steps?