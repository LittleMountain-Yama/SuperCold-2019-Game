using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUpdate, IFixUpdate
{
    public Player pl;

    Rigidbody rb;
    Animator anim;
    protected BoxCollider _bc;
    AudioSource _au;
    GameManager _gm;
    SkinnedMeshRenderer _r;

    public AudioClip grunt;
    public AudioClip hit;

    protected float speed = 0.4f;
    
    public float dis;
    public float topRange = 2;
    public float stunTimer;
    protected float gruntTimer;

    //public float magnitudStore;

    protected int range;
    public float life;
    protected int damage;
    protected int attackRange;
    protected int random;

    public bool startKnockBack;
    public bool knockedBack = false;    

    //Bools para el animator
    public bool isAttacking;
    public bool isChasing;
    public bool isDamaged;
    public bool isDying;

    protected virtual void Awake()
    {       
        pl = GameObject.Find("player").GetComponent<Player>();

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _au = GetComponent<AudioSource>();
        _gm = FindObjectOfType<GameManager>();           

        _gm.enemiesInScene.Add(this.gameObject);

        var sound = Resources.Load("ZombieHit");
        hit = sound as AudioClip;

        stunTimer = 3;

        damage = 1;
        attackRange = 1;
        life = 25;
        range = 10;
        random = Random.Range(2, 6);

        isChasing = false;
        isDamaged = false;
        isDying = false;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        UpdateManager.Instance.AddToFixUpdate(this);
    }

    public void OnUpdate()
    {     
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isChasing", isChasing);
        anim.SetBool("isDamaged", isDamaged);
        anim.SetBool("isDying", isDying);              

        dis = Vector3.Distance(pl.transform.position, transform.position);        

        if (startKnockBack == true)
        {            
            StartCoroutine(StunTime());
        }      

        if (life <= 0)
        {
            PreDeath();
        }

        if (knockedBack == false && dis <= 1.9f && isDying == false)
        {
            isAttacking = true;            
        }

        gruntTimer += Time.deltaTime * 1;

        if (gruntTimer >= random)
        {                           
            GruntTime();
        }

        /*if (magnitudStore >= life)
        {
            PreDeath();
        }*/        

        if(range < dis)
        {
            isChasing = false;
        }
    }

    public void OnFixedUpdate()
    {
        if (knockedBack == false && range >= dis && isAttacking == false && isDying == false)
        {
            Chase();
        }
    }

    protected void Chase()
    {
        isChasing = true;

        Vector3 direction = pl.transform.position - transform.position;
        Vector3 dirNormalized = new Vector3(direction.x, 0, direction.z);
        rb.velocity = dirNormalized * speed;
        transform.forward = dirNormalized;

        //rb.MovePosition(transform.position + distance.normalized * Time.deltaTime * speed);

        /*Vector3 direction = pl.rb.position - rb.position;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(direction);
        rb.velocity = newPosition;*/ //este hace que pase la cosa graciosa

        /*Vector3 move = pl.transform.position - transform.position;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(move);
        rb.MovePosition(newPosition);*/    //este hace teleport

        //Vector3 direction = pl.transform.position - transform.position;
        //transform.position += direction.normalized * speed * Time.deltaTime; //este funca pero re a lo bruto

        //float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;
        //transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }       

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(stunTimer);
        knockedBack = false;
        startKnockBack = false;
        isDamaged = false;        
        StopCoroutine(StunTime());
    }

    protected void GruntTime()
    {                
        _au.PlayOneShot(grunt);
        random = Random.Range(6, 10);
        gruntTimer = 0;
    }

    #region AnimatorEvents
    public void Hit()
    {
        if (dis <= attackRange && pl.super == false)
        {
            pl.life -= damage;
            pl.GotHurt();
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    protected void PreDeath()
    {
        isDying = true;
        isAttacking = false;
        isChasing = false;
        isDamaged = false;
        //_bc.isTrigger = true;        
    }

    public void Die()
    {
        UpdateManager.Instance.RemoveFromUpdate(this);
        UpdateManager.Instance.RemoveFromFixUpdate(this);
        _gm.enemiesInScene.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Objects>() && collision.gameObject.GetComponent<Objects>().Throwed == true)
        {
            startKnockBack = true;
            knockedBack = true;
            isDamaged = true;
        }

        if(collision.gameObject.GetComponent<Bullet>())
        {
            _au.PlayOneShot(hit);           
        }
    }
}
