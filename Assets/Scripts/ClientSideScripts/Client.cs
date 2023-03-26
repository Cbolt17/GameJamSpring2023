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
    private MenuButtons menuButtons;
    private bool choosing = false;

    private void Start()
    {
        if (!IsOwner) Destroy(this);
        worldManager = WorldManager.instance;
        menuButtons = MenuButtons.instance;
        if (IsServer)
        {
            ActivateWaitingUI();
            worldManager.GetComponent<WorldManagerNetwork>().isTheServer = true;
        }
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (worldManager.allResponsesIn)
        {
            player.isReady = false;
            choosing = false;
        }
        if (!worldManager.allResponsesIn && !player.isReady && !choosing)
        {
            choosing = true;
            ActivateChoosingUI();
        }
        if (IsServer && worldManager.roundStage == 0 && worldManager.players.Count == worldManager.maxNumPlayers)
            StartGame();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }


    public void StartGame()
    {
        worldManager.StartGame();
        DeactivateUI();
        DeactivateChoosingUI();
    }

    public void ChooseItem(int type, int itemNum, int target)
    {
        Item[] items = worldManager.weapons;
        switch (type)
        {
            case 0:
                items = worldManager.weapons;
                break;
            case 1:
                items = worldManager.defense;
                break;
            case 2:
                items = worldManager.special;
                break;
        }
        player.item = items[itemNum];
    }

    /////////Other UI functions

    public void ActivateWaitingUI()
    {
        waitingUI.SetActive(true);
    }

    public void ActivateChoosingUI()
    {
        menuButtons.itemOptions.SetActive(true);
    }

    public void DeactivateChoosingUI()
    {
        menuButtons.itemOptions.SetActive(false);
        menuButtons.unPeekButton.SetActive(false);
    }

    public void ActivateSettingsUI()
    {
        settingsUI.SetActive(true);
    }

    public void DeactivateUI()
    {
        waitingUI.SetActive(false);
    }

    public void DeactivateSettingsUI()
    {
        settingsUI.SetActive(false);
    }
}