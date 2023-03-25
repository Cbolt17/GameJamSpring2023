using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    public GameObject combatCircle;
    public List<Player> players = new List<Player>();

    public bool allResponsesIn = true;
    public int responsesRemaining = 0;

    public bool canJoin = true;
    public int decisionTime;
    public int numPlayers;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    void Start()
    {

    }

    public void StartGame()
    {
        canJoin = false;
        allResponsesIn = false;
    }

    public void PlayerAdded()
    {
        float degreesBetweenPlayers = 360f / players.Count;
        float diameter = players.Count * 1.5f;
        float radius = diameter / 2;
        combatCircle.transform.localScale = new Vector2(diameter, diameter);
        for(int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = (Vector2)(Quaternion.Euler(0, 0, degreesBetweenPlayers * i) * Vector2.right) * radius;
            players[i].mag = radius;
            players[i].angle = degreesBetweenPlayers * i;
        }
    }

}
