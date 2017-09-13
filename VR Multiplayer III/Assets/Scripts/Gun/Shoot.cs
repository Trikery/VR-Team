using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour {
    public static Action shooter;

    public Rigidbody curentBullet;
    public float bulletSpeed;

    //public GameObject gun;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
        }

        shooter += ShooterHandler;
        bulletSpeed = 100;
    }

    private void ShooterHandler()
    {
        //if (!BulletPool.shooting)
        //{
            curentBullet = BulletPool.bullets[0];
            BulletPool.bullets.Remove(curentBullet);
            curentBullet.transform.position = transform.position + (transform.forward * 2);
            curentBullet.transform.rotation = transform.rotation;
            curentBullet.transform.localEulerAngles = transform.localEulerAngles;
            curentBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed * 10);
            if (BulletPool.bullets != null)
            {
                curentBullet = BulletPool.bullets[0];
            }
            else
                print("Not Enough Bullets");
        //}
    }
}
