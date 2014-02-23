using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
	private Vector2 originPosition2;
	private Vector3 originPosition;
	private Vector2 randomCircle;
	private Quaternion originRotation;
	private float shake_decay;
	private float shake_intensity;

	public float shakeDecay = 0.2f;
	public float shakeIntensity = 0.01f;
	private Quaternion cameraRotation;

	void Start () 
	{
//		cameraRotation = camera.transform.rotation;
	}
	
	void Update ()
	{
		if (shake_intensity > 0)
		{
			randomCircle = originPosition2 + Random.insideUnitCircle * shake_intensity;
			transform.position = new Vector3 (randomCircle.x, randomCircle.y, originPosition.z);
//			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
//			transform.rotation = new Quaternion(
//				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
//				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
//				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
//				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		}
//			camera.transform.rotation = cameraRotation;

	}
	
	public void Shake(){
		originPosition = transform.position;
		originPosition2 = new Vector2 (transform.position.x, transform.position.y);
		originRotation = transform.rotation;
		shake_intensity = shakeIntensity;
		shake_decay = shakeDecay;
	}
}