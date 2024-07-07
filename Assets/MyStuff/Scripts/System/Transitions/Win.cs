using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    ProgressManager _pm;

    string sceneName;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public void GoToLevel()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        if(sceneName == "WinLvlTuto")
        {
            SceneManager.LoadScene("LvlOne");
        }

        if (sceneName == "WinLvlOne")
        {
            SceneManager.LoadScene("LvlTwo");
        }

        if (sceneName == "WinLvlTwo")
        {
            SceneManager.LoadScene("LvlThree");
        }

        if (sceneName == "WinLvlThree")
        {
            SceneManager.LoadScene("LvlFour");
        }
    }

    public void UnlockLevel()
    {
        if (sceneName == "WinLvlTuto")
        {
            _pm.UnlockLvlOne();
        }

        if (sceneName == "WinLvlOne")
        {
            _pm.UnlockLvlTwo();
        }

        if (sceneName == "WinLvlTwo")
        {
            _pm.UnlockLvlThree();
        }

        if (sceneName == "WinLvlThree")
        {
            _pm.UnlockLvlFour();
        }
    }
}
