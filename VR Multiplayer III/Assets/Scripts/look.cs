using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour {

    public Transform head;
    public Transform nose;

	
	// Update is called once per frame
	void Update () {
        if ((nose.localEulerAngles.z > 85 && nose.localEulerAngles.z < 100) || (nose.localEulerAngles.z > -85 && nose.localEulerAngles.z < -100))
        {
            transform.LookAt(nose.position + nose.transform.forward);
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        }
        else
        {
            transform.LookAt(nose.position + nose.transform.up * -1);
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        }
        
        transform.position = head.position;
        //transform.localEulerAngles = new Vector3(0, 0, 0);
        //transform.rotation = Quaternion.Euler(0, 0, 0);
		
	}
}
