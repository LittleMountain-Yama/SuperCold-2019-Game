using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    public float videoTimer;

    void Update()
    {
        videoTimer += Time.deltaTime;

        if (videoTimer >= 26)
            SceneManager.LoadScene("Menu");

        if (Input.anyKey)
            SceneManager.LoadScene("Menu");
        else if (Input.touchCount > 0)
            SceneManager.LoadScene("Menu");     
    }
}
