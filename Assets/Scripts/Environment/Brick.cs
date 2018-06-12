using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public Sprite[] hitSprites;
	public PowerUp powerUp;
	public static int breakableBrickCount = 0;

	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;
	private bool isBreakable;
	private int timesHit;

	void Awake () {
		isBreakable = this.tag == "Breakable";
		timesHit = 0;
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		audioSource = this.GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		if (isBreakable) {
			breakableBrickCount++;
		}

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D () {

	}

	void OnCollisionExit2D (Collision2D target) {
		if (isBreakable) {
			AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
			HandleHits ();
		}
	}

	private void HandleHits () {
		int maxHit = hitSprites.Length + 1;
		timesHit++;

		if (timesHit >= maxHit) {
			int powerUpChance = UnityEngine.Random.Range (0, 11);
			powerUpChance *= (maxHit + 1) / 2;
			if (powerUpChance > 9) {
				PowerUp power = Instantiate (powerUp);
				power.transform.position = this.transform.position;

			}
			breakableBrickCount--;
			GameplayManager.instance.CheckWinState ();
			Destroy (gameObject);

		} else {
			LoadSprites ();
		}
	}

	private void LoadSprites () {
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			spriteRenderer.sprite = hitSprites[spriteIndex];
		}

	}

}