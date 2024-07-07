﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : HandGun, IUpdate
{
    public AudioClip clip;
    public AudioClip noAmmo;
    public AudioSource source;
    public GameObject particle;

    protected override void Awake()
    {
        base.Awake();
        throwForce = 100;
        magazine = 4;
        cooldown = 0.8f;

        source = GetComponent<AudioSource>();

        var bullet = Resources.Load("sniperBulletPrefab");
        bulletPrefabric = bullet as GameObject;

        var sound = Resources.Load("SniperShoot");
        clip = sound as AudioClip;

        var tock = Resources.Load("GunTock");
        noAmmo = tock as AudioClip;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        particle.SetActive(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public new void PickUp()
    {
        base.PickUp();
    }

    public new void Throw()
    {
        base.Throw();
    }

    public override void UseGun()
    {
        if (magazine > 0 && canShoot == true)
        {
            base.UseGun();
            particle.SetActive(true);
            source.PlayOneShot(clip);
            StartCoroutine(ResetLight());
        }
    }

    protected new void OutOfBullets()
    {
        base.OutOfBullets();
        source.PlayOneShot(noAmmo);
    }

    public IEnumerator ResetLight()
    {
        yield return new WaitForSeconds(0.1f);
        particle.SetActive(false);
    }
}
