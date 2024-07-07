using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    AudioSource _au;
    AudioClip hit;   

    public Image lifeBar;

    public List<GameObject> weaponSpawns, weaponTypes, attackSpawns;
    public GameObject attackSpell;

    Player _pl;

    public int life; 
    public float spawnTimer, spawnTime, quantityFilled, attackTimer, attackTime;
    public bool isInmune;

    protected void Awake()
    {
        _au = GetComponent<AudioSource>();      
        _pl = FindObjectOfType<Player>();

        var sound = Resources.Load("MetalDamage");
        hit = sound as AudioClip;

        var attack = Resources.Load("BossAttack");
        attackSpell = attack as GameObject;

        life = 150;      
       
        spawnTime = 17;
        attackTime = 5;
    }

    private void Update()
    {      
        #region WepSpawn
        spawnTimer += 1 * Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            SpawnWeapon();
        }
        #endregion

        #region AttackSpawn
        attackTimer += 1 * Time.deltaTime;

        if (attackTimer >= attackTime)
        {
            SpawnAttack();
        }
        #endregion

        #region LifeThings
        quantityFilled = life / 150f;
        lifeBar.fillAmount = quantityFilled;           

        if (life <= 0)
        {
            SceneManager.LoadScene("WinLvlFour");
        }
        #endregion      
    }         

    public void ReceiveDamage(int val)
    {
        life -= val;
        _au.PlayOneShot(hit);
    }

    void SpawnWeapon()
    {
        GameObject wepTemp = Instantiate(weaponTypes[Random.Range(0, weaponTypes.Count)]);
        wepTemp.transform.position = weaponSpawns[Random.Range(0, weaponSpawns.Count)].transform.position;
        spawnTimer = 0;
    }

    void SpawnAttack()
    {
        GameObject wepTemp = Instantiate(attackSpell);
        wepTemp.transform.position = weaponSpawns[Random.Range(0, attackSpawns.Count)].transform.position;
        attackTimer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().Knockback(1, 6);
            _pl.life -= 3;
            _pl.GotHurt();
        }
    }
}
