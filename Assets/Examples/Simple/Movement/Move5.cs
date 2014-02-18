using UnityEngine;

// Follow another game object (transform).
public class Move5 : MonoBehaviour 
{
	public Transform otherGameObject;
	public float speedFactor = 0.2f;
	public Vector3 speed = Vector3.zero; // m/s
	
	void Update()
	{
		// Find the directional vector from this object to the other
		Vector3 toOtherObject = otherGameObject.transform.position - transform.position;
		
		// Only move towards object if it is more than 0.1 away.
		if(toOtherObject.magnitude > 0.1f)
		{
			// Normalize vector to make it length 1.
			toOtherObject.Normalize();
			
			// Set the speed to the directional vector (multiplied by a speedFactor).
			speed = toOtherObject * speedFactor;
			
			// Change position according to the current speed.
			transform.position = transform.position + speed * Time.deltaTime;
		}
	}
}

// Exercises:
// - Instead of changing the speed to follow the other object, use acceleration (see Move4).
//   The result should be more bouncy.