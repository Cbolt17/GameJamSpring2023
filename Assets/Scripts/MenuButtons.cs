using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public static MenuButtons instance;

    public WorldManager worldManager;

    public NetworkManager networkManager;
    public GameObject menuOptions;
    public GameObject settingsOptions;
    public GameObject networkOptions;
    public GameObject joinOptions;
    public GameObject itemOptions;
    public GameObject unPeekButton;
    public MatchMaking matchMaking;

    public TMP_InputField textInput;
    public TextMeshProUGUI maxPlayersButtonText;
    public TextMeshProUGUI volumeButtonText;
    public TextMeshProUGUI decisionTimeText;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    private void Start()
    {
        worldManager = WorldManager.instance;
    }

    public void StartPressed()
    {
        menuOptions.SetActive(false);
        networkOptions.SetActive(true);
    }

    /*
    public void Host()
    {
        networkManager.StartHost();
        networkOptions.SetActive(false);
    }

    public void Join()
    {
        networkManager.StartClient();
        networkOptions.SetActive(false);
        WorldManager.instance.PlayerAdded();
    }*/

    public void Join()
    {
        networkOptions.SetActive(false);
        joinOptions.SetActive(true);
    }

    public void SelectJoinCodeText()
    {
        textInput.text = "";
    }

    public void DeSelectJoinCodeText()
    {
        if(textInput.text.Equals(""))
            textInput.text = "Enter Code Here";
    }

    public void HostGame()
    {
        networkOptions.SetActive(false);
        matchMaking.CreateGame();
    }

    public void JoinGame()
    {
        joinOptions.SetActive(false);
        matchMaking.JoinGame(textInput.text);
    }

    public void OptionsPressed()
    {
        menuOptions.SetActive(false);
        settingsOptions.SetActive(true);
    }

    public void Back()
    {
        settingsOptions.SetActive(false);
        networkOptions.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void BackFromJoin()
    {
        joinOptions.SetActive(false);
        networkOptions.SetActive(true);
        textInput.text = "Enter Code Here";
    }

    public void ShiftMaxPlayers()
    {
        matchMaking.maxPlayers += 5;
        if(matchMaking.maxPlayers > 20) matchMaking.maxPlayers = 5;
        worldManager.maxNumPlayers = matchMaking.maxPlayers;
        maxPlayersButtonText.text = "Max Players: " + matchMaking.maxPlayers;
    }

    public void DecisionTime()
    {
        float time = worldManager.decisionTime;
        if (time < 20)
            time += 5;
        else if (time < 60)
            time += 10;
        else if (time < 120)
            time += 20;
        else
            time = 5;
        worldManager.decisionTime = time;
        decisionTimeText.text = "Decision Time: " + time;
    }

    public void Volume()
    {
        worldManager.volume -= 0.25f;
        if(worldManager.volume < 0)
            worldManager.volume = 1;
        string s = "";
        switch(worldManager.volume)
        {
            case 1:
                s = "Max";
                break;
                case 0.75f:
                s = "Medium";
                break;
            case 0.5f:
                s = "low";
                break;
            case 0.25f:
                s = "Very low";
                break;
            case 0:
                s = "Off";
                break;
        }
        volumeButtonText.text = "Volume: " + s;
    }

    public void ToggleItems()
    {
        itemOptions.SetActive(false);
    }
    
    public void TogglePeek()
    {
        unPeekButton.SetActive(false);
    }

    public void SetPlayerItemWeapon(int itemNum)
    {
        Client client = FindObjectOfType<Client>();
            client.ChooseItem(0, itemNum, 0);
    }
    public void SetPlayerItemDefense(int itemNum)
    {
        Client client = FindObjectOfType<Client>();
        client.ChooseItem(1, itemNum, 0);
    }
    public void SetPlayerItemSpecial(int itemNum)
    {
        Client client = FindObjectOfType<Client>();
        client.ChooseItem(2, itemNum, 0);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
