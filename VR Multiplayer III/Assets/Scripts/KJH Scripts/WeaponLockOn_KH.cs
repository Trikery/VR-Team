using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponLockOn_KH : MonoBehaviour {

	public static Action <ControllerKH> setFocus;

	LineRenderer line;
	RaycastHit rayHit;

	public bool canRaycast;
	public float rayTimeLimit = 2f;

	public void Start(){
		setFocus += SetFocusHandler;
		line = gameObject.AddComponent<LineRenderer> ();
		line.enabled = false;
	}

	private void SetFocusHandler(ControllerKH _controller){
		StartCoroutine (RaycastWeapon ());
		StartCoroutine (RaycastCounter ());
	}

	IEnumerator RaycastWeapon(){
		Ray ray = new Ray(transform.position, transform.up * -1);
		if (Physics.Raycast (ray, out rayHit)) {
			line.SetPosition (0, transform.position);
			line.SetPosition (1, rayHit.point);
			line.enabled = true;
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
