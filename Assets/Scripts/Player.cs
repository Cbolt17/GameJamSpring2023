
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;


public class Player : MonoBehaviour
{
    public bool isReady = false;

    private int hp;
    List<Player> connections;

    public Player target;
    public Enums.Status status, nextStatus;
    public float statusDuration, nextDuration;
    public (Type, int) itemID;
    public Item item;

    private WorldManager worldManager;

    void Start()
    {
        //You can ignore this (but keep it)
        worldManager = WorldManager.instance;
        if (worldManager.canJoin)
        {
            worldManager.players.Add(this);
            worldManager.PlayerAdded();
        }
        else
        {
            Application.Quit();
        }
    }

    public void takeDamage(int x)
    {
        this.hp -= x;
    }

    public List<Player> getConnections()
    {
        return connections;
    }

    public int getHP()
    {
        return hp;
    }
}