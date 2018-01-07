using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {
	

	public Transform destroyEffect;
	public Transform playerDestroyEffect;
	public int PointsToAdd = 10;
	private GameController gameController;



	//public Texture startButton;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();

		if (gameController == null)
			Debug.Log ("Could not find a 'gameController' script");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary") 
			return;
		if (other.tag == transform.tag)
			return;

		if (destroyEffect != null)
			Instantiate (destroyEffect, transform.position, transform.rotation);

		if (other.tag == "Player" || transform.tag == "Player") {
			Debug.Log ("uhh Ohh!" + this.tag + " : " + transform.position.z);
			gameController.die ();
		}

		Destroy (other.gameObject);
		Destroy (gameObject);

		//If the other tag is Player Generated, then grant points.
		if (other.tag == "Player Shot") {				
			if (PointsToAdd > 0) {
				gameController.AddScore (PointsToAdd);
			} else {
				gameController.SubtractScore (PointsToAdd);
			}
		}
	}

}
