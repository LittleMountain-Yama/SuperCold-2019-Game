using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour, IUpdate, IFixUpdate
{
    public Image img;
    public Sprite spriteOne;
    public Sprite spriteTwo;
    public Sprite spriteThree;
    public RaycastHit alwaysLooking;
    public RaycastHit alwaysLookingLarge;

    public Player pl;

    public GameObject grabHud;

    //PRUEBAS MIAS 
    public TouchField tf;

    private float CameraAngle;
    public float CameraAngleSpeed = 2;

    private GameObject player;

    public bool hitSomething;

    public float sensitivity;
    public float smoothness;
    public float bulletZ;
    public float mistake;

    float camHorizontal;
    float camVertical;   

    public Vector2 smoothVelocity;
    public Vector2 currentLooking;

    private void Awake()
    {
        grabHud.SetActive(false);
    }

    void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        UpdateManager.Instance.AddToFixUpdate(this);

        player = GameObject.Find("player");

        hitSomething = false;
    }

    public void OnUpdate()
    {    
        if (currentLooking.y > 75)
            currentLooking.y = 75;

        if (currentLooking.y < -75)
            currentLooking.y = -75;

        if (pl.grabIntention == true)
        {
            InEyesight();
        }

        if (Physics.Raycast(transform.position, transform.forward, out alwaysLookingLarge, 50))
        {
            if (alwaysLookingLarge.collider.gameObject)
            {
                bulletZ = alwaysLookingLarge.distance;

                //bala toma distancia desde la posición de la camara, malo

                //Debug.Log(bulletZ);               
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out alwaysLooking, 3f))
        {
            Objects obj = alwaysLooking.transform.GetComponent<Objects>();
            HandGun hg = alwaysLooking.transform.GetComponent<HandGun>();
            Eatable eb = alwaysLooking.transform.GetComponent<Eatable>();

            if (obj != null && pl.holdingSomething == false && obj.ThrowedBye == false)
                img.sprite = spriteTwo;

            else if (hg != null && pl.holdingSomething == false)
                img.sprite = spriteTwo;

            else if (eb != null && pl.holdingSomething == false)
                img.sprite = spriteThree;

            else if (pl.holdingSomething == true)
                StartCoroutine(ResetSprite());
        }
        else
            StartCoroutine(ResetSprite());

        if(pl.hasAccelerometer)
        {
            if (img.sprite == spriteTwo || pl.holdingSomething)
               grabHud.SetActive(true);
            else
                grabHud.SetActive(false);
        }        
    }

    void InEyesight()
    {
        RaycastHit lookingAt;

        if (Physics.Raycast(transform.position, transform.forward, out lookingAt, 3f))
        {
            //Debug.Log(lookingAt.collider.name);             
            Objects obj = lookingAt.transform.GetComponent<Objects>();
            HandGun hg = lookingAt.transform.GetComponent<HandGun>();
            Eatable eb = lookingAt.transform.GetComponent<Eatable>();

            if (obj != null)
                obj.rayCasted = true;

            if (hg != null)
                hg.rayCasted = true;

            if (eb != null)
                eb.rayCasted = true;
        }
    }

    public void OnFixedUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        //ORIGINAL

        if(pl.hasAccelerometer == false)
        {
            Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            inputValues = Vector2.Scale(inputValues, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
            smoothVelocity.x = Mathf.Lerp(smoothVelocity.x, inputValues.x, 1f / smoothness);
            smoothVelocity.y = Mathf.Lerp(smoothVelocity.y, inputValues.y, 1f / smoothness);

            currentLooking += smoothVelocity;

            transform.localRotation = Quaternion.AngleAxis(-currentLooking.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(currentLooking.x, player.transform.up);
        }

        //CELU
        else if (pl.hasAccelerometer == true)
        {
            smoothVelocity.x = Mathf.Lerp(smoothVelocity.x, tf.TouchDist.x / sensitivity, 1f / smoothness);
            smoothVelocity.y = Mathf.Lerp(smoothVelocity.y, tf.TouchDist.y / sensitivity, 1f / smoothness);

            currentLooking += smoothVelocity;

            transform.localRotation = Quaternion.AngleAxis(-currentLooking.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(currentLooking.x, player.transform.up);
        }    
    }

    IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(0.05f);
        img.sprite = spriteOne;
    }
}
