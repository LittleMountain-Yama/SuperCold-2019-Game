using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileBrain : IController
{
    public Player pl;

    public MobileBrain(Player player)
    {
        pl = player;
    }

    public void ListenerKey()
    {
        pl.dirHorizontal = pl.js.Horizontal;
        pl.dirVertical = pl.js.Vertical;           
    }   
}
