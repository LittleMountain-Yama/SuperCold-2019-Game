using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : HandGun, IUpdate
{
    public AudioClip rifleClip;
    public AudioClip noAmmo;
    public AudioSource source;
    public GameObject particle;

    protected override void Awake()
    {
        base.Awake();
        throwForce = 20;       
        magazine = 24;
        cooldown = 0.475f;

        source = GetComponent<AudioSource>();

        var bullet = Resources.Load("rifleBulletPrefab");
        bulletPrefabric = bullet as GameObject;

        var sound = Resources.Load("RifleShoot");
        rifleClip = sound as AudioClip;

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
            StartCoroutine(Burst(0));
            StartCoroutine(Burst(0.15f));
            StartCoroutine(Burst(0.3f));
            canShoot = false;
            StartCoroutine(ShootCooldown(cooldown));            
        }
    }

    protected new void OutOfBullets()
    {
        base.OutOfBullets();
        source.PlayOneShot(noAmmo);
    }

    IEnumerator Burst(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(bulletPrefabric, SpawnBullet.transform.position, transform.rotation);
        magazine -= 1;
        _gm.currentAmmoCount = magazine;
        source.PlayOneShot(rifleClip);
        particle.SetActive(true);
        StartCoroutine(ResetLight());
        StopCoroutine(Burst(time));
    }

    public IEnumerator ResetLight()
    {
        yield return new WaitForSeconds(0.1f);
        particle.SetActive(false);
    }
}
