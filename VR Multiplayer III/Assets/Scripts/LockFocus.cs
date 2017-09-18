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

    Material lineMat;

    public void Start()
    {
        setFocus += SetFocusHandler;
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
        line.startWidth = 0;
        line.endWidth = .5f;
        line.material = lineMat;
        line.startColor = Color.clear;
        line.endColor = Color.green;


    }

    private void SetFocusHandler(Controller _script)
    {
        canRaycast = true;
        _controller = _script;
        StartCoroutine(RaycastWeapon());
        StartCoroutine(RaycastCounter());
    }

    IEnumerator RaycastWeapon()
    {
        //Ray ray = new Ray(transform.position, transform.up * -1);
        rayOrig = transform.position;
        //Ray ray = new Ray(transform.position, transform.up * -1);
        if (Physics.Raycast(transform.position, transform.up * -1, out rayHit))
        {
            
            line.enabled = true;
            line.SetPosition(0, rayOrig);
            line.SetPosition(1, rayHit.point);
            //_controller.focus = rayHit.collider.GetComponent<Transform>();

            if(rayHit.rigidbody != null)
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
