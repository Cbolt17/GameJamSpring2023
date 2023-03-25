using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enums;


public class Player : MonoBehaviour
{
    private int hp;
    List<Player> connections;

    public Player target;
    public Status status, nextStatus;
    public float statusDuration, nextDuration;
    public (Type, int) itemID;
    public Usable item;
    public bool isReady = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
