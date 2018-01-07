using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float lifeTime = 2;
	void Start()
	{
		Destroy (gameObject, lifeTime);
	}
}


