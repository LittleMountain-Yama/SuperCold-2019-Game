using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ProgressManager : MonoBehaviour
{
    public bool lvlOne, lvlTwo, lvlThree, lvlFour;
    string savePath, dataValues;
    ManagerLevel managerLevel;

    private void Awake()
    {
        savePath = Application.dataPath + "/SaveData.json";
        dataValues = File.ReadAllText(savePath);

        managerLevel = JsonUtility.FromJson<ManagerLevel>(dataValues);

        Load();
    }

    public void Save()
    {
        managerLevel.valueOne = lvlOne;
        managerLevel.valueTwo = lvlTwo;
        managerLevel.valueThree = lvlThree;
        managerLevel.valueFour = lvlFour;
        dataValues = JsonUtility.ToJson(managerLevel);//Le decimos que pase los nuevos datos a un tipo string

        File.WriteAllText(savePath, dataValues);
    }

    public void Load()
    {
       var load = File.ReadAllText(savePath);

       lvlOne = managerLevel.valueOne;
       lvlTwo = managerLevel.valueTwo;
       lvlThree = managerLevel.valueThree;
       lvlFour = managerLevel.valueFour; 

       Debug.Log(lvlOne + "cargado");           
    }

    #region Unlocks
    public void UnlockLvlOne()
    {
        lvlOne = true;
        Save();
    }

    public void UnlockLvlTwo()
    {
        lvlTwo = true;
        Save();
    }

    public void UnlockLvlThree()
    {
        lvlThree = true;
        Save();
    }

    public void UnlockLvlFour()
    {
        lvlFour = true;
        Save();
    }

    public void UnlockAllNoSave()
    {
        lvlOne = true;
        lvlTwo = true;
        lvlThree = true;
        lvlFour = true;       
    }
    #endregion

    /* Generic
    public void UnlockLvl()
    {

    }
    */
}
