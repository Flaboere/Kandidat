using UnityEngine;

// Continuous rotation
public class Rotate1 : MonoBehaviour 
{
	public float speed = 45f;

	void Update()
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}

// Exercises:
// - Try changing speed while running the script.
// - Change the script to rotate around another axis - look it up in the Unity documentation.
// - Add script to a hierachy of game objects (use cubes) and change . Can you explain 
//   the behaviour when you run it?