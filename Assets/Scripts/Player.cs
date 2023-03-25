using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isReady = false;
    public item item;
    public float mag;
    public float angle;

    private WorldManager worldManager;

    void Start()
    {
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
}

[System.Serializable]
public struct item
{
    public int target;
    public int type;
    public int num;
}