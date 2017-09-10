using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    public Transform buletStore;

    private void OnEnable()
    {
        
        BulletPool.bullets.Add(gameObject.GetComponent<Rigidbody>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletPool.shooting = false;
        //gameObject.GetComponent <Collider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.transform.position = buletStore.position;
        BulletPool.bullets.Add(gameObject.GetComponent<Rigidbody>());
    }
}
