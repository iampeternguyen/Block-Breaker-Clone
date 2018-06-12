using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	private const int WIDTH_IN_BLOCKS = 16;
	private const float MAX_POSITION = 7.5f;
	private Ball ball;
	private bool autoPlay = false;
	public float autoPlaySpeed = 1f;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();

	}

	// Update is called once per frame
	void Update () {

		if (GameplayManager.instance.StateIsActive ()) {
			if (!autoPlay) {
				MoveWithMouse ();
			} else {
				AutoPlay ();
			}
		}

	}

	private void AutoPlay () {
		Vector3 temp = this.transform.position;
		if (ball) {
			temp.x = Mathf.Clamp (ball.transform.position.x, -MAX_POSITION, MAX_POSITION);
		}
		transform.position = temp;
		Time.timeScale = autoPlaySpeed;
	}

	private void MoveWithMouse () {
		float mousePosInBlocks = (Input.mousePosition.x / Screen.width) * WIDTH_IN_BLOCKS - WIDTH_IN_BLOCKS / 2;

		Vector3 temp = transform.position;
		mousePosInBlocks = Mathf.Clamp (mousePosInBlocks, -MAX_POSITION, MAX_POSITION);
		temp.x = mousePosInBlocks;
		transform.position = temp;
	}
}