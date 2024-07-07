using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public delegate void Commands(params object[] parameterContainer);
    Dictionary<string, Commands> commandsDic = new Dictionary<string, Commands>();

    public InputField consoleView;
    public InputField consoleInput;
    
    GameManager _gm;

    Transform arrow;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject rifle;
    public GameObject apple;

    private void Awake()
    {
        var pistolVar = Resources.Load("pistol");
        pistol = pistolVar as GameObject;

        var shotgunVar = Resources.Load("shotgun");
        shotgun = shotgunVar as GameObject;

        var rifleVar = Resources.Load("rifle");
        rifle = rifleVar as GameObject;

        var appleVar = Resources.Load("apple");
        apple = appleVar as GameObject;

        _gm = FindObjectOfType<GameManager>();

        arrow = FindObjectOfType<Arrow>().transform;

        consoleView.lineType = InputField.LineType.MultiLineNewline;

        AddCommand("Help", Help);
        AddCommand("SpawnPistol", SpawnPistol);
        AddCommand("SpawnShotGun", SpawnShotGun);
        AddCommand("SpawnRifle", SpawnRifle);
        AddCommand("SpawnApple", SpawnApple);
    }

    void AddCommand(string cmdText, Commands cmdVoid)
    {       
        if (!commandsDic.ContainsKey(cmdText))
            commandsDic.Add(cmdText, cmdVoid);
        else
            commandsDic[cmdText] += cmdVoid;
    }

    public void EnterNewCommmand()
    {
        CheckKey(consoleInput.text);
    }

    void CheckKey(string key)
    {
        char[] delimiter = new char[] { '-', ' ' };
        string[] substrings = key.Split(delimiter);
        int value = -1;

        if (substrings.Length > 1)
            value = int.Parse(substrings[substrings.Length - 1]);

        if (commandsDic.ContainsKey(substrings[0]))
        {
            if (value != -1)
                commandsDic[substrings[0]](new object[] { value });
            else
                commandsDic[substrings[0]]();
        }
        else
            consoleView.text += "Command not available";
    }

    #region CommandVoids

    void Help(params object[] parameters)
    {
        consoleView.text += "Help, SpawnPistol, SpawnRifle, SpawnShotgun, SpawnApple";
    }

    void SpawnPistol(params object[] parameters)
    {
        Instantiate(pistol, arrow.transform.position, arrow.transform.rotation);
    }

    void SpawnShotGun(params object[] parameters)
    {
        Instantiate(shotgun, arrow.transform.position, arrow.transform.rotation);
    }

    void SpawnRifle(params object[] parameters)
    {
        Instantiate(rifle, arrow.transform.position, arrow.transform.rotation);
    }

    void SpawnApple(params object[] parameters)
    {
        Instantiate(apple, arrow.transform.position, arrow.transform.rotation);
    }   

    #endregion
}
