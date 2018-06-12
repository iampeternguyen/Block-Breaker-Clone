using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public static MainMenuController instance;

	void Awake () {
		makeInstance ();

	}

	private void makeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void PlayGame () {
		SceneFader.instance.FadeOutToScene ("Level_01");
	}

	public void QuitGame () {
		Application.Quit ();
	}
}