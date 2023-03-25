using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public NetworkManager networkManager;
    public GameObject menuOptions;
    public GameObject settingsOptions;
    public GameObject networkOptions;

    public void StartPressed()
    {
        menuOptions.SetActive(false);
        networkOptions.SetActive(true);
    }

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

    public void QuitPressed()
    {
        Application.Quit();
    }
}
