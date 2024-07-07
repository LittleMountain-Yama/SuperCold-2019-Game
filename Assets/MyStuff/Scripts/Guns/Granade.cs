using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Objects, IPickUp, IUpdate
{
    public GameObject Big;
    MeshRenderer mr;
    BoxCollider bc;
    public AudioClip clipBoom;
    public AudioClip clipThrow;
    public AudioClip clipImpact;
    public float radiusOfBoom = 7f;
    public float forceOfBoom = 500f;

    protected override void Awake()
    {
        base.Awake();
        var exp = Resources.Load("BigExplosionEffect");
        Big = exp as GameObject;

        var sound = Resources.Load("GRANADA");
        clipBoom = sound as AudioClip;

        var soundT = Resources.Load("GranadaTiro");
        clipThrow = soundT as AudioClip;

        var soundI = Resources.Load("GrandaBoing");
        clipImpact = soundI as AudioClip;

        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
        throwForce = 70;
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
        if(ThrowedBye == false)
        base.PickUp();

        pl.holdingGrenade = true;
        pl.frag = this;
    }

    public override void Throw()
    {
        base.Throw();
        StartCoroutine(ExpTime());
        source.PlayOneShot(clipThrow);
        ThrowedBye = true;
        pl.holdingGrenade = false;
        pl.frag = null;
    }

    private void Explode()
    {
        Instantiate(Big, this.gameObject.transform.position, this.gameObject.transform.rotation);

        source.PlayOneShot(clipBoom);

        StartCoroutine(DestroyTime());

        bc.isTrigger = true;
        mr.enabled = false;

        gameObject.layer = 18;

        GetComponent<Rigidbody>().isKinematic = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusOfBoom);      

        foreach (Collider nearby in colliders)
        {
            Enemy enemies = nearby.GetComponent<Enemy>();

            Player pl = nearby.GetComponent<Player>();

            if (enemies != null)
            {
                var proximity = (transform.position - enemies.transform.position).magnitude;
                enemies.life -= proximity;
                Debug.Log(enemies.life);
            }            

            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(forceOfBoom, transform.position, radiusOfBoom);
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject)
        {
            source.PlayOneShot(clipImpact);
        }
    }

    public IEnumerator ExpTime()
    {
        yield return new WaitForSeconds(4f);
        Explode();
    }

    public IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }   
}
