using UnityEngine;
using System.Collections;

public class FlipTextureOnDirection : MonoBehaviour 
{

	float scale;

	public bool turningRight = true;
	// Use this for initialization
	void Start () 
	{
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("Horizontal") > 0.1f)
		{
			transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
		}
		if (Input.GetAxis ("Horizontal") < -0.1f)
		{
			transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
		}
	}
}
