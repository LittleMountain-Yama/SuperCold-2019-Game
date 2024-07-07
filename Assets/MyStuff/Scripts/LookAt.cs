using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public FirstPersonCamera cm;
    
    void Start()
    {
        //cm = GameObject.Find("MainCamera").GetComponent<FirstPersonCamera>();
    }

    void Update()
    {
        //gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cm.bulletZ));
    }
}
