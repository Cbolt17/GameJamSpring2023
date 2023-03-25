using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Client : NetworkBehaviour
{
    public Player player;
    public GameObject waitingUI;
    public GameObject choosingUI;
    public GameObject settingsUI;
    public GameObject choiceButtonUI;

    private WorldManager worldManager;
    private bool peeking = false;

    private void Start()
    {
        if (!IsOwner) Destroy(this);
        worldManager = WorldManager.instance;
        if (IsServer)
        {
            ActivateWaitingUI();
            worldManager.GetComponent<WorldManagerNetwork>().isTheServer = true;
        }
    }

    private void Update()
    {
        if (worldManager.allResponsesIn)
            player.isReady = false;
        if (!worldManager.allResponsesIn && !player.isReady && !choosingUI.activeSelf && !peeking)
        {
            ActivateChoosingUI();
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }


    public void StartGame()
    {
        worldManager.StartGame();
        DeactivateUI();
    }

    public void ChooseItem(int type, int itemNum, int target)
    {
        player.item = new item
        {
            type = type,
            num = itemNum,
            target = target
        };
    }

    /////////Other UI functions

    public void Peek()
    {
        peeking = true;
        choosingUI.gameObject.SetActive(false);
        choiceButtonUI.gameObject.SetActive(true);
    }

    public void UnPeek()
    {
        peeking = false;
        choiceButtonUI.gameObject.SetActive(false);
        choosingUI.gameObject.SetActive(true);
    }

    public void ActivateWaitingUI()
    {
        waitingUI.SetActive(true);
    }

    public void ActivateChoosingUI()
    {
        choosingUI.SetActive(true);
    }

    public void ActivateSettingsUI()
    {
        settingsUI.SetActive(true);
    }

    public void DeactivateUI()
    {   
        waitingUI.SetActive(false);
        choosingUI.SetActive(false);
    }

    public void DeactivateSettingsUI()
    {
        settingsUI.SetActive(false);
    }
}