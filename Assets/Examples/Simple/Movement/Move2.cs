using UnityEngine;

// Using a variable of type Vector3.
public class Move2 : MonoBehaviour 
{
	public Vector3 startPosition;
	
	void Start () 
	{
		// Places the game object at the position in variable startPosition.
		transform.position = startPosition;
	}
}

// Exercises:
// - Change the script so places the game object in position (0,10,10).