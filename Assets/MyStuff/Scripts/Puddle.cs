using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(TimeAlive());
    }

    IEnumerator TimeAlive()
    {
        yield return new WaitForSeconds(7);
        Destroy(this.gameObject);
        StopCoroutine(TimeAlive());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().Slow();
            Debug.Log("Entering Slow State");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().ReturnSpeed();
            Debug.Log("Exiting Slow State");
        }
    }
}
