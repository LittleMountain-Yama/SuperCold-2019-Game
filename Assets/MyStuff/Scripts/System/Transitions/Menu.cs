using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<AudioClip> allSounds;
    public AudioSource source;

    //Zona principal
    public GameObject principal, tutorial, selector;

    public List<GameObject> buttons;

    ProgressManager _pm;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        _pm = FindObjectOfType<ProgressManager>();
    }

    private void Start()
    {
        principal.SetActive(true);
        tutorial.SetActive(false);
        selector.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            _pm.UnlockAllNoSave();
            CheckUnlock();
        }        
    }

    #region Levels
    public void GoToTutorial()
    {
        SceneManager.LoadScene("LvlTutorial");
        //source.PlayOneShot(allSounds[1]);
    }

    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Lvl1");
        //source.PlayOneShot(allSounds[1]);
    }

    public void GoToLevelTwo()
    {
        SceneManager.LoadScene("Lvl2");
        //source.PlayOneShot(allSounds[1]);
    }

    public void GoToLevelThree()
    {
        SceneManager.LoadScene("Lvl3");
        //source.PlayOneShot(allSounds[1]);
    }

    public void GoToLevelFour()
    {
        SceneManager.LoadScene("Lvl4");
        //source.PlayOneShot(allSounds[1]);
    }
    #endregion

    public void GoToMenu()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void ToggleTutorial()
    {
        if (tutorial.activeSelf == true)
        {
            tutorial.SetActive(false);
            principal.SetActive(true);
        }
        else
        {
            tutorial.SetActive(true);
            principal.SetActive(false);
        }
    }

    public void ToggleSelector()
    {
        if (selector.activeSelf == true)
        {
            selector.SetActive(false);
            principal.SetActive(true);
        }
        else
        {
            selector.SetActive(true);
            principal.SetActive(false);
        }
    }

    public void CheckUnlock()
    {
        if(_pm.lvlOne == false)
        {
            buttons[0].SetActive(false);
        }
        else
            buttons[0].SetActive(true);

        if (_pm.lvlTwo == false)
        {
            buttons[1].SetActive(false);
        }
        else
            buttons[1].SetActive(true);

        if (_pm.lvlThree == false)
        {
            buttons[2].SetActive(false);
        }
        else
            buttons[2].SetActive(true);

        if (_pm.lvlFour == false)
        {
            buttons[3].SetActive(false);
        }
        else
            buttons[3].SetActive(true);
    }
}

