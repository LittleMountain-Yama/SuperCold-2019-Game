using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour,IFixUpdate
{
    public Transform la;
    public Player pl;
    public FirstPersonCamera cm;
    public float bulletForce = 5;
    Rigidbody rb;
    public RaycastHit raycast;

    Enemy _enemy;

    private Vector3 normalizeDirection;

    public Vector3 lookAtPosition;

    public float speed = 50;

    protected float deathTime = 5;

    protected int damage = 1;

    protected virtual void Awake()
    {
        UpdateManager.Instance.AddToFixUpdate(this);

        la = GameObject.Find("lookAt").GetComponent<Transform>();
        pl = GameObject.Find("player").GetComponent<Player>();
        cm = GameObject.Find("MainCamera").GetComponent<FirstPersonCamera>();

        rb = GetComponent<Rigidbody>();       
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    protected virtual void Start()
    {
        lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cm.bulletZ - (cm.transform.position.z - transform.position.z)));

        normalizeDirection = (lookAtPosition - transform.position).normalized;        
    }

    public virtual void OnFixedUpdate()
    {
        rb.transform.position += normalizeDirection * speed * bulletForce * Time.deltaTime;

        if (pl.boosted == true)
            damage = 1000;
    }

    protected virtual IEnumerator TimeToDie(float f)
    {
        yield return new WaitForSeconds(f);
        Dissapear();
        StopCoroutine(TimeToDie(f));
    }

    protected void Dissapear()
    {
        UpdateManager.Instance.RemoveFromFixUpdate(this);
        Destroy(this.gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            _enemy = collision.gameObject.GetComponent<Enemy>();
            _enemy.life -= damage;
        }

        if(collision.gameObject)
            Dissapear();

        if (collision.gameObject.GetComponent<Boss>())
        {
            if(!collision.gameObject.GetComponent<Boss>().isInmune)
            collision.gameObject.GetComponent<Boss>().ReceiveDamage(damage);    
        }
    }
}
