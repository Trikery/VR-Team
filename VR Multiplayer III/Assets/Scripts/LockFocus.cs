using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockFocus : MonoBehaviour {

    public static Action<Controller> setFocus;

    private Controller _controller;

    LineRenderer line;
    RaycastHit rayHit;

    public bool canRaycast;
    public float rayTimeLimit = 2f;
    public Vector3 rayOrig;

    public void Start()
    {
        setFocus += SetFocusHandler;
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
    }

    private void SetFocusHandler(Controller _script)
    {
        _controller = _script;
        StartCoroutine(RaycastWeapon());
        StartCoroutine(RaycastCounter());
    }

    IEnumerator RaycastWeapon()
    {
        //Ray ray = new Ray(transform.position, transform.up * -1);
        rayOrig = transform.position;
        if (Physics.Raycast(rayOrig, transform.forward, out rayHit))
        {
            line.enabled = true;
            line.SetPosition(0, rayOrig);
            line.SetPosition(1, rayHit.point);
            _controller.focus = rayHit.collider.GetComponent<Transform>();

            if(rayHit.collider.GetComponent<Rigidbody>())
            {
                _controller.focus = rayHit.collider.GetComponent<Transform>();
            }
        }
        yield return new WaitForFixedUpdate();
        if (canRaycast)
            StartCoroutine(RaycastWeapon());
    }

    IEnumerator RaycastCounter()
    {
        yield return new WaitForSeconds(rayTimeLimit);
        canRaycast = false;
        line.enabled = false;

    }
}
