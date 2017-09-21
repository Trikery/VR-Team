using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponLockOn_KH : MonoBehaviour {

	public static Action <Controller> setFocus;

	private Controller _controller;

	LineRenderer line;
	RaycastHit rayHit;

	public bool canRaycast;
	public float rayTimeLimit = 2f;
	public float lineEndWidth = .5f;

	public void Start(){
		setFocus += SetFocusHandler;
		line = gameObject.AddComponent<LineRenderer> ();
		line.enabled = false;
		line.startWidth = 0;
		line.endWidth = lineEndWidth;
	}

	private void SetFocusHandler(Controller _getFocus){
		canRaycast = true;
		_controller = _getFocus;
		_controller.allFocus.Clear ();
		StartCoroutine (RaycastWeapon ());
		StartCoroutine (RaycastCounter ());
	}

	//
	IEnumerator RaycastWeapon(){

		if (Physics.Raycast (transform.position, transform.up * -1, out rayHit)) {
			line.SetPosition (0, transform.position);
			line.SetPosition (1, rayHit.point);
			line.enabled = true;

			if (rayHit.collider.GetComponent<Shader>() == _controller.Outlineable) {
				_controller.focus = rayHit.collider.GetComponent<Transform> ();
				_controller.allFocus.Add (_controller.focus);
			}
		}
		yield return new WaitForFixedUpdate ();
		if (canRaycast)
			StartCoroutine(RaycastWeapon());
	}

	IEnumerator RaycastCounter (){
		yield return new WaitForSeconds (rayTimeLimit);
		canRaycast = false;
		line.enabled = false;

	}
}
