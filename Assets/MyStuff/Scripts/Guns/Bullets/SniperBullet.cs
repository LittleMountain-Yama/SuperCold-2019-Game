using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();

        damage = 14;
        deathTime = 8;
        bulletForce = 12;
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
