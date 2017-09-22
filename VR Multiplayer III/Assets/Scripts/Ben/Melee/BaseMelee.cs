using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class BaseMelee : NetworkBehaviour {

    public static Action MeleeAttack;
    public static Action QuickAttack;
    private bool _attacking;
    //private Coroutine _heavy;


	// Use this for initialization
	void Start () {
        if(!isLocalPlayer)
        {
            this.enabled = false;
        }
        _attacking = false;
        MeleeAttack += MeleeAttackHandler;
        QuickAttack += QuickAttackHandler;
	}

    private void QuickAttackHandler()
    {
        if(!_attacking)
        {
            //if (_heavy != null)
            //    StopCoroutine(_heavy);
            _attacking = true;
            StopCoroutine(WaitForHeavy());
            LightHit.Light();
            StartCoroutine(ResetAttack());
        }
    }

    private void MeleeAttackHandler()
    {
        if (!_attacking)
        {
            /*_heavy = */StartCoroutine(WaitForHeavy());
        }
    }

    IEnumerator WaitForHeavy ()
    {
        
        yield return new WaitForSeconds(2f);
        _attacking = true;
        HeavyHit.Heavy();
        StartCoroutine(ResetAttack());
         
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        _attacking = false;
    }
}
