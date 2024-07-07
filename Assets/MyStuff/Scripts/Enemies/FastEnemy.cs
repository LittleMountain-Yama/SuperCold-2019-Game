using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    //AudioSource _au;

    protected override void Awake()
    {
        base.Awake();

        //_au = GetComponent<AudioSource>();
        _bc = GetComponent<BoxCollider>();

        var sound = Resources.Load("FastGrunt");
        grunt = sound as AudioClip;

        life = 14;
        speed = 0.6f;
        range = 20;
        attackRange = 2;
        damage = 1;      
    }
}
