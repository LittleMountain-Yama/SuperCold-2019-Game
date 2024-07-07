using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour, IPickUp, IThrow, IUpdate, IFixUpdate
{    
    protected AudioSource source;

    public Player pl;
    private FirstPersonCamera fpc;

    private Transform playerCam;
    private Transform la;
    private Transform placeHolder;   
    private BoxCollider bc;

    public List<AudioClip> allSounds;

    protected float throwForce;

    public float speed;

    float goneTimer;

    public bool beingCarried;    
    public bool Throwed;    
    public bool rayCasted;
    public bool ThrowedBye;

    protected virtual void Awake()
    {       
        source = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider>();

        pl = GameObject.Find("player").GetComponent<Player>();
        fpc = GameObject.Find("MainCamera").GetComponent<FirstPersonCamera>();

        playerCam = GameObject.Find("MainCamera").GetComponent<Transform>();
        placeHolder = GameObject.Find("placeHolder").GetComponent<Transform>();
        la = GameObject.Find("lookAt").GetComponent<Transform>();

        speed = 20;
    }

    protected virtual void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        UpdateManager.Instance.AddToFixUpdate(this);

        rayCasted = false;
        beingCarried = false;
        Throwed = false;

        throwForce = 95;
    }

    public void OnUpdate()
    {
        if (pl.grabIntention == true && pl.holdingSomething == false && pl.holdingGun == false && rayCasted == true)
        {
            PickUp();
        }      
    }       

    public void OnFixedUpdate()
    {
        if (beingCarried == true && pl.hasAccelerometer == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Throw();
            }
        }
        else if(beingCarried == true && pl.hasAccelerometer == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("lanzo mobile");
            }
        }
    }

    public virtual void PickUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        pl.holdingSomething = true;       
        bc.isTrigger = true;
        beingCarried = true;
        pl.obj = this;
        pl.holdingObject = true;

        transform.parent = playerCam;
        gameObject.transform.position = placeHolder.position;
        
        //source.PlayOneShot(allSounds[0]);
    }

    public virtual void Throw()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce((la.position - transform.position) * throwForce);

        transform.parent = null;

        pl.holdingSomething = false;
        pl.holdingObject = false;
        bc.isTrigger = false;
        beingCarried = false;        
        rayCasted = false;
        Throwed = true;
        pl.obj = null;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Throwed = false;
        }
    }
}
