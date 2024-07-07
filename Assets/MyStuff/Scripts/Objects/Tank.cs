using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Objects, IPickUp, IUpdate
{
    GameManager _gm;

    protected override void Awake()
    {
        base.Awake();
        _gm = FindObjectOfType<GameManager>();
        _gm.tankList.Add(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        throwForce = 50;
        gameObject.layer = 13;
    }

    public new void OnUpdate()
    {
        base.OnUpdate();
    }

    public new void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void PickUp()
    {
        base.PickUp();

        pl.holdingTank = true;
    }

    public override void Throw()
    {
        base.Throw();
        pl.holdingTank = false;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.GetComponent<TankConsumer>())
        {
            _gm.tankCount += 1;
            _gm.tankList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
