using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LController : MonoBehaviour {

    public SteamVR_TrackedController controller;
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;

    private Vector2 _TouchSpot;
    public Controller _RController;
    public int _FocusCount;

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller.PadTouched += LPadTouched;
        controller.PadUntouched += LPadUntouched;
    }

    private void LPadTouched(object sender, ClickedEventArgs e)
    {
        
        if (_RController.focus != null && _RController.gripped)
        {
            device = SteamVR_Controller.Input((int)trackedObject.index);
            _TouchSpot = new Vector2(device.GetAxis().x, device.GetAxis().y);
            for (int i = 0; i < _RController.allFocus.Count; i++)
            {
                if(_RController.focus.gameObject == _RController.allFocus[i].gameObject)
                {
                    _FocusCount = i;
                }
            }
            if (_TouchSpot.x < -.5f)
            {
                _RController.focus.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1);
                if (_FocusCount < _RController.allFocus.Count -1)
                {
                    _FocusCount++;
                    
                }else
                {
                    _FocusCount = 0;

                }
                
                _RController.focus = _RController.allFocus[_FocusCount];
                _RController.focus.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", _RController.outlineSize);
            }
            if (_TouchSpot.x > .5f)
            {
                _RController.focus.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1);
                if (_FocusCount > 0)
                {
                    _FocusCount--;

                }
                else
                {
                    _FocusCount = _RController.allFocus.Count - 1;

                }
                _RController.focus = _RController.allFocus[_FocusCount];
                _RController.focus.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", _RController.outlineSize);

            }
            
        }

    }

    private void LPadUntouched(object sender, ClickedEventArgs e)
    {//
        //throw new NotImplementedException();
    }

    
}
