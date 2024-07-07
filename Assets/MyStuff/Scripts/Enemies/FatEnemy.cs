using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatEnemy : Enemy
{   
    protected override void Awake()
    {
        base.Awake();
      
        _bc = GetComponent<BoxCollider>();

        //var sound = Resources.Load("NormalGrunt");
        //grunt = sound as AudioClip;

        life = 30;
        speed = 0.35f;
        range = 11;
        attackRange = 3;
        damage = 1;
    }

    protected void PushPlayer()
    {
        if (dis <= attackRange*1.2f && pl.super == false)
        {
            pl.Knockback(-1.2f, 2);
            pl.GotHurt();
            Debug.Log("Knockback");
        }
    }
}
