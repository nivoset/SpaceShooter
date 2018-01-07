using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour 
{
	public GUIText DisplayText;

	void Start()
	{
		DisplayText.text = "test";
	}
	void Update () 
	{
		//Touch myTouch = Input.GetTouch(0);
		//Touch[] myTouches = Input.touches;
		/*
		string display = "Test:\n";

		for(int i = 0; i < Input.touchCount; i++)
		{
			//Do something with the touches
			display += "x: " + Input.GetTouch(i).position.x + "\ny: " + Input.GetTouch(i).position.y + "\n";

		}

		if (DisplayText != null)
			DisplayText.text = display; //*/
	} 
}