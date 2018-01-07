using UnityEngine;
using System.Collections;

public class playerMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.HasKey("screenLimits")) {
			var Temp = (float) PlayerPrefs.GetFloat ("screenLimits");
			transform.position = new Vector3(-Temp,transform.position.y,transform.position.z);

		}
		
	}
}
