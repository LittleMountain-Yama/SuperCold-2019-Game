using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,IUpdate
{
    UpdateManager _um;
    Player _p;

    #region UI
    //Gameplay UI
    public GameObject game;
    public GameObject ammoShow;
    public GameObject horde;
    bool hordeActivated;

    public GameObject HeartA;
    public GameObject HeartB;
    public GameObject HeartC;
    public GameObject HeartD;
    public GameObject HeartE;

    //UI de pausa
    public GameObject pause;
    public GameObject console;
    public GameObject consoleOpener;
    public GameObject consoleCloser;

    public Text tanksUsed;   //El texto que muestra los tanques
    public Text currentAmmo; //Balas que tenés
    #endregion

    public List<GameObject> tankList = new List<GameObject>();

    public int tankCount; //La cantidad de tanques 
    public int currentAmmoCount;

    #region Spawners   
    public List<GameObject> spawners = new List<GameObject>();
    public List<GameObject> enemiesInScene = new List<GameObject>(); //Cantidad de enemigos en escena
    public List<GameObject> prefabsEnemies = new List<GameObject>(); //prefabs de los enemigos

    int maxEnemies;
    float spawnTimer;
    float spawnTime;
    public bool canSpawn;
    #endregion

    string sceneName; 

    private void Awake()
    {        
        game.SetActive(true);        
        ammoShow.SetActive(false);
        horde.SetActive(false);
        hordeActivated = false;

        console.SetActive(false);
        pause.SetActive(false);

        _um = FindObjectOfType<UpdateManager>();
        _p = FindObjectOfType<Player>();

        //Cursor.lockState = CursorLockMode.Locked;

        var normal = Resources.Load("NormalEnemy");
        prefabsEnemies[0] = normal as GameObject;

        var fast = Resources.Load("FastEnemy");
        prefabsEnemies[1] = fast as GameObject;

        var heavy = Resources.Load("HeavyEnemy");
        prefabsEnemies[2] = heavy as GameObject;

        var bald = Resources.Load("BaldZombie");
        prefabsEnemies[3] = bald as GameObject;

        var boom = Resources.Load("BoomEnemy");
        prefabsEnemies[4] = boom as GameObject;

        var small = Resources.Load("SmolEnemyScale");
        prefabsEnemies[5] = small as GameObject;


        spawnTime = 6.5f;

        maxEnemies = 3;

        canSpawn = true;

        //Para poder cambiar el objetivo por nivel
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        #region spawnthings  

        if( 0 <= spawners.Count)
        {
            canSpawn = false;
        }
        
        if (enemiesInScene.Count < maxEnemies)
        {
            canSpawn = true;         
        }
        else if (enemiesInScene.Count >= maxEnemies)
        {
            canSpawn = false;
        }
        
        if (spawnTimer >= spawnTime)
        {
            SpawnTime();
        }

        if (canSpawn == true)
        {
            spawnTimer += Time.deltaTime;
        }
        else
            spawnTimer = 0;

        if ((sceneName == "LvlThree" && tankCount == 3) || (sceneName == "LvlTwo" && tankCount == 3))
        {
            spawnTime = 3.5f;

            maxEnemies = 5;

            if (hordeActivated == false)
            {
                horde.SetActive(true);
            }

            StartCoroutine(HordeTime());
        }
        #endregion
        Hearts();

        if (_p.life == 0)
        {
            StartCoroutine(TimeToLose());
            StopCoroutine(TimeToWin());
        }       

        tanksUsed.text = "" + tankCount;

        currentAmmo.text = "" + currentAmmoCount;

        #region win conditions
        if (sceneName == "LvlTuto" && tankCount == 1)
            StartCoroutine(TimeToWin());

        if (sceneName == "LvlOne" && tankCount == 2)        
            StartCoroutine(TimeToWin());        

        if (sceneName == "LvlTwo" && tankCount == 4)
            StartCoroutine(TimeToWin());

        if (sceneName == "LvlThree" && tankCount == 6)
            StartCoroutine(TimeToWin());
        #endregion
    }

    void SpawnTime()
    {       
        GameObject enemyTemp = Instantiate(prefabsEnemies[Random.Range(0, prefabsEnemies.Count)]);        
        enemyTemp.transform.position = spawners[Random.Range(0, spawners.Count)].transform.position;   
        spawnTimer = 0;
    }

    #region loads
    IEnumerator TimeToWin()
    {
        yield return new WaitForSeconds(2);
        StopCoroutine(TimeToWin());        
        GoToWin();
    }

    IEnumerator TimeToLose()
    {
        yield return new WaitForSeconds(2);
        StopCoroutine(TimeToLose());
        SceneManager.LoadScene("GameOver");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToWin()
    {
        if (sceneName == "LvlTuto")
        {
            SceneManager.LoadScene("WinLvlTuto");
        }

        if (sceneName == "LvlOne")
        {
            SceneManager.LoadScene("WinLvlOne");
        }

        if (sceneName == "LvlTwo")
        {
            SceneManager.LoadScene("WinLvlTwo");
        }

        if (sceneName == "LvlThree")
        {
            SceneManager.LoadScene("WinLvlThree");
        }

        if (sceneName == "LvlFour")
        {
            SceneManager.LoadScene("WinLvlFour");
        }
    }
    #endregion

    #region UI
    public void ToggleAmmo()
    {
        if (ammoShow.activeSelf == false)
        {
            ammoShow.SetActive(true);
        }
        else
        {
            ammoShow.SetActive(false);
            currentAmmo.text = "";
        }
    }

    public void PauseMenu()
    {
        if (_um.pause == false)
        {
            game.SetActive(true);
            pause.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            game.SetActive(false);
            pause.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ToggleConsole()
    {
        if(console.activeSelf == false)
        {
            console.SetActive(true);
            consoleOpener.SetActive(false);
            consoleCloser.SetActive(true);
        }
        else
        {
            console.SetActive(false);            
            consoleOpener.SetActive(true);
            consoleCloser.SetActive(false);
        }        
    }

    void Hearts()
    {
        switch (_p.life)
        {
            case 0:
                HeartA.SetActive(false);
                HeartB.SetActive(false);
                HeartC.SetActive(false);
                HeartD.SetActive(false);
                HeartE.SetActive(false);
                break;
            case 1:
                HeartA.SetActive(true);
                HeartB.SetActive(false);
                HeartC.SetActive(false);
                HeartD.SetActive(false);
                HeartE.SetActive(false);
                break;
            case 2:
                HeartA.SetActive(true);
                HeartB.SetActive(true);
                HeartC.SetActive(false);
                HeartD.SetActive(false);
                HeartE.SetActive(false);
                break;
            case 3:
                HeartA.SetActive(true);
                HeartB.SetActive(true);
                HeartC.SetActive(true);
                HeartD.SetActive(false);
                HeartE.SetActive(false);
                break;
            case 4:
                HeartA.SetActive(true);
                HeartB.SetActive(true);
                HeartC.SetActive(true);
                HeartD.SetActive(true);
                HeartE.SetActive(false);
                break;
            case 5:
                HeartA.SetActive(true);
                HeartB.SetActive(true);
                HeartC.SetActive(true);
                HeartD.SetActive(true);
                HeartE.SetActive(true);
                break;

            default:
                _p.life = 5;
            break;
        }
    }

    IEnumerator HordeTime()
    {
        yield return new WaitForSeconds(3);
        horde.SetActive(false);
        hordeActivated = true;
        StopCoroutine(HordeTime());
    }
#endregion   
}
