using UnityEngine;

// Scaling vectors.
public class Vectors4 : MonoBehaviour 
{
	void Start() 
	{
		// Define a vector v.
		Vector3 v = new Vector3(1, 2, 4);
		
		// Scale the vector with 5.
		v = v * 2f;
		
		// Print vector to console after scale.
		Debug.Log(v);
	}
}

// Exercises:
// - What happens to the length of v when it is scaled by 2? Investigate using v.magnitude 
//   and Debug.Log(...).
// - How would you scale an arbitrary vector to make it length 1f (normalization)? Unit-length vectors
//   are very important when working with directions.
