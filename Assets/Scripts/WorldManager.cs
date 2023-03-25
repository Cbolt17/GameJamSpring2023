using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

using Enums;

public class WorldManager : MonoBehaviour
{
    //ID, Damage, Type, Range, Name, Description, Statuseffect, StatusDuration
    public string[][8] itemtemplate
    {
        { "1", "15", "OFFENSE", "MELEE", "Sword", "A cool-looking sword", "NORMAL", "1" }
    }

    public static WorldManager instance;

    public GameObject combatCircle;
    public List<Player> players = new List<Player>();
    public static List<Item> items = new List<Item>();

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

<<<<<<< Updated upstream
=======
    private IEnumerator StartAnimations()
    {
        for(int i = 0; i < players.Count; i++)
        {
            yield return StartCoroutine(takeTurn(players[i]));
        }
    }

    /// <summary>
    /// Tells worldmanager to rearrange players around the combat circle
    /// </summary>
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
    
    private initItems() {
        
    }

    private IEnumerator takeTurn(Player p)
    {
        switch (p.status) {
            case Status.NORMAL:
                if (p.item != null) {
                    Item i = p.item;
                    if (i.range != Range.All) {
                        Player[] t = { p.target };
                        i.use(p, t);
                    } else {
                        i.use(p, players.ToArray);
                    }
                }
                break;
            case Status.MEDITATING:
                p.takeDamage(-5);
                break;
            case Status.FROZEN:
                break;
        }

        yield return new WaitForSeconds();
    }
>>>>>>> Stashed changes
}
