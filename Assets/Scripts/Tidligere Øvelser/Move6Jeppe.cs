using UnityEngine;
using System.Collections;

public class Move6Jeppe : MonoBehaviour {

	public float timer;
	public Vector3 step;
	
	public Jeppe target;
	
	void Start () 
	{
	
		transform.position = new Vector3 (0f,0f,0f);
		timer = 1f;
		
		Beer carlsberg = new Beer();
		carlsberg.name = "Carlsberg";
		carlsberg.alcohol = 7.4f;
		
		
	}
	
	void Update () 
	{
		
		
		timer = timer - Time.deltaTime;
		if (timer < 0)
		{
			transform.position = transform.position + Random.onUnitSphere;
			timer = 1f;
		}
		Debug.Log (Time.deltaTime);
		
	}
}
// For at få den til at gå med kortere intervaller, ændre timer = 1f to timer = 0.5f (både i update og start, men update vigtigst. Så tæller den ned fra et mindre tal, og det går hurtigere at den når 0!



class Beer 
{
	public string name;
	public float alcohol;
	
	
}