using UnityEngine;

// Continuous movement
public class Move4 : MonoBehaviour 
{
	public Vector3 speed = Vector3.zero; // m/s
	public Vector3 acceleration = Vector3.zero; // (m/s)/s = m/s^2

	void Update()
	{
		// Change speed according to current acceleration. 
		speed = speed + acceleration * Time.deltaTime;
		
		// Change position according to the current speed.
		transform.position = transform.position + speed * Time.deltaTime;
	}
}

// Exercises:
// - What happens when you use this component in a running scene and change "speed"
//   from the editor.
// - What happens when you use this component in a running scene and change "acceleration"
//   from the editor.
// - Add if-statements so that speed can be changed using the arrow keys on the keyboard 
//   (use what you did in Input1).
// - Add if-statements so that acceleration can be changed using the arrow keys on the 
//   keyboard (use what you did in Input1).
