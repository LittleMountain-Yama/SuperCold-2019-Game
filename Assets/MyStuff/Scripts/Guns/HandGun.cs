using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour,IPickUp, IThrow, IUpdate
{
    public BoxCollider bc;

    public Transform playerCam;
    public Transform placeHolder;
    public Transform lookAt;

    public GameObject bulletPrefabric;
    public GameObject boomEffect;
    public GameObject lostBulletEffect;

    public Player pl;
    public BulletSpawn SpawnBullet;

    protected GameManager _gm;

    public bool playerClose;
    public bool beingCarried;
    public bool rayCasted;
    protected bool canShoot;

    public int magazine; 

    public float throwForce;   
    public float speed;
    protected float cooldown;

    protected virtual void Awake()
    {       
        bc = GetComponent<BoxCollider>();

        playerCam = GameObject.Find("MainCamera").GetComponent<Transform>();        
        placeHolder = GameObject.Find("placeHolder").GetComponent<Transform>();
        lookAt = GameObject.Find("lookAt").GetComponent<Transform>();

        pl = FindObjectOfType<Player>();
        SpawnBullet = GameObject.Find("BulletSpawn").GetComponent<BulletSpawn>();
        _gm = FindObjectOfType<GameManager>();        

        playerClose = false;
        beingCarried = false;
        rayCasted = false;
        canShoot = true;

        speed = 20;
        magazine = 2;
        cooldown = 1;
    }

    void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public virtual void OnUpdate()
    {
        if (pl.grabIntention == true && pl.holdingSomething == false && pl.holdingGun == false && rayCasted == true)
        {
            PickUp();
        }

        if (beingCarried == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Throw();
            }
        }

        if (magazine == 0)
        {
            OutOfBullets();
        }
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        SpawnBullet = GetComponentInChildren<BulletSpawn>();  

        transform.parent = playerCam;
        transform.LookAt(lookAt);
        gameObject.transform.position = placeHolder.position;

        pl.holdingGun = true;
        pl.holdingSomething = true;
        bc.isTrigger = true;
        beingCarried = true;               

        pl.hg = this;

        _gm.ToggleAmmo();
        _gm.currentAmmoCount = magazine;
    }

    public void Throw()
    {
        pl.holdingGun = false;
        pl.holdingSomething = false;
        bc.isTrigger = false;
        beingCarried = false;        
        rayCasted = false;

        transform.parent = null;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);

        _gm.ToggleAmmo();
    }

    public virtual void UseGun()
    {
        if (magazine > 0 && canShoot == true)
        {
            Instantiate(bulletPrefabric, SpawnBullet.transform.position, transform.rotation);
            magazine -= 1;
            _gm.currentAmmoCount = magazine;
            canShoot = false;
            StartCoroutine(ShootCooldown(cooldown));
        }
    }

    protected void OutOfBullets()
    {
        _gm.ToggleAmmo();

        UpdateManager.Instance.RemoveFromUpdate(this);

        StartCoroutine(TimeToDie());
    }

    protected IEnumerator ShootCooldown(float cooldownShoot)
    {
        yield return new WaitForSeconds(cooldownShoot);
        canShoot = true;
        StopCoroutine(ShootCooldown(cooldown));
    }

    protected IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(0.8f);
        
        Destroy(this.gameObject);
        StopCoroutine(TimeToDie());
        pl.holdingGun = false;
        pl.holdingSomething = false;
        bc.isTrigger = false;
        beingCarried = false;
        rayCasted = false;
    }
}
