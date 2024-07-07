using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();

        damage = 2;
        deathTime = 3.5f;
        bulletForce = 2.5f;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
