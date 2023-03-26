using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Collections;

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
    private bool menuMusic = true;
    private bool playingMenu = false;
    private AudioSource menuMus;

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
        menuMus = worldManager.sounds[0];
        playingMenu = true;
        menuMusic = true;
    }

    private void Update()
    {
        if (worldManager.allResponsesIn)
        {
            player.isReady = false;
            choosing = false;
        }
        if (worldManager.roundStage == 2)
        {
            DeactivateChoosingUI();
            choosing = false;
        }
        if (!worldManager.allResponsesIn && !player.isReady && !choosing)
        {
            choosing = true;
            ActivateChoosingUI();
        }
        if (IsServer && worldManager.roundStage == 0 && worldManager.players.Count == worldManager.maxNumPlayers)
            StartGame();

        if (!menuMusic && playingMenu)
        {
            StartCoroutine(StartFade(menuMus, 5f, 0f));
            playingMenu = false;
        }
        else if (menuMusic && !playingMenu)
        {
            StartCoroutine(StartFade(menuMus, 5f, 1f));
            playingMenu = true;
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
        DeactivateChoosingUI();


        addPlayerConnections();
        menuMusic = false;
    }

    private void addPlayerConnections()
    {
        for (int i = 0; i < worldManager.players.Count; i++)
        {
            if (i - 1 < 0)
            {
                worldManager.players[i].connections.Add(worldManager.players[worldManager.players.Count - 1]);
            }
            else
            {
                worldManager.players[i].connections.Add(worldManager.players[i - 1]);
            }
            if (i + 1 >= worldManager.players.Count)
            {
                worldManager.players[i].connections.Add(worldManager.players[0]);
            }
            else
            {
                worldManager.players[i].connections.Add(worldManager.players[i + 1]);
            }
        }
    }

    public void ChooseItem(int type, int itemNum, int target)
    {
        List<Item> items = worldManager.weapons;
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
        player.target = worldManager.players[0];
    }

    /////////Other UI functions

    public void ActivateWaitingUI()
    {
        waitingUI.SetActive(true);
    }

    public void ActivateChoosingUI()
    {
        if (player.getHP() <= 0) return;
        menuButtons.itemOptions.SetActive(true);
    }

    public void DeactivateChoosingUI()
    {
        menuButtons.itemOptions.SetActive(false);
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

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if (duration == 0f)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        yield break;
    }

}