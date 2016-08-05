using UnityEngine;
using System.Collections;

public class ThridCharacterCamera : MonoBehaviour 
{
	public Transform target;
	public float distance = 5f;
	public float height = 5f;
	public float smooth = 5f;
	private float yVelocity = 0.0f;

	void Update () 
	{
		float yAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);
		Vector3 position = target.position;
		position += Quaternion.Euler (0, yAngle, 0) * new Vector3 (0, height, -distance);
		transform.position = position;
		transform.LookAt (target);
	}
}
