using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mover : NetworkBehaviour {

    public static Action</*string,*/ Controller> CallMover;
    public static GameObject thisDino;

    Controller controller;

    private float newRotate;
    private Quaternion focusRotate;
    private Vector3 moveRotate;
    private Rigidbody characterRigid;
    private float forward;
    private bool jumping;


    // Use this for initialization
    void Start ()
    {

        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        thisDino = gameObject;
        jumping = false;
        characterRigid = gameObject.GetComponent<Rigidbody>();
        CallMover += CallMoverHandler;
    }

    private void CallMoverHandler(/*string command,*/ Controller mover)
    {
        controller = mover;
        controller.jumpSpeed = 2000;
        controller.forwardJmpSpeed = 300;
        if (controller.clicked && !jumping)
        {
            //characterRigid.AddForce(Vector3.up * Time.deltaTime * controller.jumpSpeed);
            StartCoroutine(Jump());
           // StartCoroutine(ForwardForce());
            StartCoroutine(JumpCount());
            StartCoroutine(ResetJump());
            jumping = true;
            StopCoroutine(MoveController());

        }
        if (controller.touching || controller.gripped)
        {
            if (!controller.clicked && !jumping)
            {
                StartCoroutine(MoveController());
            }
            
        }
        
        if(controller.pulledTrigger)
        {
            //StartCoroutine(ShootGun());

        }
            //switch(command)
        //{
        //    case "Touching":
                
        //        StartCoroutine(MoveController());
        //        break;
        //    case "Not Touching":
        //        break;
        //    default:
        //        return;

        //}
    }


    IEnumerator MoveController()
    {
        yield return new WaitForFixedUpdate();
        controller.device = SteamVR_Controller.Input((int)controller.trackedObject.index);
        controller.touchSpot = new Vector2(controller.device.GetAxis().x, controller.device.GetAxis().y);
        

        

        if (controller.touchSpot != new Vector2(0, 0) && !controller.gripped)
        {
            //set input value to always be positive
            if (controller.touchSpot != null)
            {
                forward = controller.device.GetAxis().x + controller.device.GetAxis().y;
                if (forward < 0)
                {
                    forward *= -1;
                }
                if (forward > 1)
                {
                    forward *= .5f;
                }
            }

            //controls transformations
            characterRigid.MovePosition(transform.localPosition + transform.TransformDirection(new Vector3(0 , 0, forward)
                ) * controller.moveSpeed * Time.deltaTime);

            //controls rotation
            newRotate = Mathf.Atan2(controller.device.GetAxis().y * -1, (controller.device.GetAxis().x )) * Mathf.Rad2Deg;
            Quaternion tempRotate = Quaternion.Euler(0, newRotate - ((controller.cameraHead.transform.localEulerAngles.y -90) * -1), 0);    
            transform.localRotation = Quaternion.Slerp(transform.rotation, tempRotate, Time.deltaTime * controller.rotateSpeed);

        }

        if (controller.gripped)
        {
            //controlls rotation
            focusRotate = Quaternion.LookRotation(controller.focus.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, focusRotate, Time.deltaTime * controller.rotateSpeed);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //controlls transformation
            characterRigid.MovePosition(transform.localPosition + transform.TransformDirection
                (new Vector3(controller.touchSpot.x, 0, controller.touchSpot.y)) * controller.moveSpeed * Time.deltaTime);

        }
        if (controller.touching && !controller.clicked)
            StartCoroutine(MoveController());

    }

    IEnumerator Jump()
    {
        yield return new WaitForFixedUpdate();
        characterRigid.AddForce(Vector3.up * controller.jumpSpeed * Time.deltaTime);
        characterRigid.AddForce(transform.forward * controller.moveSpeed * controller.forwardJmpSpeed * Time.deltaTime);
        //transform.Translate(Vector3.up * controller.jumpSpeed * Time.deltaTime);
        if (controller.frameCount < controller.jumpAmount)
            StartCoroutine(Jump());
    }
    //IEnumerator ForwardForce()
    //{
        
    //    //characterRigid.MovePosition(transform.localPosition + transform.TransformDirection(new Vector3(0, 0, forward)) * controller.moveSpeed * 4 * Time.deltaTime);
    //    yield return new WaitForFixedUpdate();
    //    if (jumping)
    //        StartCoroutine(ForwardForce());
    //}

    IEnumerator JumpCount()
    {
        yield return new WaitForSeconds(.1f);
        controller.frameCount++;
        if (controller.frameCount < controller.jumpAmount)
            StartCoroutine(JumpCount());
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1f);
        controller.frameCount = 0;
        jumping = false;
        if (controller.touching || controller.gripped)
        {
            StartCoroutine(MoveController());
        }
    }

}


