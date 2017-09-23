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
    public Vector3 rayOrig;

	public void Start(){
		setFocus += SetFocusHandler;
		line = gameObject.GetComponent<LineRenderer> ();
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

        rayOrig = transform.position;

		if (Physics.Raycast (transform.position, transform.up * -1, out rayHit)) {
            line.enabled = true;
            line.SetPosition (0, rayOrig);
			line.SetPosition (1, rayHit.point);
			

			if (rayHit.collider.GetComponent<Renderer>().material.shader == _controller.Outlineable) {// || rayHit.rigidbody != null) {
				if (_controller.focus != rayHit.collider.GetComponent<Transform> ()) {
                    if (_controller.focus != null)
					    _controller.focus.GetComponent<Renderer>().material.SetFloat ("_OutlineWidth", 1);
					_controller.focus = rayHit.collider.GetComponent<Transform> ();
					_controller.focus.GetComponent<Renderer>().material.SetFloat ("_OutlineWidth", _controller.outlineSize);
                    if(_controller.allFocus.Count != 0)
                    {
                        for (int i = 0; i  <= _controller.allFocus.Count -1; i++)
                        {
                            if (_controller.focus != _controller.allFocus[i])
                                _controller.allFocus.Add(_controller.focus);
                        }
                    }
                    if(_controller.allFocus.Count == 0)
                        _controller.allFocus.Add(_controller.focus);
                }
			}
		}
        
		yield return new WaitForFixedUpdate ();
		if (canRaycast && _controller.gripped)
			StartCoroutine(RaycastWeapon());

        if (!_controller.gripped)
        {
            _controller.focus = null;
            line.enabled = false;
        }
    }

	IEnumerator RaycastCounter (){
		yield return new WaitForSeconds (rayTimeLimit);
		canRaycast = false;
		line.enabled = false;

	}
}
