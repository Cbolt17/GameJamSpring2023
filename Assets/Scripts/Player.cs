using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isReady = false;
    public item item;

    private WorldManager worldManager;

    void Start()
    {
        //You can ignore this (but keep it)
        worldManager = WorldManager.instance;
        if(worldManager.canJoin)
        {
            worldManager.players.Add(this);
            worldManager.PlayerAdded();
        }
        else
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Function that does stuff based on item. Yield return new WaitForSeconds(1f) 
    /// makes it so that the function waits 1 sec before resuming at the next line.
    /// 1 second is just my estimate of the animation for a single person's turn,
    /// probably gonna be less though
    /// </summary>
    /// <returns></returns>
    public IEnumerator TakeTurn()
    {


        yield return new WaitForSeconds(1f);
    }
}

[System.Serializable]
public struct item
{
    public int target;
    public int type;
    public int num;
}