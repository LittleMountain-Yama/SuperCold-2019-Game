using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStrength : Objects, IPickUp, IUpdate, IFixUpdate
{
    public Renderer rend;
    public AudioClip usePowere;

    protected override void Awake()
    {
        base.Awake();

        var sound = Resources.Load("powerUpGuns");
        usePowere = sound as AudioClip;
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
        source.PlayOneShot(usePowere);
        rend.enabled = false;
        pl.holdingSomething = false;
        pl.boosted = true;
        StartCoroutine(pl.ResetBoost());
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        beingCarried = false;
        rayCasted = false;        
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(DestroyTime());
        Destroy(this.gameObject);
    }
}
