using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Paddle paddle;

	private Vector3 ballToPaddleDistance;

	private Rigidbody2D rgbd;
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;

	private bool isSpedUp = false;

	void Awake () {
		rgbd = this.GetComponent<Rigidbody2D> ();
		audioSource = this.GetComponent<AudioSource> ();
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		paddle = GameObject.Find ("Paddle").GetComponent<Paddle> ();
	}

	// Use this for initialization
	void Start () {

		ballToPaddleDistance = this.transform.position - paddle.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (GameplayManager.instance.StateIsReady ()) {
			this.transform.position = paddle.transform.position + ballToPaddleDistance;
			if (Input.GetMouseButtonDown (0)) {
				GameplayManager.instance.ChangePlayState (GameplayManager.PlayStates.Active);
				rgbd.velocity = new Vector3 (rgbd.velocity.x, 10f, 0f);
			}
		}

		if (isSpedUp) {
			ChangeColor ();
		}
		CheckBallPosition ();

	}

	private void ChangeColor () {
		spriteRenderer.color = spriteRenderer.color == Color.green ? Color.white : Color.green;
	}

	internal void SetSpeedUp (bool spedUp) {
		isSpedUp = spedUp;
		rgbd.velocity = rgbd.velocity.y > 0 ? new Vector2 (rgbd.velocity.x, 20f) : new Vector2 (rgbd.velocity.x, -20f);

	}

	internal void SetSpeedNormal () {
		rgbd.velocity = new Vector2 (rgbd.velocity.x, 10f);
		spriteRenderer.color = Color.white;

	}

	private void CheckBallPosition () {
		// If for whatever reason the ball bounces out of the zone
		// delete ball. Useful in auto play mode. 
		Vector3 temp = transform.position;

		if (Mathf.Abs (temp.x) > 10 || Mathf.Abs (temp.y) > 8) {
			Destroy (gameObject);
		}
	}

	private void CheckBallVelocity () {

		if (rgbd.velocity.y < 4f) {
			rgbd.velocity = new Vector3 (rgbd.velocity.x, UnityEngine.Random.Range (5f, 10f), 0f);
		}
		if (Math.Abs (rgbd.velocity.x) < 2f) {
			if (rgbd.velocity.x >= 0) {
				rgbd.velocity = new Vector3 (UnityEngine.Random.Range (2f, 4f), rgbd.velocity.y, 0f);
			} else {
				rgbd.velocity = new Vector3 (UnityEngine.Random.Range (-2f, -4f), rgbd.velocity.y, 0f);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D target) {
		audioSource.PlayOneShot (audioSource.clip);
		Vector2 tweak = new Vector2 (UnityEngine.Random.Range (0f, 0.2f), UnityEngine.Random.Range (0f, 0.2f));
		rgbd.velocity += tweak;
		if (target.gameObject.tag == "Paddle") {
			CheckBallVelocity ();

		}

	}

	void OnDestroy () {
		LoseGame.CheckGameOverState ();
	}

}