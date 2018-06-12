using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

	public static GameplayManager instance;

	public enum PlayStates {
		Ready,
		Active,
		Paused,
		GameOver,
		Win
	}

	public PlayStates PlayState {
		get { return playState; }
	}

	private PlayStates playState;

	public delegate void PlayStateChangedHandeler ();
	public static event PlayStateChangedHandeler playStateChanged;

	void Awake () {

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		ChangePlayState (PlayStates.Ready);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ChangePlayState (PlayStates state) {
		playState = state;
		if (playStateChanged != null) {
			playStateChanged ();
		}
	}

	public bool StateIsActive () {
		return playState == PlayStates.Active;
	}

	public bool StateIsReady () {
		return playState == PlayStates.Ready;
	}

	public bool StateIsPaused () {
		return playState == PlayStates.Paused;
	}

	public bool StateIsGameOver () {
		return playState == PlayStates.GameOver;
	}

	public bool StateIsWin () {
		return playState == PlayStates.Win;
	}
	internal void CheckWinState () {
		if (Brick.breakableBrickCount <= 0) {
			GameplayManager.instance.ChangePlayState (GameplayManager.PlayStates.Win);
		}
	}
}