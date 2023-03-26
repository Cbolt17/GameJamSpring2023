
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;
using static UnityEditor.Progress;


public class Player : MonoBehaviour
{
    public bool isReady = false;

    private int hp;
    List<Player> connections;

    public Player target;
    public Enums.Status status, nextStatus;
    public int statusDuration, nextDuration;
    public (Type, int) itemID;
    public Item item;
    public Sprite sprite;

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

    public void takeDamage(int x)
    {
        if (this.status == Enums.Status.DEFLECTING || this.status == Enums.Status.DEFENDED) {
            this.statusDuration -= 1;
        } else if (this.status == Enums.Status.VULNERABLE) {
            this.hp -= 2 * x;
            this.statusDuration -= 1;
        } else { 
            this.hp -= x;
        }
    }

    public List<Player> getConnections()
    {
        return connections;
    }

    public int getHP()
    {
        return hp;
    }

    public void updateStatus()
    {
        if (this.statusDuration <= 0) {
            this.statusDuration = this.nextDuration;
            this.status = this.nextStatus;
            this.nextStatus = Enums.Status.NORMAL;
            this.nextDuration = 0;
        }
    }
}