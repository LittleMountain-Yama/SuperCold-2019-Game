using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();

        damage = 4;
        deathTime = 5;
        bulletForce = 30;
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
