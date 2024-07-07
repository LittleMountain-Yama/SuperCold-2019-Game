using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();

        damage = 6;
        deathTime = 7;
        bulletForce = 10;
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
