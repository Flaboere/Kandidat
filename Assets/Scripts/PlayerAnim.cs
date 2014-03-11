using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour 
{

	Vector3 playerRotate;
	public float rotateAmount = 2;
	// Use this for initialization
	void Start () 
	{
	
	}
	void FixedUpdate () 
	{
		CharacterController controler = GetComponent<CharacterController> ();
		// Rotation af avatar ved hop
		playerRotate.z = controler.velocity.y;
		
		if (controler.velocity.x > 0)
		{
			this.transform.rotation = Quaternion.Euler (playerRotate * -rotateAmount);
		}
		
		if (controler.velocity.x < 0)
		{
			this.transform.rotation = Quaternion.Euler (playerRotate * rotateAmount);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
