using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateMenu_KH : MonoBehaviour {

	public GameObject pauseMenu;
	public MenuButton_KH menuScript;

	 void OnTriggerEnter (){
		if (menuScript._menuIsOn == true) {
			pauseMenu.SetActive (false);
			menuScript._menuIsOn = false;
		}
	}
}
