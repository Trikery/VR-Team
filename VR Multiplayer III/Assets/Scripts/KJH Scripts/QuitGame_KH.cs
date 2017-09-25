using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame_KH : MonoBehaviour {

	public GameObject pauseMenu;
	public MenuButton_KH menuScript;

	public Scene menuScene;

	void OnTriggerEnter(){
		pauseMenu.SetActive (false);
		menuScript._menuIsOn = false;
		SceneManager.LoadScene (menuScene.name, LoadSceneMode.Single);
	}
}
