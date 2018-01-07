using UnityEngine;
using System.Collections;


public class EnemyStrafe : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public float RateOfChange = 0.1f;
	public float leftMax = -2f;
	public float rightMax = 2f;

	private float horizontalSpeed = 3f;
	
	void Start() {
		
		if (PlayerPrefs.HasKey("screenLimits")) {
			var Temp =  PlayerPrefs.GetFloat ("screenLimits");
			boundary.xMin = -Temp;
			boundary.xMax = Temp;
			
		}
		
	}

	void FixedUpdate ()
	{
		
		if (rigidbody.position.x > rightMax / 2) {
			horizontalSpeed -= RateOfChange;
		} else if (rigidbody.position.x < leftMax / 2) {
			horizontalSpeed += RateOfChange;
		} else if (horizontalSpeed == 0) {
			horizontalSpeed = 2;
		}
		//Debug.Log ("Horizontal Speed: " + horizontalSpeed);
		Vector3 movement = new Vector3 (horizontalSpeed * speed, 0.0f, rigidbody.velocity.z);
		rigidbody.velocity = movement;
		
		
		
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				rigidbody.position.z
				);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * - tilt);
	}
}