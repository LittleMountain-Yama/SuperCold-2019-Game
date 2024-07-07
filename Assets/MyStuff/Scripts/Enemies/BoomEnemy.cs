using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : Enemy, IDeath
{
    public GameObject explosion;

    protected override void Awake()
    {
        base.Awake();

        life = 12;
        speed = 0.3f;
        range = 11;
        attackRange = 3;
        damage = 1;

        var exp = Resources.Load("PlasmaExplosionEffect");
        explosion = exp as GameObject;
    }

    public void OnDeath()
    {
        Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);

        StartCoroutine(ExplotionEffect());       
    }

    private IEnumerator ExplotionEffect()
    {
        yield return new WaitForSeconds(0.1f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (Collider nearby in colliders)
        {
            Player play = nearby.GetComponent<Player>();

            if (play != null)
            {
                play.life--;
                play.GotSlimed();
                Debug.Log("lmao");
            }

            Rigidbody rbP = nearby.GetComponent<Rigidbody>();

            if (rbP != null)
            {
                rbP.AddExplosionForce(500, transform.position, 2);
            }
        }

        Die();
    }
}
