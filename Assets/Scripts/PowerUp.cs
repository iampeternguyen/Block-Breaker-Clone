using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public Ball ball;
	public Bullet bullet;

	private Paddle paddle;

	private bool isActivated = false;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.color = Color.red;
	}

	// Update is called once per frame
	void Update () {
		ChangeColor ();

	}

	private void ChangeColor () {
		spriteRenderer.color = spriteRenderer.color == Color.red ? Color.white : Color.red;
	}

	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "Paddle" && !isActivated) {
			isActivated = true;
			AssignNewPower ();
			gameObject.transform.localScale = Vector3.zero;
		}
	}

	private void AssignNewPower () {
		int powerUp = UnityEngine.Random.Range (0, 3);
		switch (powerUp) {
			case 0:
				StartCoroutine (BallMultiplier ());
				break;
			case 1:
				StartCoroutine (BiggerPaddle ());
				break;
			case 2:
				StartCoroutine (ShootBullets ());
				break;
		}

	}

	IEnumerator BiggerPaddle () {
		Vector3 temp = paddle.transform.localScale;
		paddle.transform.localScale = new Vector3 (temp.x * 2, 1f, 1f);
		yield return new WaitForSeconds (10f);
		paddle.transform.localScale = new Vector3 (paddle.transform.localScale.x / 2, 1f, 1f);
		Destroy (gameObject);
	}

	IEnumerator BallMultiplier () {
		for (int i = 0; i < 5; i++) {
			Ball currentBall = Instantiate (ball);
			Vector2 tempLocation = paddle.transform.position;
			currentBall.transform.position = tempLocation;
			currentBall.GetComponent<Rigidbody2D> ().velocity = new Vector3 (2f, 10f, 0f);
		}
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

	IEnumerator ShootBullets () {
		int bullets = 10;

		for (int i = 0; i < bullets; i++) {
			Vector2 tempLocation = paddle.transform.position;
			tempLocation.y += 2f;

			Bullet currentBullet = Instantiate (bullet);
			currentBullet.transform.position = tempLocation;
			currentBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 25f);
			yield return new WaitForSeconds (1f);

		}
		Destroy (gameObject);
	}

	IEnumerator FastBall () {

		Ball[] balls = GameObject.FindObjectsOfType<Ball> ();

		foreach (Ball selectBall in balls) {
			selectBall.SetSpeedUp (true);
		}

		yield return new WaitForSeconds (10f);
		foreach (Ball selectBall in balls) {
			selectBall.SetSpeedUp (false);
			selectBall.SetSpeedNormal ();
		}

		Destroy (gameObject);
	}
}