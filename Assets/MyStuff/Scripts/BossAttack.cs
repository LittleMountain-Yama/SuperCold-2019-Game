using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Player _pl;
    AudioSource _as;
    public AudioClip clipBoom;
    public GameObject Big;

    public float radiusOfBoom = 7f;
    public float forceOfBoom = 500f;

    private void Awake()
    {
        _pl = FindObjectOfType<Player>();
        _as = GetComponent<AudioSource>();

        var sound = Resources.Load("GRANADA");
        clipBoom = sound as AudioClip;

        var exp = Resources.Load("BigExplosionEffect");
        Big = exp as GameObject;

        StartCoroutine(ExpTime());
    }

    private void Explode()
    {
        Instantiate(Big, this.gameObject.transform.position, this.gameObject.transform.rotation);

        _as.PlayOneShot(clipBoom);               

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

            if (pl != null)
            {
                _pl.life -= 1;
                _pl.GotHurt();
            }

            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(forceOfBoom, transform.position, radiusOfBoom);
            }
        }

        Destroy(this.gameObject);
    }

    public IEnumerator ExpTime()
    {
        yield return new WaitForSeconds(1.7f);
        Explode();
    }
}
