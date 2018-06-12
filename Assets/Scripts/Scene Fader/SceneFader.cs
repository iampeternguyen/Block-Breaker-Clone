using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField] private GameObject fadeCanvas;
	[SerializeField] private Animator fadeAnim;

	void Awake () {

		makeSingleton ();

	}
	public void FadeOutToScene (string levelName) {
		StartCoroutine (FadeOutAnimation ());
		SceneManager.LoadScene (levelName);
		FadeIn ();
	}

	public void FadeOutToNextLevel () {
		StartCoroutine (FadeOutAnimation ());
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		FadeIn ();

	}

	private void FadeIn () {
		StartCoroutine (FadeInAnimation ());
	}

	IEnumerator FadeOutAnimation () {
		fadeCanvas.SetActive (true);
		fadeAnim.Play ("FadeOut");
		yield return new WaitForSecondsRealtime (.7f);

	}

	IEnumerator FadeInAnimation () {
		fadeAnim.Play ("FadeIn");
		yield return new WaitForSecondsRealtime (1f);
		fadeCanvas.SetActive (false);

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void makeSingleton () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
}