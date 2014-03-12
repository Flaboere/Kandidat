using UnityEngine;
using System.Collections;

public class VertexColor : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Color[] colors = new Color[vertices.Length];
		int i = 0;
		while (i < vertices.Length) {
			colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
			i++;
		}
		mesh.colors = colors;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
