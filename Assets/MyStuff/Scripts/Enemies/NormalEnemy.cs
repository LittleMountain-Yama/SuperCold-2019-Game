using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
     //AudioSource _au;

    protected override void Awake()
    {
        base.Awake();

        //_au = GetComponent<AudioSource>();
        _bc = GetComponent<BoxCollider>();

        var sound = Resources.Load("NormalGrunt");
        grunt = sound as AudioClip;

        life = 21;
        speed = 0.4f;
        range = 12;
        attackRange = 3;
        damage = 1;
    }    
}
