using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "Ball") {
			Destroy (target);
		}

		if (target.tag == "PowerUp") {
			Destroy (target);
		}
	}

	public static void CheckGameOverState () {
		Ball[] balls = GameObject.FindObjectsOfType<Ball> ();
		if (balls.Length <= 0) {
			GameplayManager.instance.ChangePlayState (GameplayManager.PlayStates.GameOver);
		}
	}
}