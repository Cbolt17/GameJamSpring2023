
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Enums;
using Unity.VisualScripting;
using TMPro;

public class WorldManager : MonoBehaviour
{
    //ID, Damage, Type, Range, Name, Description, Statuseffect, StatusDuration
    public string[][] itemTemplates = new string[][] {

    };

    public static WorldManager instance;

    public List<Player> players = new List<Player>();
    public Item[] weapons;
    public Item[] defense;
    public Item[] special;
    public TextMeshProUGUI joinCode;

    public bool allResponsesIn = true;
    public int responsesRemaining = 0;

    public bool canJoin = true;
    public float decisionTime = 30f;
    public float responseTimeRemaining = 0f;
    public int maxNumPlayers = 5;
    public int roundStage = 0; //Round stage 0: players selecting items; round stage 1: animations
    public float animationTime = 1f;

    public float volume = 0.75f;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    void Start()
    {

    }

    private void Update()
    {
        if (roundStage == 1)
        {
            joinCode.text = "";
            responseTimeRemaining -= Time.deltaTime;
            if (allResponsesIn || responseTimeRemaining < 0)
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
        responseTimeRemaining = decisionTime;
        roundStage = 1;
    }

    private IEnumerator StartAnimations()
    {
        for (int i = 0; i < players.Count; i++)
        {
            yield return StartCoroutine(takeTurn(players[i]));
        }
        roundStage = 0;
    }

    /// <summary>
    /// Tells worldmanager to rearrange players around the combat circle
    /// </summary>
    public void PlayerAdded()
    {
        float degreesBetweenPlayers = 360f / players.Count;
        float diameter = players.Count * 1.5f;
        float radius = diameter / 2;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = (Vector2)(Quaternion.Euler(0, 0, degreesBetweenPlayers * i) * Vector2.right) * radius;
        }
    }


    private void initItems()
    {
        foreach (string[] itm in itemTemplates)
        {
            Item i = new Item(Int32.Parse(itm[1]),
                (Enums.Type)Enum.Parse(typeof(Enums.Type), itm[2]), (Enums.Range)Enum.Parse(typeof(Enums.Range), itm[3]),
                itm[4], itm[5],
                (Status)Enum.Parse(typeof(Status), itm[6]), Int32.Parse(itm[7]));
            switch (i.type) {
                case Enums.Type.OFFENSE:
                    offence.Add(i);
                    break;
                case Enums.Type.DEFENSE:
                    defense.Add(i);
                    break;
                case Enums.Type.SPECIAL:
                    special.Add(i);
                    break;
            }
        }
    }

    private IEnumerator takeTurn(Player p)
    {
        switch (p.status)
        {
            case Status.NORMAL:
                if (p.item != null)
                {
                    Item i = (Item)p.item;
                    if (i.range != Enums.Range.ALL)
                    {
                        Player[] t = { p.target };
                        i.use(p, t);
                    }
                    else
                    {
                        i.use(p, players.ToArray());
                    }
                }
                break;
            case Status.MEDITATING:
                p.takeDamage(-5);
                break;
            case Status.FROZEN:
                break;
        }
        yield return new WaitForSeconds(1);
    }
}