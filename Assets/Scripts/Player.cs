using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Item
{
    private int hp;
    private Player connections[];

    public Player target;
    public Status status, nextStatus;
    public int statusDuration, nextDuration; 
    public Usable item;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takeDamage(int x)
    {
        this.hp -= x;
    }


}

public enum Status
{
    NEUTRAL,
    FROZEN,
    MEDITATING
}