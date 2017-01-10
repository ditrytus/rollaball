using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public Button startButton;

	public GameObject hudPanel;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		startButton.onClick.AddListener(StartGame);
	}
	
	void StartGame() {
		Time.timeScale = 1;
		gameObject.SetActive(false);
		hudPanel.SetActive(true);
	}

}
