using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankConsumer : MonoBehaviour
{
    //Esto hace que los tanques se mueran

    AudioSource _au;

    AudioClip ding;

    private void Awake()
    {
        _au = GetComponent<AudioSource>();

        var sound = Resources.Load("TankDing");
        ding = sound as AudioClip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tank>())
        {
            _au.PlayOneShot(ding);
        }
    }

}
