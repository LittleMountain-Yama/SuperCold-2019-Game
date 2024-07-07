using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEnemy : Enemy, IDeath
{
    public GameObject puddle;

    protected override void Awake()
    {
        base.Awake();

        //_au = GetComponent<AudioSource>();
        //_bc = GetComponent<BoxCollider>();

        //var sound = Resources.Load("NormalGrunt");
        //grunt = sound as AudioClip;        

        var exp = Resources.Load("Puddle");
        puddle = exp as GameObject;

        life = 21;
        speed = 0.5f;
        range = 10;
        attackRange = 2;
        damage = 1;

        //Instantiate(puddle, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    public void OnDeath()
    {
        Instantiate(puddle, this.gameObject.transform.position + new Vector3(0,0.3f), this.gameObject.transform.rotation);

        Debug.Log("lmao");

        Die();
    }
}
