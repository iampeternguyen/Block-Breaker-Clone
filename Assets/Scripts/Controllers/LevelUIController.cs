using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour {

	public static LevelUIController instance;

	private Button restartGameButton, instructionsButton;

	private Text pausePanelText, resumeGameText;

	private GameObject pausePanel;

	void Awake () {
		makeInstance ();
		GetGameObjectReferences ();
		GameReady ();

		PlayStateChanged ();
		GameplayManager.playStateChanged += this.PlayStateChanged;

		if (SceneFader.instance == null) {
			Instantiate (Resources.Load ("Scene Fader"));
		}
	}

	private void GetGameObjectReferences () {
		restartGameButton = GameObject.Find ("Resume Game Button").GetComponent<Button> ();
		instructionsButton = GameObject.Find ("Instructions Button").GetComponent<Button> ();
		pausePanelText = GameObject.Find ("Paused Text").GetComponent<Text> ();
		resumeGameText = GameObject.Find ("Resume Game Button Text").GetComponent<Text> ();
		pausePanel = GameObject.Find ("Pause Panel");

	}

	void OnDestroy () {
		GameplayManager.playStateChanged -= this.PlayStateChanged;
	}

	private void PlayStateChanged () {
		switch (GameplayManager.instance.PlayState) {
			case GameplayManager.PlayStates.Ready:
				GameReady ();
				break;
			case GameplayManager.PlayStates.Active:
				ResumeGame ();
				break;
			case GameplayManager.PlayStates.Paused:
				PauseGame ();
				break;
			case GameplayManager.PlayStates.GameOver:
				GameOver ();
				break;
			case GameplayManager.PlayStates.Win:
				WinGame ();
				break;
		}

	}

	public void ChangePlayStateToPaused () {
		if (GameplayManager.instance.PlayState == GameplayManager.PlayStates.Active) {
			GameplayManager.instance.ChangePlayState (GameplayManager.PlayStates.Paused);

		}
	}

	public void ChangePlayStateToActive () {
		GameplayManager.instance.ChangePlayState (GameplayManager.PlayStates.Active);
	}

	private void makeInstance () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	private void GameReady () {
		pausePanel.SetActive (false);
		FreezeGame ();
		instructionsButton.gameObject.SetActive (true);

	}

	private void PauseGame () {

		pausePanelText.text = "Game Paused";
		resumeGameText.text = "Resume";

		pausePanel.SetActive (true);

		FreezeGame ();
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => ResumeGame ());

	}

	private void GameOver () {

		pausePanelText.text = "Game Over";
		resumeGameText.text = "Restart";

		pausePanel.SetActive (true);

		FreezeGame ();
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => RestartLevel ());

	}

	private void WinGame () {
		FreezeGame ();
		pausePanel.SetActive (true);
		restartGameButton.onClick.RemoveAllListeners ();

		bool isNextLevel = SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene ().buildIndex + 1;
		if (isNextLevel) {
			pausePanelText.text = "You beat the level!";
			resumeGameText.text = "Continue?";
			restartGameButton.onClick.AddListener (() => SceneFader.instance.FadeOutToNextLevel ());
		} else {
			pausePanelText.text = "You beat the game!";
			restartGameButton.gameObject.SetActive (false);
		}

	}

	public void GoToMenu () {
		UnFreezeGame ();
		SceneFader.instance.FadeOutToScene ("MainMenu");
	}

	private void ResumeGame () {
		pausePanel.SetActive (false);
		instructionsButton.gameObject.SetActive (false);
		UnFreezeGame ();
	}

	public void RestartLevel () {
		Brick.breakableBrickCount = 0;
		SceneFader.instance.FadeOutToScene (SceneManager.GetActiveScene ().name);
	}

	private void FreezeGame () {
		Time.timeScale = 0f;
	}
	private void UnFreezeGame () {
		Time.timeScale = 1f;
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}