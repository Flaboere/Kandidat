using UnityEngine;

// Length of vectors.
public class Vectors2 : MonoBehaviour 
{
	void Start() 
	{
		// Define a vector "v" with value (1,1,0).
		Vector3 v = new Vector3(1f, 1f, 0f);
		
		// Calculate the length of v.
		float length = v.magnitude;
		
		// Print length to the editor console.
		Debug.Log(length);
	}
}

// Exercises:
// - Calculate the length of a vector (3,4,0).
// - Calculate the length of (10,10,10) + (-10,-10,-10).