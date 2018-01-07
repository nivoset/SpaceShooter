using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;

	private int fire;

	void Start() {
		
		if (PlayerPrefs.HasKey("screenLimits")) {
			var Temp = (float) PlayerPrefs.GetFloat ("screenLimits");
			boundary.xMin = -(float)Temp;
			boundary.xMax = (float)Temp;
			
		}

	}


	void Update ()
	{
#if UNITY_ANDROID 
		if (PlayerPrefs.HasKey("controls") && PlayerPrefs.GetInt ("controls") == 1) {
			fire = Input.touchCount;
		} else {
			fire = Input.GetButton("Fire1")?2:0;
		}
#else
		fire = Input.GetButton("Fire1")?2:0;
#endif

		if ((nextFire -= Time.deltaTime) <= 0 && fire >= 2 )
		{
			nextFire = fireRate;
			if (shot != null) {
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				if (PlayerPrefs.HasKey ("sfx")) {
					if (PlayerPrefs.GetInt ("sfx") == 1)
						audio.Play ();
				}
			}
		}
	}

	void FixedUpdate ()
	{

#if UNITY_ANDROID 

		fire = Input.touchCount;
		float moveHorizontal = Input.acceleration.x * speed*5;
		float moveVertical = Input.acceleration.y * speed*5;
		
		if (PlayerPrefs.HasKey("controls") && PlayerPrefs.GetInt ("controls") == 1) {
			
			float leftToRightPlayerProgression = (rigidbody.position.x - boundary.xMin) / (boundary.xMax - boundary.xMin); //Percentage from left to right
			float leftToRightFingerProgression = 0;

			float upToDownPlayerProgression = (rigidbody.position.y - boundary.zMin) / (boundary.zMax - boundary.zMin); //Percentage from left to right
			float upToDownFingerProgression = 0;
			Vector2 finger;

			if (fire > 0){
				finger = Input.GetTouch(0).position;
				leftToRightFingerProgression = finger.x/Screen.width;
				upToDownFingerProgression = finger.y/Screen.height;
				moveHorizontal = ((leftToRightFingerProgression - leftToRightPlayerProgression)*2);
				moveHorizontal = moveHorizontal * speed * 5;

				moveVertical = ((upToDownFingerProgression - upToDownPlayerProgression)*2);
				moveVertical = moveVertical * speed * 5;
			} else {
				moveHorizontal = 0;
				moveVertical = 0;
			}
			
		}


#else
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed;
		float moveVertical = Input.GetAxis ("Vertical") * speed; //*/
#endif

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement;



		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * - tilt);
	}
}