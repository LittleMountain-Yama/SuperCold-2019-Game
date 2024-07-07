using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainPlayer: IController 
{
    public Player pl;

    public BrainPlayer(Player player)
    {
        pl = player;
    }

    public void ListenerKey()
    {
        pl.dirHorizontal = Input.GetAxis("Horizontal");
        pl.dirVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && pl.isJumping == false)
        pl.Jump();

        if (Input.GetMouseButton(0))
            pl.grabIntention = true;
        else
            pl.grabIntention = false;

        if (Input.GetMouseButtonDown(0) && pl.holdingGun == true)
            pl.fireGun = true;

        else
            pl.fireGun = false;

        //if (Input.GetMouseButtonDown(1) && pl.holdingFood == true)
            //pl.Eat();
            //pl.eatIntention = true;

    }
}
