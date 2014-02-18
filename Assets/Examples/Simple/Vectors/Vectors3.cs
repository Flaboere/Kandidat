using UnityEngine;

// Drawing vectors.
public class Vectors3 : MonoBehaviour 
{
	void Update() 
	{
		// Draws a line from position (0,0,0) to (2,2,2) in the Unity editor.
		Debug.DrawLine(new Vector3(0,0,0), new Vector3(2,2,2), Color.red);
	}
}

// Exercises:
// - Draw a 3D box using Debug.DrawLine(...)
// - Add a (float) variable "size" and use it in your drawing - so it
//   is possible to change the size of the box.
// - (extra) Add a roof to make it a house.
