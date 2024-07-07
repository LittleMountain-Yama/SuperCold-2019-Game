using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSuper : Objects, IPickUp, IUpdate, IFixUpdate
{
    public Renderer rend;
    public AudioClip usePower;

    protected override void Awake()
    {
        base.Awake();

        var sound = Resources.Load("powerUpStar");
        usePower = sound as AudioClip;
    }

    protected override void Start()
    {
        base.Start();
        rend = GetComponent<Renderer>();
    }

    public new void OnUpdate()
    {
        base.OnUpdate();
    }

    public new void OnFixedUpdate()
    {
        if (beingCarried == true)
        {                        
          Consume();            
        }
    }

    public override void PickUp()
    {
        base.PickUp();
    }

    private void Consume()
    {
        rend.enabled = false;
        //source.PlayOneShot(allSounds[1]);
        pl.holdingSomething = false;
        pl.super = true;
        StartCoroutine(pl.ResetSuper());
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        beingCarried = false;
        rayCasted = false;
        source.PlayOneShot(usePower);        
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(DestroyTime());
        Destroy(this.gameObject);
    }
}
