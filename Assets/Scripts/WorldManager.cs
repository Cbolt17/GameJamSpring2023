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
    public float decisionTime = 30f;
    public float remainingDecisionTime = 0f;
    public int maxNumPlayers;
    public int roundStage = 0; //Round stage 0: rounds not going; Round stage 1: players selecting items; round stage 2: animations
    public float animationTime = 1f;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    private void Update()
    {
        if (roundStage == 1)
        {
            remainingDecisionTime -= Time.deltaTime;
            if (allResponsesIn || remainingDecisionTime < 0)
            {
                roundStage = 2;
                StartCoroutine(StartAnimations());
            }
        }
    }

    public void StartGame()
    {
        canJoin = false;
        allResponsesIn = false;
        responsesRemaining = players.Count;
        remainingDecisionTime = decisionTime;
        roundStage = 1;
    }

    private IEnumerator StartAnimations()
    {
        for(int i = 0; i < players.Count; i++)
        {
            yield return StartCoroutine(players[i].TakeTurn());
        }
    }

    /// <summary>
    /// Tells worldmanager to rearrange players around the combat circle
    /// </summary>
    public void PlayerAdded()
    {
        float degreesBetweenPlayers = 360f / players.Count;
        float diameter = players.Count * 1.5f;
        float radius = diameter / 2;
        combatCircle.transform.localScale = new Vector2(diameter, diameter);
        for(int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = (Vector2)(Quaternion.Euler(0, 0, degreesBetweenPlayers * i) * Vector2.right) * radius;
        }
    }
}
