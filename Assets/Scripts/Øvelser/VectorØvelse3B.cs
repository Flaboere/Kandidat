using UnityEngine;
using System.Collections;

public class VectorØvelse3B : MonoBehaviour 
{
	public float size;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		// 2d Kasse
		Debug.DrawLine(new Vector3(0,0,0)* size, new Vector3(0,2,0)* size, Color.red);
		Debug.DrawLine(new Vector3(0,2,0)* size, new Vector3(2,2,0)* size, Color.red);
		Debug.DrawLine(new Vector3(2,2,0)* size, new Vector3(2,0,0)* size, Color.red);
		Debug.DrawLine(new Vector3(2,0,0)* size, new Vector3(0,0,0)* size, Color.red);
//		// 3d kasse, "baggrund"
//		Debug.DrawLine(new Vector3(1,1,2), new Vector3(1,3,2), Color.red);
//		Debug.DrawLine(new Vector3(1,3,2), new Vector3(3,3,2), Color.red);
//		Debug.DrawLine(new Vector3(3,3,2), new Vector3(3,1,2), Color.red);
//		Debug.DrawLine(new Vector3(3,1,2), new Vector3(1,1,2), Color.red);
//		// 3d kasse, "forbindelse"
//		Debug.DrawLine(new Vector3(2,0,0), new Vector3(3,1,2), Color.red);
//		Debug.DrawLine(new Vector3(3,3,2), new Vector3(2,2,0), Color.red);
//		Debug.DrawLine(new Vector3(1,3,2), new Vector3(0,2,0), Color.red);
//		Debug.DrawLine(new Vector3(1,1,2), new Vector3(0,0,0), Color.red);
	}
}
