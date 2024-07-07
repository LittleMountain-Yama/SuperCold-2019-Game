using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour, IPickUp, IUpdate
{    
    private AudioSource source;    
    public BoxCollider bc;

    public Transform playerCam;
    public Transform placeHolder;

    public Player pl;
    public FirstPersonCamera fpc;
    public Renderer rend;

    public List<AudioClip> allSounds;

    public bool beingCarried;
    public bool rayCasted;
    
    void Awake()
    {        
        pl = GameObject.Find("player").GetComponent<Player>();
        fpc = GameObject.Find("MainCamera").GetComponent<FirstPersonCamera>();
        playerCam = GameObject.Find("MainCamera").GetComponent<Transform>();
        placeHolder = GameObject.Find("placeHolder").GetComponent<Transform>();

        bc = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();

        rayCasted = false;
        beingCarried = false;
    }

    void Start()
    {
        bc = GetComponent<BoxCollider>();
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        if (pl.grabIntention == true && pl.holdingSomething == false && rayCasted == true)
        {
            PickUp();

        }     

        if (beingCarried == true)
        {
            
                Eat();
                pl.holdingFood = false;
            
        }
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        gameObject.transform.position = placeHolder.position;
        transform.parent = playerCam;

        pl.holdingSomething = true;
        pl.holdingFood = true;        
        beingCarried = true;
        bc.isTrigger = true;

        source.PlayOneShot(allSounds[0]);
    }

    public void Eat()
    {
        rend.enabled = false;
        source.PlayOneShot(allSounds[1]);
        pl.holdingSomething = false;
        pl.life++;
        pl.eatIntention = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        beingCarried = false;
        bc.isTrigger = false;
        rayCasted = false;
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(DestroyTime());
        Destroy(this);
    }
}
