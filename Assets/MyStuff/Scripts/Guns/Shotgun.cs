using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : HandGun, IUpdate
{
    public AudioClip shotgunClip;
    public AudioClip noAmmo;
    public AudioSource source;
    public GameObject particle;

    protected override void Awake()
    {
        base.Awake();
        throwForce = 20;
        magazine = 8;
        cooldown = 0.75f;

        source = GetComponent<AudioSource>();

        var bullet = Resources.Load("shotgunBulletPrefab");
        bulletPrefabric = bullet as GameObject;

        var sound = Resources.Load("ShotgunShoot");
        shotgunClip = sound as AudioClip;

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

            Vector3 right = new Vector3(0, 18, 0);
            Vector3 left = new Vector3(0, -18, 0);
            Vector3 up = new Vector3(7, 0, 0);
            Vector3 down = new Vector3(-7, 0, 0);

            //Middle Row
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, 0, 0), transform.rotation * Quaternion.Euler(right));
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, 0, 0), transform.rotation * Quaternion.Euler(left));

            //Upper Row
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, 0.05f, 0.05f), transform.rotation * Quaternion.Euler(right) * Quaternion.Euler(up));
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, 0.05f, 0.05f), transform.rotation * Quaternion.Euler(up));
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, 0.05f, 0.05f), transform.rotation * Quaternion.Euler(left) * Quaternion.Euler(up));

            //Lower Row
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, -0.05f, 0.05f), transform.rotation * Quaternion.Euler(right) * Quaternion.Euler(down));
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, -0.05f, 0.05f), transform.rotation * Quaternion.Euler(down));
            Instantiate(bulletPrefabric, SpawnBullet.transform.position + new Vector3(0, -0.05f, 0.05f), transform.rotation * Quaternion.Euler(left) * Quaternion.Euler(down));

            source.PlayOneShot(shotgunClip);

            particle.SetActive(true);
            StartCoroutine(ResetLight());
            
            canShoot = false;
            StartCoroutine(ShootCooldown(cooldown));
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
