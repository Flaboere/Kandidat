using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
	public float speed = 45f;
	public Vector3 direction;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (direction, speed * Time.deltaTime);
	}
}
