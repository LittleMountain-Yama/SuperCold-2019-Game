using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    //AudioSource _au;

    protected override void Awake()
    {
        base.Awake();

        //_au = GetComponent<AudioSource>();
        _bc = GetComponent<BoxCollider>();

        var sound = Resources.Load("HeavyGrunt");
        grunt = sound as AudioClip;

        life = 37;
        speed = 0.2f;
        range = 13;
        attackRange = 2;
        damage = 2;
    }
}
