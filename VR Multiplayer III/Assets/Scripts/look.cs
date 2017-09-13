using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour {

    public Transform head;
    public Transform box;

	
	// Update is called once per frame
	void Update () {
        if ((box.localEulerAngles.z > 85 && box.localEulerAngles.z < 100) || (box.localEulerAngles.z > -85 && box.localEulerAngles.z < -100))
        {
            transform.LookAt(box.position + box.transform.forward);
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        }
        else
        {
            transform.LookAt(box.position + (box.transform.up * -1));
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        }
        
        transform.position = head.position;
        //transform.localEulerAngles = new Vector3(0, 0, 0);
        //transform.rotation = Quaternion.Euler(0, 0, 0);
		
	}
}
