using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IUpdate, IFixUpdate
{    
    public AudioSource source;
    public Rigidbody rb;
    public Joystick js;

    BrainPlayer brain;
    MobileBrain brainMo;
    IController currentController;

    public HandGun hg;
    public Eatable eb;
    public Objects obj;
    public Granade frag;

    public List<AudioClip> allSounds;   

    public Vector3 okbro;

    #region HUDs
    public GameObject hurtHud;
    public GameObject slimeHud;
    public GameObject mobileHud;
    #endregion  

    #region floats
    [SerializeField] float speed;

    public float reloadTime;
    public float jumpForce;    
    public float life;
    public float jumpRayDis;    

    //Directional
    public float dirHorizontal;
    public float dirVertical;
    #endregion

    #region bools
    //public bool eatItem;
    public bool holdingSomething;
    public bool holdingGun;
    public bool holdingFood;
    public bool holdingTank;
    public bool holdingGrenade;
    public bool holdingObject;

    public bool boosted;
    public bool super;

    public bool grabIntention;
    public bool eatIntention;
    public bool isJumping;
    public bool fireGun;

    //Mobile bools
    public bool hasAccelerometer;  
    #endregion

    void Awake()
    {       
        source = GetComponent<AudioSource>();
        fireGun = false;
        isJumping = false;
        holdingFood = false;
        holdingGun = false;
        holdingSomething = false;
        holdingTank = false;    

        jumpForce = 150;
        life = 5;

        hurtHud.SetActive(false);

        if (SystemInfo.deviceType == DeviceType.Handheld)
            hasAccelerometer = true;
        else
            hasAccelerometer = false;
        if (!hasAccelerometer)
        {
            brain = new BrainPlayer(this);
        }
        else
            brainMo = new MobileBrain(this);
    }

    void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        UpdateManager.Instance.AddToFixUpdate(this);

        rb = GetComponent<Rigidbody>();        

        speed = 0.1f;
        life = 5;
        jumpRayDis = 1.1f;
    } 

    public void OnUpdate()
    {      
        #region MobileThinguies
        //Después mover a Awake (o no)

        if (hasAccelerometer)
        {
            currentController = new MobileBrain(this);
            Cursor.lockState = CursorLockMode.None;
            mobileHud.SetActive(true);
            Debug.Log("Mobile Mode on");
        }
        else
        {
            currentController = new BrainPlayer(this);
            Cursor.lockState = CursorLockMode.Locked;
            mobileHud.SetActive(false);
            Debug.Log("Pc Mode On");
        }     
        #endregion

        if (fireGun == true)
        {
            hg.UseGun();
        }        

        if (life < 0)
        {
            life = 0;
        }

        if(super == true)
        {
            StartCoroutine(ResetSuper());
            //source.PlayOneShot(allSounds[6]);
            Debug.Log("pogU");
        }

        if(boosted == true)
        {
            StartCoroutine(ResetBoost());
            Debug.Log("poggers");
        }      
    }

    public void OnFixedUpdate()
    {
        if (life == 0)
        {
            return;
        }

        if (!hasAccelerometer)
        {
            brain.ListenerKey();
            //brainMo.pl = null;
        }

       else if(hasAccelerometer)
       {
            brainMo.ListenerKey();
            //brain.pl = null;
       }           

        Vector3 move = new Vector3(dirHorizontal, 0, dirVertical) * speed;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(move);
        rb.MovePosition(newPosition);
    }

    #region Mobile buttons
    public void Jump()
    {             
        if (isJumping == false)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isJumping = true;
            source.PlayOneShot(allSounds[1]);
        }          
    }

    public void Shoot()
    {
       if (holdingGun == true)
            hg.UseGun();

        if (holdingGrenade)
            frag.Throw();
    }

    public void GrabDrop()
    {
        if (holdingSomething == false)
            grabIntention = true;
        else if(holdingGun == true)
        {
            grabIntention = false;
            hg.Throw();
        }
        else if(holdingObject == true)
        {
            grabIntention = false;
            obj.Throw();
        }

        if (holdingGrenade)
            frag.Throw();
    }  
  
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Floor>())       
            isJumping = false;     
    }

    public void Knockback(float f, float force)
    {
        float velY = rb.velocity.y;
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(new Vector3(0, 2f, 1 * f) * force, ForceMode.Impulse);
    }

    #region States
    public void GotHurt()
    {
        hurtHud.SetActive(true);
        StartCoroutine(HurtTime());
        source.PlayOneShot(allSounds[5]);
    }

    public void GotSlimed()
    {
        slimeHud.SetActive(true);
        StartCoroutine(SlimeTime());
        source.PlayOneShot(allSounds[5]);
    }   

    IEnumerator HurtTime()
    {
        yield return new WaitForSeconds(0.5f);
        hurtHud.SetActive(false);      
        StopCoroutine(HurtTime());
    }

    IEnumerator SlimeTime()
    {
        yield return new WaitForSeconds(4f);
        slimeHud.SetActive(false);
        StopCoroutine(SlimeTime());
    }

    public IEnumerator ResetBoost()
    {
        yield return new WaitForSeconds(10f);
        boosted = false;
        Debug.Log("poggers");
    }

    public IEnumerator ResetSuper()
    {
        source.PlayOneShot(allSounds[6]);
        yield return new WaitForSeconds(10f);
        super = false;
        source.Stop();
        Debug.Log("ndeah");
    }

    public void Slow()
    {
        speed = 0.05f;
    }

    public void ReturnSpeed()
    {
        speed = 0.1f;
    }
    #endregion
}
