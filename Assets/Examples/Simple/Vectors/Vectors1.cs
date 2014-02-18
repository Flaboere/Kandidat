using UnityEngine;

// Adding and subtracting vectors.
public class Vectors1 : MonoBehaviour 
{
	void Start() 
	{
		// Define a variable "a" and assign the value (0,5,0).
		Vector3 a = new Vector3(0f, 5f, 0f);
		
		// Define a variable "b" and assign the value (5,0,0).
		Vector3 b = new Vector3(5f, 0f, 0f);
		
		// Add a and b and put the result in a variable named "c".
		Vector3 c = a + b;
		
		// Print the result to the editor console.
		Debug.Log("c = " + c);
	}
}

// Exercises:
// - Change the script so it subtracts b from a. What is the result why?
// - Calculate the sum (1,0,0) + (0,2,0) + (0,0,3). 
// - Calculate the sum (1,0,0) + (1,2,0) and make a drawing that explains it.